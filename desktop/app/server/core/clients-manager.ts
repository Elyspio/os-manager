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
        const clients = new Map<ClientIdentifier, Client>();
        this.internal.clients.forEach((client, id) => {
            clients.set(id, client.clone())
        })
        return clients;
    }

    public static get(name: string) {
        return Array.from(this.instance.clients.values()).find(client => client.name === name)
    }

    public register(client: ClientData) {
        this.internal.clients.set({host: client.host}, new Client(client))
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

