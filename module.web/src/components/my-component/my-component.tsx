import { Component, h, Prop, Host, Element, Listen } from "@stencil/core";
import { ItemClient, LocalizationClient, LocalizationViewModel } from "../../services/services";
import state, { localizationState } from "../../store/state";
import alertError from "../../services/alert-error";

@Component({
  tag: 'my-component',
  styleUrl: 'my-component.scss',
  shadow: true
})
export class MyComponent {
  private service: ItemClient;
  private localizationService: LocalizationClient;
  private resx: LocalizationViewModel;

  constructor() {
    //alert("MyComponent constructor");
    this.service = new ItemClient({ moduleId: this.moduleId });
    state.moduleId = this.moduleId;
    this.localizationService = new LocalizationClient({ moduleId: this.moduleId });
  }

  @Element() el: HTMLMyComponentElement;

  /** The Dnn module id, required in order to access web services. */
  @Prop() moduleId!: number;

  componentWillLoad() {
    //alert("ComponentWillLoad called");
    return new Promise<void>((resolve, reject) => {
      this.localizationService.getLocalization()
        .then(l => {
          localizationState.viewModel = l;
          this.resx = localizationState.viewModel;
          resolve();
        })
        .catch(reason => {
          alertError(reason);
          reject();
        });
    })
  }

  componentDidLoad(): void {
    //alert("ComponentDiLoad called");
    ($($("my-component").prop("shadowRoot")).find("#dialog") as any).dialog(); // open master password prompt

    this.service.userCanEdit().then(canEdit => state.userCanEdit = canEdit);
  }

  @Listen("itemCreated")
  handleItemCreated() {
    state.searchQuery = "";
  }

  async OnClick() {
    ($("#dialog") as any).dialog("close"); // close master password prompt
    state.m_strMasterPassword = $("#password_input").val() as string;
    state.lastFetchedPage--; // prevent incorrect data from being loaded
    state.items = [];
    state.availableItems = 0;
    await state.m_cMyItemsList.loadMore();
  }

  render() {
    return <Host>
      <div class="header">
        <div id="dialog" title="Enter master password">
          <input id="password_input" type="password" title="password" />
          <dnn-button type="primary" reversed onClick={async () => await this.OnClick()}>
            {"Submit"}
          </dnn-button>
        </div>
        <dnn-searchbox placeholder={this.resx.uI.searchPlaceholder || "Search"} onQueryChanged={e => state.searchQuery = e.detail} />
        {state.userCanEdit &&
          <my-create />
        }
      </div>
      <my-items-list />
    </Host>;
  }
}
