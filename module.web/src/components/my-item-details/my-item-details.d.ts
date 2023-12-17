import { IItemViewModel } from '../../services/services';
export declare class MyItemDetails {
    /** The item to display */
    item: IItemViewModel;
    private modal;
    private editForm;
    private itemClient;
    private resx;
    constructor();
    private setCookie;
    private deleteItem;
    render(): any;
}
