import { Component, Host, h, Prop } from '@stencil/core';
import { IItemViewModel, ItemClient, UIInfo } from '../../services/services';
import state, { store, localizationState } from '../../store/state';

@Component({
  tag: 'my-item-details',
  styleUrl: 'my-item-details.scss',
  shadow: true,
})
export class MyItemDetails {
  /** The item to display */
  @Prop() item!: IItemViewModel;

  private modal!: HTMLDnnModalElement;
  private editForm!: HTMLMyEditElement;
  private itemClient!: ItemClient;
  private resx: UIInfo;

  constructor() {
    this.setCookie(state.moduleId);
    this.itemClient = new ItemClient({ moduleId: state.moduleId });
    //state.moduleId = this.moduleId;
    this.resx = localizationState.viewModel.uI;
  }

  private setCookie(iModID: number): void {
    const date = new Date();

    // Set it expire in 7 days
    date.setTime(date.getTime() + (7 * 24 * 60 * 60 * 1000));

    // Set it
    document.cookie = "moduleID=" + iModID + "; expires="+date.toUTCString()+"; path=/";
  }

  private deleteItem(): void {
    this.itemClient.deleteItem(this.item.id)
      .then(async function() {
        state.lastFetchedPage--; // prevent incorrect data from being loaded
        state.items = [];
        state.availableItems = 0;
        await state.m_cMyItemsList.loadMore();
        const oldCanEdit = state.userCanEdit;
        store.reset();
        state.userCanEdit = oldCanEdit;
      })
  }

  render() {
    return (
      <Host>
        <div class="item-details">
          {this.item.description}
        </div>
        {state.userCanEdit &&
          <div class="controls">
            <dnn-button
              type="primary"
              onClick={() => this.modal.show().then(() => this.editForm.setFocus())}
            >
              {this.resx.edit || "Edit"}
            </dnn-button>
            <dnn-button
              type="secondary"
              confirm
              confirmMessage={this.resx.deleteItemConfirm || "Are you sure you want to delete this item?"}
              confirmNoText={this.resx.no || "No"}
              confirmYesText={this.resx.yes || "Yes"}
              onConfirmed={() => this.deleteItem()}
            >{this.resx.delete || "Delete"}</dnn-button>
            <dnn-modal
              ref={e => this.modal = e}
              showCloseButton={false}
              backdropDismiss={false}
            >
              <my-edit ref={e => this.editForm = e} item={this.item} />
            </dnn-modal>
          </div>
        }
      </Host>
    );
  }
}
