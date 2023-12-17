//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.16.1.0 (NJsonSchema v10.7.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { DnnServicesFramework } from '@dnncommunity/dnn-elements';
export class ClientBase {

  private sf: DnnServicesFramework;
  private moduleId: number;

  constructor(configuration: ConfigureRequest) {
    this.moduleId = configuration.moduleId;
    this.sf = new DnnServicesFramework(this.moduleId);
  }

  protected getBaseUrl(_defaultUrl: string, baseUrl?: string): string {
    baseUrl = this.sf.getServiceRoot("JTMaher_JTM2_PasswordsStencilJS");

    // Strips the last / if present for future concatenations
    baseUrl = baseUrl.replace(/\/$/, "");

    return baseUrl || "";
  }

  protected transformOptions(options: RequestInit): Promise<RequestInit> {
    const dnnHeaders = this.sf.getModuleHeaders();

    dnnHeaders.forEach((value, key) => {
      options.headers[key] = value;
    });

    return Promise.resolve(options);
  }
}

export class ItemClient extends ClientBase {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(configuration: ConfigureRequest, baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super(configuration);
        this.http = http ? http : window as any;
        this.baseUrl = this.getBaseUrl("", baseUrl);
    }

    /**
     * Creates a new item.
     * @param item (optional) The item to create.
     * @return OK
     */
    createItem(item: CreateItemDTO | null | undefined, signal?: AbortSignal | undefined): Promise<ItemViewModel> {
        let url_ = this.baseUrl + "/Item/CreateItem";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(item);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            signal,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processCreateItem(_response);
        });
    }

    protected processCreateItem(response: Response): Promise<ItemViewModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ItemViewModel.fromJS(resultData200);
            return result200;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result400 = resultData400 !== undefined ? resultData400 : <any>null;
    
            return throwException("Bad Request", status, _responseText, _headers, result400);
            });
        } else if (status === 500) {
            return response.text().then((_responseText) => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = Exception.fromJS(resultData500);
            return throwException("Error", status, _responseText, _headers, result500);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ItemViewModel>(null as any);
    }

    /**
     * Gets a paged and sorted list of items matching a certain query.
     * @param m_StrMasterPassword (optional) Gets or sets the master password.
     * @param query (optional) Gets or sets the optional search query.
     * @param page (optional) Gets or sets the page number to get.
     * @param pageSize (optional) Gets or sets the size of pages.
     * @param descending (optional) Gets or sets a value indicating whether the items should be ordered descending.
     * @return OK
     */
    getItemsPage(m_StrMasterPassword: string | null | undefined, query: string | null | undefined, page: number | undefined, pageSize: number | undefined, descending: boolean | undefined, signal?: AbortSignal | undefined): Promise<ItemsPageViewModel> {
        let url_ = this.baseUrl + "/Item/GetItemsPage?";
        if (m_StrMasterPassword !== undefined && m_StrMasterPassword !== null)
            url_ += "M_StrMasterPassword=" + encodeURIComponent("" + m_StrMasterPassword) + "&";
        if (query !== undefined && query !== null)
            url_ += "Query=" + encodeURIComponent("" + query) + "&";
        if (page === null)
            throw new Error("The parameter 'page' cannot be null.");
        else if (page !== undefined)
            url_ += "Page=" + encodeURIComponent("" + page) + "&";
        if (pageSize === null)
            throw new Error("The parameter 'pageSize' cannot be null.");
        else if (pageSize !== undefined)
            url_ += "PageSize=" + encodeURIComponent("" + pageSize) + "&";
        if (descending === null)
            throw new Error("The parameter 'descending' cannot be null.");
        else if (descending !== undefined)
            url_ += "Descending=" + encodeURIComponent("" + descending) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            signal,
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processGetItemsPage(_response);
        });
    }

    protected processGetItemsPage(response: Response): Promise<ItemsPageViewModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ItemsPageViewModel.fromJS(resultData200);
            return result200;
            });
        } else if (status === 500) {
            return response.text().then((_responseText) => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = Exception.fromJS(resultData500);
            return throwException("Error", status, _responseText, _headers, result500);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ItemsPageViewModel>(null as any);
    }

    /**
     * Deletes an existing item.
     * @param itemId The id of the item to delete.
     * @return OK
     */
    deleteItem(itemId: number, signal?: AbortSignal | undefined): Promise<void> {
        let url_ = this.baseUrl + "/Item/DeleteItem?";
        if (itemId === undefined || itemId === null)
            throw new Error("The parameter 'itemId' must be defined and cannot be null.");
        else
            url_ += "itemId=" + encodeURIComponent("" + itemId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "POST",
            signal,
            headers: {
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processDeleteItem(_response);
        });
    }

    protected processDeleteItem(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            return;
            });
        } else if (status === 500) {
            return response.text().then((_responseText) => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = Exception.fromJS(resultData500);
            return throwException("Error", status, _responseText, _headers, result500);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }

    /**
     * Checks if a user can edit the current items.
     * @return OK
     */
    userCanEdit(signal?: AbortSignal | undefined): Promise<boolean> {
        let url_ = this.baseUrl + "/Item/UserCanEdit";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            signal,
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processUserCanEdit(_response);
        });
    }

    protected processUserCanEdit(response: Response): Promise<boolean> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
                result200 = resultData200 !== undefined ? resultData200 : <any>null;
    
            return result200;
            });
        } else if (status === 500) {
            return response.text().then((_responseText) => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = Exception.fromJS(resultData500);
            return throwException("Error", status, _responseText, _headers, result500);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<boolean>(null as any);
    }

    /**
     * Updates an existing item.
     * @param item (optional) The new information about the item, UpdateItemDTO.
     * @return OK
     */
    updateItem(item: UpdateItemDTO | null | undefined, signal?: AbortSignal | undefined): Promise<void> {
        let url_ = this.baseUrl + "/Item/UpdateItem";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(item);

        let options_: RequestInit = {
            body: content_,
            method: "POST",
            signal,
            headers: {
                "Content-Type": "application/json",
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processUpdateItem(_response);
        });
    }

    protected processUpdateItem(response: Response): Promise<void> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            return;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ArgumentException.fromJS(resultData400);
            return throwException("Malformed request", status, _responseText, _headers, result400);
            });
        } else if (status === 500) {
            return response.text().then((_responseText) => {
            let result500: any = null;
            let resultData500 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result500 = Exception.fromJS(resultData500);
            return throwException("Error", status, _responseText, _headers, result500);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<void>(null as any);
    }
}

export class LocalizationClient extends ClientBase {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(configuration: ConfigureRequest, baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        super(configuration);
        this.http = http ? http : window as any;
        this.baseUrl = this.getBaseUrl("", baseUrl);
    }

    /**
     * Gets localization keys and values.
     * @return OK
     */
    getLocalization(signal?: AbortSignal | undefined): Promise<LocalizationViewModel> {
        let url_ = this.baseUrl + "/Localization/GetLocalization";
        url_ = url_.replace(/[?&]$/, "");

        let options_: RequestInit = {
            method: "GET",
            signal,
            headers: {
                "Accept": "application/json"
            }
        };

        return this.transformOptions(options_).then(transformedOptions_ => {
            return this.http.fetch(url_, transformedOptions_);
        }).then((_response: Response) => {
            return this.processGetLocalization(_response);
        });
    }

    protected processGetLocalization(response: Response): Promise<LocalizationViewModel> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = LocalizationViewModel.fromJS(resultData200);
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<LocalizationViewModel>(null as any);
    }
}

