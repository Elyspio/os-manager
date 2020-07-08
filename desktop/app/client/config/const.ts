import * as  custom from "./custom.json"
import * as commonCustom from "../../common/config/custom.json"
import * as process from "process";

const isDev = process.env.NODE_ENV !== "production"

export const expressPort = commonCustom.ports.client;
export let serverURL = isDev ? custom.serverUrl.development : custom.serverUrl.production
export const name = custom.name;
