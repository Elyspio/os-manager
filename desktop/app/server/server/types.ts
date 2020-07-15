import {Request} from "express";

export namespace Request {
    export interface Register extends Request {
        body: {
            id: string,
            ips: string[],
            name: string
        }
    }

    export interface ServerHardwarePowerActions extends Request {
        params: {
            target: string
            command: "shutdown" | "sleep" | "hibernate"
        }
    }
}