/** Represents the basic information about an item. */
export class ItemViewModel implements IItemViewModel {
    /** Gets or sets the id of the item. */
    id!: number;
    /** Gets or sets the name of the item. */
    name!: string;
    /** Gets or sets the item description. */
    description?: string | undefined;
    /** Gets or sets the item master password. */
    m_StrMasterPassword?: string | undefined;

    constructor(data?: IItemViewModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["Id"];
            this.name = _data["Name"];
            this.description = _data["Description"];
            this.m_StrMasterPassword = _data["M_StrMasterPassword"];
        }
    }

    static fromJS(data: any): ItemViewModel {
        data = typeof data === 'object' ? data : {};
        let result = new ItemViewModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Id"] = this.id;
        data["Name"] = this.name;
        data["Description"] = this.description;
        data["M_StrMasterPassword"] = this.m_StrMasterPassword;
        return data;
    }
}

/** Represents the basic information about an item. */
export interface IItemViewModel {
    /** Gets or sets the id of the item. */
    id: number;
    /** Gets or sets the name of the item. */
    name: string;
    /** Gets or sets the item description. */
    description?: string | undefined;
    /** Gets or sets the item master password. */
    m_StrMasterPassword?: string | undefined;
}

export class Exception implements IException {
    message!: string;
    innerException?: Exception | undefined;
    source?: string | undefined;
    stackTrace?: string | undefined;

