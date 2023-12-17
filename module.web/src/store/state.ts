import { createStore } from "@stencil/store";
import { IItemViewModel, LocalizationViewModel } from "../services/services";
import { MyItemsList } from "../components/my-items-list/my-items-list";

/** Defines the shape of the global state store. */
interface IStore {
  /** Indicates whether all the available items are already loaded. */
  allLoaded: boolean;

  /** The total amount of items availalble
   * (but may not be fetched yet because of paging support) */
  availableItems: number;

  /** The currently expanded item id */
  expandedItemId: number;

  /** The list of items, could be partial since we have paging. */
  items: IItemViewModel[];

  /** The id of the last page of items we already fetched. */
  lastFetchedPage: number;

  /** The id of the Dnn module */
  moduleId: number;

  /** The current master password */
  m_strMasterPassword: string;

  /** The current search query */
  searchQuery: string;

  /** The total amount of pages we know will be available to fetch. */
  totalPages: number;

  /** Indicates whether the current user can edit items. */
  userCanEdit: boolean;

  m_cMyItemsList: MyItemsList;
}

export function restoreModuleIDFromCookie(): number {
  let strID = $("div[id^=\"dnn_ctr\"][id$=\"_ModuleContent\"]").prop("id");
  return parseInt(strID.substring("dnn_ctr".length, strID.lastIndexOf("_")), 10);
}

/** Initializes the store with an initial (default) state. */
export const store = createStore<IStore>({
  allLoaded: false,
  availableItems: 0,
  expandedItemId: -1,
  items: [],
  lastFetchedPage: 0,
  moduleId: restoreModuleIDFromCookie(),
  m_strMasterPassword: $("#dialog").text(),
  searchQuery: "",
  totalPages: 0,
  userCanEdit: false,
  m_cMyItemsList: null
});

store.onChange("searchQuery", () => {
  store.state.allLoaded = false;
  store.state.availableItems = 0;
  store.state.expandedItemId = -1;
  store.state.items = [];
  store.state.lastFetchedPage = 0;
  store.state.totalPages = 0;
});

export default store.state;

interface LocalizationStore {
  viewModel: LocalizationViewModel;
}

const localizationStore = createStore<LocalizationStore>({
  viewModel: new LocalizationViewModel(),
});

export const localizationState = localizationStore.state;
export const resx = localizationStore.state.viewModel; 
