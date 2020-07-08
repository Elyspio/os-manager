import * as path from "path"
import * as custom from "./custom.json"

export const logFolder = custom.logFolder ?? path.resolve(__dirname, "..", "..", "..", "logs");
