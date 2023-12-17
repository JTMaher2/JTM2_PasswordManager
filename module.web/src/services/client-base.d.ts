export declare class ClientBase {
    private sf;
    private moduleId;
    constructor(configuration: ConfigureRequest);
    protected getBaseUrl(_defaultUrl: string, baseUrl?: string): string;
    protected transformOptions(options: RequestInit): Promise<RequestInit>;
}
export interface ConfigureRequest {
    moduleId: number;
}
