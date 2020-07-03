import * as express from "express"
import {ArgumentParser} from "argparse"
import {logger} from "../util/logger";
import {expressPort} from "../config/const";
import * as  bodyParser from "body-parser";


if(require.main === module) {

    logger.debug("Starting ")

    const parser = new ArgumentParser();
    parser.addArgument("--port", {type: "int", defaultValue: expressPort})
    const {port} = parser.parseArgs();

    const app = express();
    app.use(bodyParser.urlencoded({extended: true}));
    app.use(bodyParser.json())

    app.post("/register", ((req, res) => {
        logger.info("register ips: ",  req.body.ips)
        res.send("");
    }))

    app.listen(port, async () => {
        logger.info(`express client is listening on port ${port}`)
    })
}