    constructor(data?: IException) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.message = _data["Message"];
            this.innerException = _data["InnerException"] ? Exception.fromJS(_data["InnerException"]) : <any>undefined;
            this.source = _data["Source"];
            this.stackTrace = _data["StackTrace"];
        }
    }

    static fromJS(data: any): Exception {
        data = typeof data === 'object' ? data : {};
        let result = new Exception();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Message"] = this.message;
        data["InnerException"] = this.innerException ? this.innerException.toJSON() : <any>undefined;
        data["Source"] = this.source;
        data["StackTrace"] = this.stackTrace;
        return data;
    }
}

export interface IException {
    message: string;
    innerException?: Exception | undefined;
    source?: string | undefined;
    stackTrace?: string | undefined;
}

/** Data transfer object to create a new item. */
export class CreateItemDTO implements ICreateItemDTO {
    /** Gets or sets the name for the item. */
    name!: string;
    /** Gets or sets the description of the item. */
    description?: string | undefined;
    /** Gets or sets the master password for the item. */
    m_StrMasterPassword?: string | undefined;

    constructor(data?: ICreateItemDTO) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["Name"];
            this.description = _data["Description"];
            this.m_StrMasterPassword = _data["M_StrMasterPassword"];
        }
    }

    static fromJS(data: any): CreateItemDTO {
        data = typeof data === 'object' ? data : {};
        let result = new CreateItemDTO();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Name"] = this.name;
        data["Description"] = this.description;
        data["M_StrMasterPassword"] = this.m_StrMasterPassword;
        return data;
    }
}

/** Data transfer object to create a new item. */
export interface ICreateItemDTO {
    /** Gets or sets the name for the item. */
    name: string;
    /** Gets or sets the description of the item. */
    description?: string | undefined;
    /** Gets or sets the master password for the item. */
    m_StrMasterPassword?: string | undefined;
}

/** Represents a page of items, Item. */
export class ItemsPageViewModel implements IItemsPageViewModel {
    /** Gets or sets a list of items for this page. */
    items?: ItemViewModel[] | undefined;
    /** Gets or sets the current page number. */
    page!: number;
    /** Gets or sets the total amount of results found. */
    resultCount!: number;
    /** Gets or sets the total amount of pages available. */
    pageCount!: number;

    constructor(data?: IItemsPageViewModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (Array.isArray(_data["Items"])) {
                this.items = [] as any;
                for (let item of _data["Items"])
                    this.items!.push(ItemViewModel.fromJS(item));
            }
            this.page = _data["Page"];
            this.resultCount = _data["ResultCount"];
            this.pageCount = _data["PageCount"];
        }
    }

    static fromJS(data: any): ItemsPageViewModel {
        data = typeof data === 'object' ? data : {};
        let result = new ItemsPageViewModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.items)) {
            data["Items"] = [];
            for (let item of this.items)
                data["Items"].push(item.toJSON());
        }
        data["Page"] = this.page;
        data["ResultCount"] = this.resultCount;
        data["PageCount"] = this.pageCount;
        return data;
    }
}

/** Represents a page of items, Item. */
export interface IItemsPageViewModel {
    /** Gets or sets a list of items for this page. */
    items?: ItemViewModel[] | undefined;
    /** Gets or sets the current page number. */
    page: number;
    /** Gets or sets the total amount of results found. */
    resultCount: number;
    /** Gets or sets the total amount of pages available. */
    pageCount: number;
}

