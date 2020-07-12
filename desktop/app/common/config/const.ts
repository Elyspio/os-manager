import * as path from "path"
import * as os from "os";
import * as fs from "fs";
import * as merge from "lodash.merge"

const config = getCustom(__dirname)

export function getCustom(dir: string) {
    const baseOption = require(path.join(dir, "custom.json"))
    const pathToCustom = path.join(dir, "env", `${os.hostname()}.json`)

    console.log("Try to use custom file", pathToCustom, fs.existsSync(pathToCustom));

    if (fs.existsSync(pathToCustom)) {
        return merge(baseOption, require(pathToCustom));
    }
}

console.log("config", config);

export const logFolder = config.logFolder ?? path.resolve(__dirname, "..", "..", "..", "logs");
