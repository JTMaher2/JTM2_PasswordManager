export declare class MyComponent {
    private service;
    private localizationService;
    private resx;
    constructor();
    el: HTMLMyComponentElement;
    /** The Dnn module id, required in order to access web services. */
    moduleId: number;
    componentWillLoad(): Promise<void>;
    componentDidLoad(): void;
    handleItemCreated(): void;
    OnClick(): Promise<void>;
    render(): any;
}
