import {logger} from "./util/logger";
import {ArgumentParser} from "argparse";
import {expressPort} from "./config/const";
import {createServer} from "./server/server";

if (require.main === module) {

    logger.debug("Starting ")

    const parser = new ArgumentParser();
    parser.addArgument("--port", {type: "int", defaultValue: expressPort})
    const {port} = parser.parseArgs();

    const server = createServer();

    server.listen(port, async () => {
        logger.info(`express server is listening on port ${port}`)
    })
}
