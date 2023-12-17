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
export declare function restoreModuleIDFromCookie(): number;
/** Initializes the store with an initial (default) state. */
export declare const store: import("@stencil/store").ObservableMap<IStore>;
declare const _default: IStore;
export default _default;
interface LocalizationStore {
    viewModel: LocalizationViewModel;
}
export declare const localizationState: LocalizationStore;
export declare const resx: LocalizationViewModel;
