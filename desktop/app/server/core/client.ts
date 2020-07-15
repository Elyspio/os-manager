import {Computer} from "../../../../mobile/app/data/computer-manager/reducer";

export interface ClientData extends Computer {
}

/**
 * Id of the client
 */
export type ClientIdentifier = string

export class Client {
    private readonly data: ClientData

    public constructor(data: ClientData) {
        this.data = data;
    }

    public get host() {
        return this.data.host;
    }

    public get name() {
        return this.data.name;
    }

    public get id() {
        return this.data.id;
    }

    public clone() {
        return new Client(this.data);
    }

    public toJSON() {
        return {
            ...this.data
        }
    }
}
