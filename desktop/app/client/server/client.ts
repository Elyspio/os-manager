import * as express from "express"
import * as  bodyParser from "body-parser";
import {Request} from "./types";
import {Operation} from "../core/hardware";
import * as cors from "cors"


export function createServer() {
    const app = express();
    app.use(bodyParser.urlencoded({extended: true}));
    app.use(bodyParser.json())
    app.use(cors());


    app.post("/config", async (req: Request.HardwarePowerActions, res) => {
        const functions = {
            "shutdown": Operation.shutdown,
            "sleep": Operation.sleep,
            "reboot": Operation.reboot,
            "lock": Operation.lock
        }
        res.send("");
        return functions[req.body.type]();
    })

    app.get("/ping", (req, res) => {
        res.json({time: Date.now()})
    })


    return app;
}
