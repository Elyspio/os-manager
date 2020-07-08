import {default as axios} from "axios"
import {expressPort, name, serverURL} from "../config/const"
import {getIps} from "./hardware";

export async function register() {
    return axios.post(`${serverURL}/register`, {name, ips: getIps().map(ip => `${ip}:${expressPort}`)})
}
