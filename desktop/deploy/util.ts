import * as path from "path";
import {promises as fs} from "fs"
import {exec as _exec, execFile as _execFile, ExecOptions} from "child_process";
import AdmZip from "adm-zip"

export async function exec(command: string, option?: ExecOptions) {
    return new Promise((resolve, reject) => {
        _exec(command, option, ((error, stdout) => {
            if (error) reject(error);
            resolve(stdout);
        }))
    })
}

export async function execFile(command: string, option?: ExecOptions) {
    return new Promise((resolve, reject) => {
        _execFile(command, option, ((error, stdout) => {
            if (error) reject(error);
            resolve(stdout);
        }))
    })
}


export async function zip(root: string, output: string, ...nodes: string[]) {
    const zip = new AdmZip();
    await Promise.all(nodes.map(async node => {
        if ((await fs.lstat(node)).isDirectory()) {
            zip.addLocalFolder(node, path.relative(root, node))
        } else {
            zip.addLocalFile(node, path.relative(root, node))
        }
    }))
    return new Promise<void>(resolve => {
        zip.writeZip(output, () => resolve());
    })
}

export let root = path.resolve(__dirname, "..");
export let lib = path.resolve(root, "lib");
