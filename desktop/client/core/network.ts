import * as os from "os"

export const getIps = (): string[] => {

    const raw = os.networkInterfaces()
    const interfaces = Object.keys(raw);

    return interfaces
        .filter(int => !["VMware", "Loopback", "WSL"].some(search => int.includes(search)))
        .map(int => raw[int].find(i => i.family === "IPv4")?.address).filter(ip => ip !== undefined)
}
