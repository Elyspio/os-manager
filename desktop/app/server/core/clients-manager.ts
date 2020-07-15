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

    public get clients(): Client[] {
        const clients = [];
        this.internal.clients.forEach((client) => {
            clients.push(client.clone())
        })
        return clients;
    }

    public static get(id: string) {
        return Array.from(this._instance.internal.clients.values()).find(client => client.id === id)
    }

    /**
     * @return true if the number of clients changed
     * @param client
     */
    public register(client: ClientData): boolean {
        const originLength = this.internal.clients.size;
        this.internal.clients.set(client.id, new Client(client))
        return originLength !== this.internal.clients.size;
    }

    public toJSON() {
        const obj = {
            clients: []
        };

        this.internal.clients.forEach((val, key) => {
            obj.clients.push(val.toJSON());
        })

        return obj;

    }

    public remove(id: string) {
        this.internal.clients.delete(id)
    }
}

