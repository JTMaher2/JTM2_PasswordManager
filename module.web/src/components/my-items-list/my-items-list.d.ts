export declare class MyItemsList {
    /** Defines how many items to fetch per request. */
    pageSize: number;
    /** Defines how many pixels under the fold to preload. */
    preloadPixels: number;
    loading: boolean;
    el: HTMLMyItemsListElement;
    private itemClient;
    private abortController;
    private resx;
    constructor();
    connectedCallback(): void;
    disconnectedCallback(): void;
    componentDidLoad(): void;
    componentDidUpdate(): void;
    private preload;
    private scrollHandler;
    private handleScroll;
    loadMore(): Promise<void>;
    render(): any;
}
