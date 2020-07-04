import {serverURL} from "../config/const.json"
import {default as axios} from "axios"
import {Computer} from "../data/computer-manager/reducer";

export function fetchServer(resource: string, method: "GET" | "POST", ...params: any[]) {

    if (method === "GET") {
        return axios.get(`${serverURL}/${resource}`, {
            params: params
        })
    }
    if (method === "POST") {
        return axios.post(`${serverURL}/${resource}`, {
            body: params
        })
    }

    throw  new Error("Request method " + method);
}

export namespace HardwareChange {
    export function shutdown(computer: Computer) {
        return fetchServer(`${computer.name}/shutdown`, "POST")
    }
    export function sleep(computer: Computer) {
        return fetchServer(`${computer.name}/sleep`, "POST")
    }
    export function reboot(computer: Computer) {
        return fetchServer(`${computer.name}/reboot`, "POST")
    }
}
