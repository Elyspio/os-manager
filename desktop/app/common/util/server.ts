import {Logger} from "winston";
import express, {Express} from "express";
import * as bodyParser from "body-parser";
import cors from "cors";
import net from "net";
import {Server as ServerIO} from "socket.io";
import {middlewares} from "../../server/server/middleware";

const defaultOptions: ServerOption = {
    cors: true,
    websocket: false,
};

type ServerOption = { cors?: boolean, logger?: Logger, websocket?: boolean };

export function createServer(options: ServerOption = defaultOptions) {
    const app: Express = express();
    app.use(bodyParser.urlencoded({extended: true}));
    app.use(bodyParser.json())

    if (options.cors) {
        app.use(cors());
    }


    if (options.logger) {
        app.use((req, res, next) => {
            options.logger?.log("request", `Got a request`, {
                method: req.method,
                url: req.originalUrl,
                from: req.hostname,
                data: req.method === "get" ? req.params : req.body
            })
            next();
        })

    }

    if (options.websocket) {
        const server: net.Server = require("http").Server(app);
        const socketIoServer: ServerIO = require("socket.io")(server);

        app.use(...middlewares);
        return {
            http: server,
            socket: socketIoServer,
            express: app
        }
    } else {
        return {express: app};
    }


}
