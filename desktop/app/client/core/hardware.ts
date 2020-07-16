import * as os from "os";
import {exec} from "../../common/util/hardware";

export const getIps = (): (string | undefined)[] => {

    const raw = os.networkInterfaces()
    const interfaces = Object.keys(raw);

    return interfaces
        .filter(int => !["VMware", "Loopback", "WSL"].some(search => int.includes(search)))
        .map(int => raw[int].find(i => i.family === "IPv4")?.address).filter(ip => ip !== undefined)
}


function unSupportedPlatform() {
    throw  new Error(`Unsupported OS ${os.platform()}`)
}

export namespace Operation {

    const commands = {
        sleep: {
            win32: "rundll32.exe powrprof.dll,SetSuspendState 0,1,0",
            linux: "systemctl suspend"
        },
        reboot: {
            win32: "shutdown -r -t 0",
            linux: "/sbin/shutdown -r now"
        },
        shutdown: {
            win32: "shutdown -s -t 0",
            linux: "/sbin/shutdown now"
        },
        lock: {
            win32: "rundll32.exe user32.dll,LockWorkStation",
            linux: "gnome-screensaver-command --lock"
        }
    }

    export function sleep() {
        let command = commands.sleep[os.platform()]
        if (command) {
            return exec(command)
        }
        unSupportedPlatform();
    }

    export function shutdown() {
        let command = commands.shutdown[os.platform()]
        if (command) {
            return exec(command)
        }
        unSupportedPlatform();
    }

    export function reboot() {
        let command = commands.reboot[os.platform()]
        if (command) {
            return exec(command)
        }
        unSupportedPlatform();
    }

    export function lock() {
        let command = commands.lock[os.platform()]
        if (command) {
            return exec(command)
        }
        unSupportedPlatform();
    }
}
