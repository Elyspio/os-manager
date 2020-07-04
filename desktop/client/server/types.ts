import {Request} from "express";

export namespace Request {
    export interface HardwarePowerActions extends Request {
        body: {
            type: "shutdown" | "sleep" | "hibernate"
        }
    }

    export interface ChangeCpuSpeed extends Request {
        body: {
            allowOverclocking: boolean
        }
    }
}