export class SystemException extends Exception implements ISystemException {

    constructor(data?: ISystemException) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
    }

    static fromJS(data: any): SystemException {
        data = typeof data === 'object' ? data : {};
        let result = new SystemException();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        super.toJSON(data);
        return data;
    }
}

export interface ISystemException extends IException {
}

export class ArgumentException extends SystemException implements IArgumentException {
    message!: string;
    paramName?: string | undefined;

    constructor(data?: IArgumentException) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.message = _data["Message"];
            this.paramName = _data["ParamName"];
        }
    }

    static fromJS(data: any): ArgumentException {
        data = typeof data === 'object' ? data : {};
        let result = new ArgumentException();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Message"] = this.message;
        data["ParamName"] = this.paramName;
        super.toJSON(data);
        return data;
    }
}

export interface IArgumentException extends ISystemException {
    message: string;
    paramName?: string | undefined;
}

/** Data transfer object used to update an item. */
export class UpdateItemDTO extends CreateItemDTO implements IUpdateItemDTO {
    /** Gets or sets the id of the item to edit. */
    id!: number;

    constructor(data?: IUpdateItemDTO) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.id = _data["Id"];
        }
    }

    static fromJS(data: any): UpdateItemDTO {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateItemDTO();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["Id"] = this.id;
        super.toJSON(data);
        return data;
    }
}

/** Data transfer object used to update an item. */
export interface IUpdateItemDTO extends ICreateItemDTO {
    /** Gets or sets the id of the item to edit. */
    id: number;
}

/** A viewmodel that exposes all resource keys in strong types. */
export class LocalizationViewModel implements ILocalizationViewModel {
    /** Localized strings present the ModelValidation resources. */
    modelValidation?: ModelValidationInfo | undefined;
    /** Localized strings present the UI resources. */
    uI?: UIInfo | undefined;

    constructor(data?: ILocalizationViewModel) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.modelValidation = _data["ModelValidation"] ? ModelValidationInfo.fromJS(_data["ModelValidation"]) : <any>undefined;
            this.uI = _data["UI"] ? UIInfo.fromJS(_data["UI"]) : <any>undefined;
        }
    }

    static fromJS(data: any): LocalizationViewModel {
        data = typeof data === 'object' ? data : {};
        let result = new LocalizationViewModel();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["ModelValidation"] = this.modelValidation ? this.modelValidation.toJSON() : <any>undefined;
        data["UI"] = this.uI ? this.uI.toJSON() : <any>undefined;
        return data;
    }
}

/** A viewmodel that exposes all resource keys in strong types. */
export interface ILocalizationViewModel {
    /** Localized strings present the ModelValidation resources. */
    modelValidation?: ModelValidationInfo | undefined;
    /** Localized strings present the UI resources. */
    uI?: UIInfo | undefined;
}

/** Localized strings for the ModelValidation resources. */
export class ModelValidationInfo implements IModelValidationInfo {
    /** Gets or sets the IdGreaterThanZero localized text. */
    idGreaterThanZero?: string | undefined;
    /** Gets or sets the NameRequired localized text. */
    nameRequired?: string | undefined;

    constructor(data?: IModelValidationInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.idGreaterThanZero = _data["IdGreaterThanZero"];
            this.nameRequired = _data["NameRequired"];
        }
    }

    static fromJS(data: any): ModelValidationInfo {
        data = typeof data === 'object' ? data : {};
        let result = new ModelValidationInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["IdGreaterThanZero"] = this.idGreaterThanZero;
        data["NameRequired"] = this.nameRequired;
        return data;
    }
}

/** Localized strings for the ModelValidation resources. */
export interface IModelValidationInfo {
    /** Gets or sets the IdGreaterThanZero localized text. */
    idGreaterThanZero?: string | undefined;
    /** Gets or sets the NameRequired localized text. */
    nameRequired?: string | undefined;
}

