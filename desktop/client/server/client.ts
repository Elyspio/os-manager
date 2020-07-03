import * as express from "express"
import {ArgumentParser} from "argparse"
import {logger} from "../util/logger";
import {expressPort} from "../config/const";
import {register} from "../core/http";

import * as  bodyParser from "body-parser";

if (require.main === module) {

    const parser = new ArgumentParser();
    parser.addArgument("--port", {type: "int", defaultValue: expressPort})
    const {port} = parser.parseArgs();

    const app = express();
    app.use(bodyParser.urlencoded({extended: true}));
    app.use(bodyParser.json())

    app.listen(port, () => {
        logger.info(`express server is listening on port ${port}`)
        register();
    })


}







