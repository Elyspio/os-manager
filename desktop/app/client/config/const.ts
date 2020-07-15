import * as commonCustom from "../../common/config/custom.json"
import * as process from "process";
import {getCustom} from "../../common/config/const";
import * as os from "os";

const isDev = process.env.NODE_ENV !== "production"

const config = getCustom(__dirname, false)

export const expressPort = commonCustom.ports.client;
export let serverURL = isDev ? config.serverUrl.development : config.serverUrl.production
export const id = os.hostname();
export const name = config.name
