import {Request} from "express";

export namespace Request {
    export interface Register extends Request {
        body: {
            name: string,
            ips: string[]
        }
    }

    export interface ServerHardwarePowerActions extends Request {
        params: {
            target: string
            command: "shutdown" | "sleep" | "hibernate"
        }
    }
}
