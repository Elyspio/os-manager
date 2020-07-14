import * as path from "path";
import * as  fs from "fs-extra"
import * as os from "os";
import {exec, execFile, lib, root, zip} from "./util";

async function build() {
    if ((await fs.readdir(root)).includes("lib")) {
        await fs.remove(lib)
    }
    await fs.ensureDir(lib)

    const tscPath = path.resolve(root, "./node_modules/.bin/tsc" + (os.platform() === "win32" ? ".cmd" : ""));

    await Promise.all([
        execFile(tscPath, {cwd: root}),
        exec("yarn build", {cwd: path.resolve(root, "app", "server", "front"), env: {NODE_ENV: "production"}})
    ])


    await fs.copy(path.join(root, "app"), lib, {
        filter: src => {
            let pathToFront = path.resolve(root, "app", "server", "front");

            if (src === pathToFront) return true;
            if (src.includes(pathToFront) && !src.includes(path.resolve(pathToFront, "build"))) return false;

            if (src.split(path.sep).slice(-1)[0].includes(".ts")) return false;

            return true;
        }
    })

    const pkg = JSON.parse((await fs.readFile(path.resolve(root, "package.json"))).toString())
    const newPkg = {
        "name": pkg.name,
        "version": pkg.version,
        "license": "MIT",
        dependencies: pkg.dependencies,
        scripts: {
            client: "node client/index.js",
            server: "node server/index.js",
        }
    }
    await fs.writeFile(path.resolve(lib, "package.json"), JSON.stringify(newPkg))
    await exec("npm install", {cwd: lib})
    await fs.copyFile("C:\\Program Files\\nodejs\\node.exe", path.resolve(lib, "node.exe"))
    await fs.copy(path.resolve(__dirname, "register"), path.resolve(lib, "register"))
}


async function main() {
    await build();
    await zip(lib, path.join(lib, "lib.zip"), lib)
}

if (require.main === module) {
    main();
}

