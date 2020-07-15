import {logger} from "../util/logger";
import {Request} from "./types";
import {ClientsManager} from "../core/clients-manager";
import {default as axios} from "axios"
import * as path from "path";
import {socketEvents} from "../config/socket";
import {middlewares} from "./middleware";
import {Express} from "express";
import * as net from "net";
import {Server as ServerIO} from "socket.io"

export function createServer() {
    const express = require('express');
    const app: Express = express();
    const server: net.Server = require("http").Server(app);
    const socketIoServer: ServerIO = require("socket.io")(server);

    app.use(...middlewares);


    app.post("/register", ((req: Request.Register, res) => {
        logger.info("register", {ips: req.body})
        const changed = ClientsManager.instance.register({id: req.body.id, host: req.body.ips[0], name: req.body.name})
        if (changed) {
            socketIoServer.sockets.emit(socketEvents.updateAll, ClientsManager.instance.clients.map(c => c.id))
        }
        res.send("");
    }))

    app.get("/computers", (req, res) => {
        res.json(ClientsManager.instance.toJSON());
    })

    app.get("/computers/:target", (req, res) => {
        const target = ClientsManager.get(req.params.target);
        res.send(target);
    })


    app.post("/computers/:target/:command", async (req: Request.ServerHardwarePowerActions, res) => {
        logger.info("Query", {method: "POST", "param": req.params})
        const targetUrl = ClientsManager.get(req.params.target).host
        await axios.post(`http://${targetUrl}/config`, {
            type: req.params.command
        })
        res.send("");
    })


    app.use("/", express.static(path.resolve(__dirname, "..", "front", "build")))


    socketIoServer.on("connection", (socket) => {

        const names = ClientsManager.instance.clients.map(c => c.id);
        logger.info("client ws connection", {names})
        socket.emit(socketEvents.updateAll, names)
    });


    setInterval(async () => {

        let clients = ClientsManager.instance.clients;
        const promises = clients.map(client => axios.get(`http://${client.host}/ping`, {timeout: 250}))

        const results = await Promise.allSettled(promises)

        results.forEach((result, index) => {
            if (result.status === "rejected") {
                let client = clients[index];
                logger.info(`Client ${client.id} is not accessible, it will be removed`)
                ClientsManager.instance.remove(client.id)
                socketIoServer.sockets.emit(socketEvents.remove, client.id)
            }
        })
    }, 2000)

    return server;

}









