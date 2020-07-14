import {Client, ClientData, ClientIdentifier} from "./client";

export class ClientsManager {
    private internal: {
        clients: Map<ClientIdentifier, Client>;
    }

    private constructor() {
        this.internal = {
            clients: new Map<ClientIdentifier, Client>()
        }
    }

    private static _instance = new ClientsManager();

    public static get instance() {
        return this._instance;
    }

    public get clients() {
        const clients = [];
        this.internal.clients.forEach((client) => {
            clients.push(client.clone())
        })
        return clients;
    }

    public static get(name: string) {
        return Array.from(this.instance.clients.values()).find(client => client.name === name)
    }

    /**
     * @return true if the number of clients changed
     * @param client
     */
    public register(client: ClientData): boolean {
        const originLength = this.internal.clients.size;
        this.internal.clients.set(client.host, new Client(client))
        return originLength !== this.internal.clients.size;
    }

    public toJSON() {
        const obj = {
            clients: []
        };

        this.internal.clients.forEach((val, key) => {
            obj.clients.push(val);
        })

        return obj;

    }

}

