export interface ClientData extends ClientIdentifier {
    name: string
}

export type ClientIdentifier = {
    host: string
}

export class Client {
    private readonly data: ClientData

    public constructor(data: ClientData) {
        this.data = data;
    }

    public get host() {
        return this.data.host
    }

    public get name() {
        return this.data.name
    }

    public clone() {
        return new Client(this.data)
    }

    public toJSON() {
        return {
            ...this.data
        }
    }
}
