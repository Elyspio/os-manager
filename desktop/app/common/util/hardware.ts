import {exec as _exec} from "child_process"

export async function exec(command: string) {
    return new Promise((resolve, reject) => {
        _exec(command, ((error, stdout) => {
            if (error) reject(error);
            resolve(stdout);
        }))
    })
}
