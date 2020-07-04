import * as express from "express"
import {Express} from "express"
import {logger} from "../util/logger";
import * as  bodyParser from "body-parser";
import {Request} from "./types";
import {ClientsManager} from "../core/clients-manager";
import {default as axios} from "axios"
import * as cors from "cors"

export function createServer(): Express {
    const app = express();
    app.use(bodyParser.urlencoded({extended: true}));
    app.use(bodyParser.json())
    app.use(cors())

    app.post("/register", ((req: Request.Register, res) => {
        logger.info("register ips: ", {client: req.body})
        ClientsManager.instance.register({name: req.body.name, host: req.body.ips[0]})
        res.send("");
    }))

    app.get("/", (req, res) => {
        res.json(ClientsManager.instance.toJSON());
    })
    app.get("/:target/:command", async (req: Request.ServerHardwarePowerActions, res) => {
        const targetUrl = ClientsManager.get(req.params.target).host
        await axios.post(`http://${targetUrl}/config`, {
            type: req.params.command
        })
        res.send("");
    })


    return app;

}









