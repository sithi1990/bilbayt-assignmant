﻿/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.2.2.0 (NJsonSchema v10.1.4.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

export interface IAccountsClient {
    register(requestCommand: RegisterUserCommand): Promise<ResponseMetadata>;
    createAndGetAccessToken(requestCommand: CreateTokenCommand): Promise<TokenResponse>;
    getUserInfo(): Promise<UserInfoResponse>;
}

export class AccountsClient implements IAccountsClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : <any>window;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    register(requestCommand: RegisterUserCommand): Promise<ResponseMetadata> {
        let url_ = this.baseUrl + "/api/Accounts";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(requestCommand);

        let options_ = <RequestInit>{
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processRegister(_response);
        });
    }

    protected processRegister(response: Response): Promise<ResponseMetadata> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = ResponseMetadata.fromJS(resultData200);
            return result200;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ResponseMetadata.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<ResponseMetadata>(<any>null);
    }

    createAndGetAccessToken(requestCommand: CreateTokenCommand): Promise<TokenResponse> {
        let url_ = this.baseUrl + "/api/Accounts/token";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(requestCommand);

        let options_ = <RequestInit>{
            body: content_,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processCreateAndGetAccessToken(_response);
        });
    }

    protected processCreateAndGetAccessToken(response: Response): Promise<TokenResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = TokenResponse.fromJS(resultData200);
            return result200;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ResponseMetadata.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<TokenResponse>(<any>null);
    }

    getUserInfo(): Promise<UserInfoResponse> {
        let url_ = this.baseUrl + "/api/Accounts/user-info";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGetUserInfo(_response);
        });
    }

    protected processGetUserInfo(response: Response): Promise<UserInfoResponse> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = UserInfoResponse.fromJS(resultData200);
            return result200;
            });
        } else if (status === 400) {
            return response.text().then((_responseText) => {
            let result400: any = null;
            let resultData400 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result400 = ResponseMetadata.fromJS(resultData400);
            return throwException("A server side error occurred.", status, _responseText, _headers, result400);
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<UserInfoResponse>(<any>null);
    }
}

export interface IWeatherForecastClient {
    get(): Promise<WeatherForecast[]>;
}

export class WeatherForecastClient implements IWeatherForecastClient {
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.http = http ? http : <any>window;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    get(): Promise<WeatherForecast[]> {
        let url_ = this.baseUrl + "/WeatherForecast";
        url_ = url_.replace(/[?&]$/, "");

        let options_ = <RequestInit>{
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        };

        return this.http.fetch(url_, options_).then((_response: Response) => {
            return this.processGet(_response);
        });
    }

    protected processGet(response: Response): Promise<WeatherForecast[]> {
        const status = response.status;
        let _headers: any = {}; if (response.headers && response.headers.forEach) { response.headers.forEach((v: any, k: any) => _headers[k] = v); };
        if (status === 200) {
            return response.text().then((_responseText) => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(WeatherForecast.fromJS(item));
            }
            return result200;
            });
        } else if (status !== 200 && status !== 204) {
            return response.text().then((_responseText) => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        return Promise.resolve<WeatherForecast[]>(<any>null);
    }
}

export class ResponseMetadata implements IResponseMetadata {
    success!: boolean;
    errors?: ProblemDetails[] | undefined;

    constructor(data?: IResponseMetadata) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.success = _data["success"];
            if (Array.isArray(_data["errors"])) {
                this.errors = [] as any;
                for (let item of _data["errors"])
                    this.errors!.push(ProblemDetails.fromJS(item));
            }
        }
    }

    static fromJS(data: any): ResponseMetadata {
        data = typeof data === 'object' ? data : {};
        let result = new ResponseMetadata();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["success"] = this.success;
        if (Array.isArray(this.errors)) {
            data["errors"] = [];
            for (let item of this.errors)
                data["errors"].push(item.toJSON());
        }
        return data; 
    }
}

export interface IResponseMetadata {
    success: boolean;
    errors?: ProblemDetails[] | undefined;
}

export class ProblemDetails implements IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;
    extensions?: { [key: string]: any; } | undefined;

    constructor(data?: IProblemDetails) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.type = _data["type"];
            this.title = _data["title"];
            this.status = _data["status"];
            this.detail = _data["detail"];
            this.instance = _data["instance"];
            if (_data["extensions"]) {
                this.extensions = {} as any;
                for (let key in _data["extensions"]) {
                    if (_data["extensions"].hasOwnProperty(key))
                        this.extensions![key] = _data["extensions"][key];
                }
            }
        }
    }

    static fromJS(data: any): ProblemDetails {
        data = typeof data === 'object' ? data : {};
        let result = new ProblemDetails();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["type"] = this.type;
        data["title"] = this.title;
        data["status"] = this.status;
        data["detail"] = this.detail;
        data["instance"] = this.instance;
        if (this.extensions) {
            data["extensions"] = {};
            for (let key in this.extensions) {
                if (this.extensions.hasOwnProperty(key))
                    data["extensions"][key] = this.extensions[key];
            }
        }
        return data; 
    }
}

