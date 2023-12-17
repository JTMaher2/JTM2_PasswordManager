import { EventEmitter } from '@stencil/core';
import { IItemViewModel } from '../../services/services';
export declare class MyEdit {
    /** The item to create or edit. */
    item: IItemViewModel;
    el: HTMLMyEditElement;
    private nameInput;
    private itemClient;
    private resx;
    constructor();
    /** Sets focus on the first form element */
    setFocus(): Promise<void>;
    /** Resets the form to insert a new item. */
    resetForm(): Promise<void>;
    /** Fires up when an item got created. */
    itemCreated: EventEmitter;
    componentWillLoad(): void;
    private hideModal;
    private saveItem;
    render(): any;
}
