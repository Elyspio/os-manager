import {default as axios} from "axios"
import {serverURL} from "../config/const"
import {getIps} from "./network";

export async function register() {
    console.log(getIps())
    return axios.post(`${serverURL}/register`,{ips: getIps()})
}
