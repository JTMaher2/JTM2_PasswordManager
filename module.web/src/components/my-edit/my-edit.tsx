import {
  Component, Host, h, Element, Method,
  Event, EventEmitter, Prop
} from '@stencil/core';
import { CreateItemDTO, IItemViewModel, ItemClient, UIInfo, UpdateItemDTO } from '../../services/services';
import state, { store, localizationState } from "../../store/state";
import alertError from "../../services/alert-error";

@Component({
  tag: 'my-edit',
  styleUrl: 'my-edit.scss',
  shadow: true,
})
export class MyEdit {
  /** The item to create or edit. */
  @Prop({ mutable: true }) item: IItemViewModel;

  @Element() el!: HTMLMyEditElement;

  private nameInput!: HTMLInputElement;
  private itemClient!: ItemClient;
  private resx: UIInfo;

  constructor() {
    //state.moduleId = this.moduleId;
    //alert("My Edit Constructor");
    this.itemClient = new ItemClient({
      moduleId: state.moduleId,
    });
    this.resx = localizationState.viewModel.uI;
  }

  /** Sets focus on the first form element */
  @Method()
  public async setFocus() {
    setTimeout(() => {
      this.nameInput.focus();
    }, 500);
  }

  /** Resets the form to insert a new item. */
  @Method()
  public async resetForm() {
    this.item = {
      id: -1,
      name: "",
      description: "",
      m_StrMasterPassword: ""
    }
  }

  /** Fires up when an item got created. */
  @Event() itemCreated: EventEmitter

  componentWillLoad() {
    //alert("ComponentWillLoad called");
    if (this.item == undefined) {
      this.resetForm();
    }
  }

  private hideModal(): void {
    this.el.closest("dnn-modal").hide();
  }

  private saveItem(): void {

    if (this.item.id < 1) {
      this.itemClient.createItem(new CreateItemDTO({
        name: this.item.name,
        description: this.item.description,
        m_StrMasterPassword: sessionStorage.getItem("m_StrMasterPassword")
      }))
        .then(async () => {
          this.hideModal(); // why does createItem() need a then() callback, while updateItem() does not need one?
        },
          reason => alertError(reason))
        .catch(reason => alertError(reason));
    }
    else {
      this.itemClient.updateItem(new UpdateItemDTO({
        id: this.item.id,
        name: this.item.name,
        description: this.item.description,
        m_StrMasterPassword: sessionStorage.getItem("m_StrMasterPassword"),
      })).catch(reason => alert(reason));
    }

    const oldCanEdit = state.userCanEdit;
    store.reset();
    state.userCanEdit = oldCanEdit;
  }

  render() {
    return (
      <Host>
        <div class="grid">
          <label htmlFor="name">{this.resx.name || "Name"}</label>
          <input
            id="name"
            type="text"
            value={this.item.name}
            required
            ref={e => this.nameInput = e}
            onInput={e => this.item = { ...this.item, name: (e.target as HTMLInputElement).value }}
          />

          <label htmlFor="description">{this.resx.description || "Description"}</label>
          <textarea
            id="description"
            value={this.item.description}
            onInput={e => this.item = { ...this.item, description: (e.target as HTMLTextAreaElement).value }} />
        </div>
        <div class="controls">
          <dnn-button
            type="secondary"
            reversed
            onClick={() => this.hideModal()}
          >
            {this.resx.cancel || "Cancel"}
          </dnn-button>
          {this.item.id < 1 &&
            <dnn-button
              type="primary"
              disabled={this.item.name.trim().length === 0}
              onClick={() => this.saveItem()}
            >
              {this.resx.create || "Create"}
            </dnn-button>
          }
          {this.item.id > 0 &&
            <dnn-button
              type="primary"
              disabled={this.item.name.trim().length === 0}
              onClick={() => this.saveItem()}
            >
              {this.resx.save || "Save"}
            </dnn-button>
          }
        </div>
      </Host>
    );
  }
}
