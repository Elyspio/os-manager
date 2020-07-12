import * as commonCustom from "../../common/config/custom.json"
import * as process from "process";
import {getCustom} from "../../common/config/const";

const isDev = process.env.NODE_ENV !== "production"

const config = getCustom(__dirname)
console.log("config", config);


export const expressPort = commonCustom.ports.client;
export let serverURL = isDev ? config.serverUrl.development : config.serverUrl.production
export const name = config.name;
