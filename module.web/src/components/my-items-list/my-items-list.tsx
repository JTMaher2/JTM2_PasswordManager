import { Debounce } from '@dnncommunity/dnn-elements';
import { Component, Host, h, State, Prop, Element } from '@stencil/core';
import { ItemClient, UIInfo } from '../../services/services';
import state, { localizationState } from '../../store/state';

@Component({
  tag: 'my-items-list',
  styleUrl: 'my-items-list.scss',
  shadow: true,
})
export class MyItemsList {
  /** Defines how many items to fetch per request. */
  @Prop() pageSize = 100;

  /** Defines how many pixels under the fold to preload. */
  @Prop() preloadPixels = 1000;

  @State() loading = true;

  @Element() el: HTMLMyItemsListElement;

  private itemClient!: ItemClient;
  private abortController: AbortController;
  private resx: UIInfo;

  constructor() {
    //alert("My Items List Constructor");
    //state.moduleId = this.moduleId;
    this.itemClient = new ItemClient({
      moduleId: state.moduleId,
    });
    this.resx = localizationState.viewModel.uI;
    state.m_cMyItemsList = this;
  }

  connectedCallback() {
    window.addEventListener("scroll", this.scrollHandler);
  }

  disconnectedCallback() {
    window.removeEventListener("scroll", this.scrollHandler);
  }

  componentDidLoad() {
    //alert("ComponentDidLoad called");
    requestAnimationFrame(() => {
      this.preload();
    })
  }

  componentDidUpdate() {
    this.itemClient = new ItemClient({
      moduleId: state.moduleId, // for some reason, state.moduleId gets set to null
    });
    this.preload();
  }

  private async preload() {
    if (this.el.getBoundingClientRect().bottom - window.innerHeight < this.preloadPixels) {
      if (!state.allLoaded) {
        await this == null ?
          null :
          this.loadMore()
            .then(() => {
              if (!state.allLoaded) {
                this.preload();
              }
            })
            .catch(() => { });
      }
    }
  }

  private scrollHandler = () => {
    this.handleScroll();
  }

  @Debounce()
  private async handleScroll() {
    if (this.el.getBoundingClientRect().bottom - window.innerHeight < this.preloadPixels) {
      await this == null ?
        null :
        this.loadMore();
    }
  }

  public async loadMore() {
    //await new Promise(resolve => setTimeout(resolve, 1000)); // workaround to fix update problem
    return new Promise<void>((resolve, reject) => {
      //if (state.items.length == 0 || state.items.length < state.availableItems) {
      requestAnimationFrame(() => {
        this.loading = true;
        this.abortController?.abort();
        this.abortController = new AbortController();
        var strMasterPassword = sessionStorage.getItem("m_StrMasterPassword");
        if (strMasterPassword !== null) {
          this.itemClient.getItemsPage(
            strMasterPassword,
            state.searchQuery,
            state.lastFetchedPage + 1,
            this.pageSize,
            false,
            this.abortController.signal)
            .then(results => {
              state.items = [...state.items, ...results.items];
              state.availableItems = results.resultCount;
              state.lastFetchedPage = results.page;
              state.totalPages = results.pageCount;
              this.loading = false;
              if (state.items.length === results.resultCount) {
                state.allLoaded = true;
              }
              resolve();
            }, rejectReason => {
              if (rejectReason instanceof DOMException && rejectReason.code === rejectReason.ABORT_ERR) {
                reject(() => { });
                return;
              }
              alert(rejectReason);
              reject(rejectReason);
            })
            .catch(rejectReason => {
              alert(rejectReason);
              reject(rejectReason);
            });
        }
      });
    });
  }

  render() {
    return (
      <Host>
        {state.items.map(item =>
          <div class="item">
            <div
              class="title"
              onClick={() => {
                if (state.expandedItemId === item.id) {
                  state.expandedItemId = -1;
                } else {
                  state.expandedItemId = item.id;
                }
              }}
            >
              <dnn-chevron
                expanded={state.expandedItemId === item.id}
              />
              {item.name}
            </div>
            <dnn-collapsible expanded={state.expandedItemId === item.id}>
              {state.expandedItemId === item.id &&
                <my-item-details item={item} />
              }
            </dnn-collapsible>
          </div>
        )}
        {this.loading &&
          <div class="loading"></div>
        }
        <div class="footer">
          <p>{this.resx.shownItems.replace("{0}", state.items.length.toString()).replace("{1}", state.availableItems.toString())}</p>
          {!this.loading && state.items.length < state.availableItems &&
            <dnn-button type="primary" reversed
              onClick={async () => await this == null ?
                null :
                this.loadMore()}
            >
              {this.resx.loadMore || "Load More"}
            </dnn-button>
          }
        </div>
      </Host>
    );
  }
}
