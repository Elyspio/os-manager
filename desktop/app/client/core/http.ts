import {default as axios} from "axios"
import {expressPort, id, name, serverURL} from "../config/const"
import {getIps} from "./hardware";
import {logger} from "../util/logger";

export async function register() {
    logger.info("register " + serverURL)
    return axios.post(`${serverURL}/register`, {id, name, ips: getIps().map(ip => `${ip}:${expressPort}`)})
}
