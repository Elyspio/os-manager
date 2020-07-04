import * as  custom from "./custom.json"

const isDev = process.env.NODE_ENV !== "production"


export let serverURL = isDev ? custom.serverUrl.development : custom.serverUrl.production
export const expressPort = custom.serverPort;
export const name = custom.name;
