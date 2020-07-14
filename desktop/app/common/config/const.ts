import * as path from "path"
import * as os from "os";
import * as fs from "fs";
import * as merge from "lodash.merge"
import * as process from "process";

const config = getCustom(__dirname)

export function getCustom(dir: string, onlyProduction = true) {
    const baseOption = require(path.resolve(dir, "custom.json"))
    const pathToCustom = path.resolve(dir, "env", `${os.hostname()}.json`)

    if (fs.existsSync(pathToCustom) && (!onlyProduction || process.env.NODE_ENV?.includes("production"))) {
        console.log("merge");
        return merge(baseOption, require(pathToCustom));
    }
    return baseOption;
}

console.log("config", config);

export const logFolder = config.logFolder;
export const ports = config.ports