export interface IProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;
    extensions?: { [key: string]: any; } | undefined;
}

export class RegisterUserCommand implements IRegisterUserCommand {
    userName?: string | undefined;
    password?: string | undefined;
    fullName?: string | undefined;

    constructor(data?: IRegisterUserCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.userName = _data["userName"];
            this.password = _data["password"];
            this.fullName = _data["fullName"];
        }
    }

    static fromJS(data: any): RegisterUserCommand {
        data = typeof data === 'object' ? data : {};
        let result = new RegisterUserCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userName"] = this.userName;
        data["password"] = this.password;
        data["fullName"] = this.fullName;
        return data; 
    }
}

export interface IRegisterUserCommand {
    userName?: string | undefined;
    password?: string | undefined;
    fullName?: string | undefined;
}

export class TokenResponse extends ResponseMetadata implements ITokenResponse {
    accessToken?: string | undefined;
    expiration!: Date;

    constructor(data?: ITokenResponse) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.accessToken = _data["accessToken"];
            this.expiration = _data["expiration"] ? new Date(_data["expiration"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): TokenResponse {
        data = typeof data === 'object' ? data : {};
        let result = new TokenResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["accessToken"] = this.accessToken;
        data["expiration"] = this.expiration ? this.expiration.toISOString() : <any>undefined;
        super.toJSON(data);
        return data; 
    }
}

export interface ITokenResponse extends IResponseMetadata {
    accessToken?: string | undefined;
    expiration: Date;
}

export class CreateTokenCommand implements ICreateTokenCommand {
    userName?: string | undefined;
    password?: string | undefined;

    constructor(data?: ICreateTokenCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.userName = _data["userName"];
            this.password = _data["password"];
        }
    }

    static fromJS(data: any): CreateTokenCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CreateTokenCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userName"] = this.userName;
        data["password"] = this.password;
        return data; 
    }
}

export interface ICreateTokenCommand {
    userName?: string | undefined;
    password?: string | undefined;
}

export class UserInfoResponse extends ResponseMetadata implements IUserInfoResponse {
    userInfo?: UserInfoResult | undefined;

    constructor(data?: IUserInfoResponse) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.userInfo = _data["userInfo"] ? UserInfoResult.fromJS(_data["userInfo"]) : <any>undefined;
        }
    }

    static fromJS(data: any): UserInfoResponse {
        data = typeof data === 'object' ? data : {};
        let result = new UserInfoResponse();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userInfo"] = this.userInfo ? this.userInfo.toJSON() : <any>undefined;
        super.toJSON(data);
        return data; 
    }
}

export interface IUserInfoResponse extends IResponseMetadata {
    userInfo?: UserInfoResult | undefined;
}

export class UserInfoResult implements IUserInfoResult {
    userId?: string | undefined;
    userName?: string | undefined;
    fullName?: string | undefined;

    constructor(data?: IUserInfoResult) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.userId = _data["userId"];
            this.userName = _data["userName"];
            this.fullName = _data["fullName"];
        }
    }

    static fromJS(data: any): UserInfoResult {
        data = typeof data === 'object' ? data : {};
        let result = new UserInfoResult();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["userId"] = this.userId;
        data["userName"] = this.userName;
        data["fullName"] = this.fullName;
        return data; 
    }
}

export interface IUserInfoResult {
    userId?: string | undefined;
    userName?: string | undefined;
    fullName?: string | undefined;
}

export class WeatherForecast implements IWeatherForecast {
    date!: Date;
    temperatureC!: number;
    temperatureF!: number;
    summary?: string | undefined;

    constructor(data?: IWeatherForecast) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.date = _data["date"] ? new Date(_data["date"].toString()) : <any>undefined;
            this.temperatureC = _data["temperatureC"];
            this.temperatureF = _data["temperatureF"];
            this.summary = _data["summary"];
        }
    }

    static fromJS(data: any): WeatherForecast {
        data = typeof data === 'object' ? data : {};
        let result = new WeatherForecast();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["date"] = this.date ? this.date.toISOString() : <any>undefined;
        data["temperatureC"] = this.temperatureC;
        data["temperatureF"] = this.temperatureF;
        data["summary"] = this.summary;
        return data; 
    }
}

export interface IWeatherForecast {
    date: Date;
    temperatureC: number;
    temperatureF: number;
    summary?: string | undefined;
}

export class SwaggerException extends Error {
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

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new SwaggerException(message, status, response, headers, null);
}