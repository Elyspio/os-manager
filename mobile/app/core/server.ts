import {serverURL} from "../config/const.json"
import {default as axios} from "axios"

export function fetchServer(resource: string, method: "GET" | "POST", params?: any[]) {

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