/** Localized strings for the UI resources. */
export class UIInfo implements IUIInfo {
    /** Gets or sets the AddItem localized text. */
    addItem?: string | undefined;
    /** Gets or sets the Cancel localized text. */
    cancel?: string | undefined;
    /** Gets or sets the Create localized text. */
    create?: string | undefined;
    /** Gets or sets the Delete localized text. */
    delete?: string | undefined;
    /** Gets or sets the DeleteItemConfirm localized text. */
    deleteItemConfirm?: string | undefined;
    /** Gets or sets the Description localized text. */
    description?: string | undefined;
    /** Gets or sets the Edit localized text. */
    edit?: string | undefined;
    /** Gets or sets the LoadMore localized text. */
    loadMore?: string | undefined;
    /** Gets or sets the Name localized text. */
    name?: string | undefined;
    /** Gets or sets the No localized text. */
    no?: string | undefined;
    /** Gets or sets the Save localized text. */
    save?: string | undefined;
    /** Gets or sets the SearchPlaceholder localized text. */
    searchPlaceholder?: string | undefined;
    /** Gets or sets the ShownItems localized text. */
    shownItems?: string | undefined;
    /** Gets or sets the M_StrMasterPassword localized text. */
    m_StrMasterPassword?: string | undefined;
    /** Gets or sets the Yes localized text. */
    yes?: string | undefined;

    constructor(data?: IUIInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.addItem = _data["AddItem"];
            this.cancel = _data["Cancel"];
            this.create = _data["Create"];
            this.delete = _data["Delete"];
            this.deleteItemConfirm = _data["DeleteItemConfirm"];
            this.description = _data["Description"];
            this.edit = _data["Edit"];
            this.loadMore = _data["LoadMore"];
            this.name = _data["Name"];
            this.no = _data["No"];
            this.save = _data["Save"];
            this.searchPlaceholder = _data["SearchPlaceholder"];
            this.shownItems = _data["ShownItems"];
            this.m_StrMasterPassword = _data["M_StrMasterPassword"];
            this.yes = _data["Yes"];
        }
    }

    static fromJS(data: any): UIInfo {
        data = typeof data === 'object' ? data : {};
        let result = new UIInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["AddItem"] = this.addItem;
        data["Cancel"] = this.cancel;
        data["Create"] = this.create;
        data["Delete"] = this.delete;
        data["DeleteItemConfirm"] = this.deleteItemConfirm;
        data["Description"] = this.description;
        data["Edit"] = this.edit;
        data["LoadMore"] = this.loadMore;
        data["Name"] = this.name;
        data["No"] = this.no;
        data["Save"] = this.save;
        data["SearchPlaceholder"] = this.searchPlaceholder;
        data["ShownItems"] = this.shownItems;
        data["M_StrMasterPassword"] = this.m_StrMasterPassword;
        data["Yes"] = this.yes;
        return data;
    }
}

/** Localized strings for the UI resources. */
export interface IUIInfo {
    /** Gets or sets the AddItem localized text. */
    addItem?: string | undefined;
    /** Gets or sets the Cancel localized text. */
    cancel?: string | undefined;
    /** Gets or sets the Create localized text. */
    create?: string | undefined;
    /** Gets or sets the Delete localized text. */
    delete?: string | undefined;
    /** Gets or sets the DeleteItemConfirm localized text. */
    deleteItemConfirm?: string | undefined;
    /** Gets or sets the Description localized text. */
    description?: string | undefined;
    /** Gets or sets the Edit localized text. */
    edit?: string | undefined;
    /** Gets or sets the LoadMore localized text. */
    loadMore?: string | undefined;
    /** Gets or sets the Name localized text. */
    name?: string | undefined;
    /** Gets or sets the No localized text. */
    no?: string | undefined;
    /** Gets or sets the Save localized text. */
    save?: string | undefined;
    /** Gets or sets the SearchPlaceholder localized text. */
    searchPlaceholder?: string | undefined;
    /** Gets or sets the ShownItems localized text. */
    shownItems?: string | undefined;
    /** Gets or sets the M_StrMasterPassword localized text. */
    m_StrMasterPassword?: string | undefined;
    /** Gets or sets the Yes localized text. */
    yes?: string | undefined;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}

export interface ConfigureRequest {
  moduleId: number;
}