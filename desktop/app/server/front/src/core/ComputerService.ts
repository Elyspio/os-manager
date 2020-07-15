import {conf} from "../config/conf";
import {Computer, ComputerIdentifier} from "../../../../../../mobile/app/data/computer-manager/reducer";
import axios from "axios"

export class ComputerService {
    private readonly base = `${conf.endpoints.api}/computers/`;

    private proxy = axios.create({
        baseURL: this.base, headers: {
            "Content-Type": "application/json",
        }
    })

    static _instance: ComputerService = new ComputerService();

    public static get instance() {
        return ComputerService._instance;
    }

    public async reboot(id: ComputerIdentifier) {
        return this.call(`${this.baseWithId(id)}/reboot`, {method: "POST"}).then(raw => raw.data)
    }

    public async sleep(id: ComputerIdentifier) {
        return this.call(`${this.baseWithId(id)}/sleep`, {method: "POST"}).then(raw => raw.data)
    }

    public async lock(id: ComputerIdentifier) {
        return this.call(`${this.baseWithId(id)}/lock`, {method: "POST"}).then(raw => raw.data)
    }

    public async shutdown(id: ComputerIdentifier) {
        return this.call(`${this.baseWithId(id)}/shutdown`, {method: "POST"}).then(raw => raw.data)
    }

    public async get(id: ComputerIdentifier): Promise<Computer> {
        return this.call(this.baseWithId(id)).then(raw => raw.data)
    }

    private baseWithId = (id: ComputerIdentifier) => `${id.id}`;

    private call = async (url, config?: { method?: "GET" | "POST"; ignoreCallLimit?: boolean }, data?: object,) => {

        const method = config?.method ?? "GET";

        let params: object | undefined = undefined;

        if (method === "GET") {
            params = data;
            data = undefined;
        }

        return this.proxy(url, {
            method: method,
            data,
            params
        });
    };
}
