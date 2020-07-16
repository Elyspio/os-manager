import {Request} from "./types";
import {Operation} from "../core/hardware";
import {createServer} from "../../common/util/server";
import {logger} from "../util/logger";

export function initServer() {
    const {express: app} = createServer({cors: true, logger})

    app.post("/config", async (req: Request.HardwarePowerActions, res) => {
        const functions = {
            "shutdown": Operation.shutdown,
            "sleep": Operation.sleep,
            "reboot": Operation.reboot,
            "lock": Operation.lock
        }
        functions[req.body.type]();
        return res.send("");
    })

    app.get("/ping", (req, res) => {
        res.json({time: Date.now()})
    })


    return app;
}
