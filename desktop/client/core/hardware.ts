import * as os from "os";
import {exec} from "../../common/util/hardware";

export const getIps = (): string[] => {

    const raw = os.networkInterfaces()
    const interfaces = Object.keys(raw);

    return interfaces
        .filter(int => !["VMware", "Loopback", "WSL"].some(search => int.includes(search)))
        .map(int => raw[int].find(i => i.family === "IPv4")?.address).filter(ip => ip !== undefined)
}


const isWindows = os.platform() === "win32"
const isLinux = os.platform() === "linux"


export namespace Operation {

    export function sleep() {

        let command;
        if (isWindows) {
            command = "rundll32.exe powrprof.dll,SetSuspendState 0,1,0"
        }
        if (isLinux) {
            command = "systemctl suspend";
        }
        return exec(command);

    }

    export function shutdown() {
        let command;
        if (isWindows) {
            command = "shutdown -s -t 0"
        }
        if (isLinux) {
            command = "/sbin/shutdown now";
        }
        return exec(command);
    }

    export function reboot() {
        let command;
        if (isWindows) {
            command = "shutdown -r -t 0"
        }
        if (isLinux) {
            command = "/sbin/shutdown -r now";
        }
        return exec(command);
    }
}
