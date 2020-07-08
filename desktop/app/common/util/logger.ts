import {logFolder} from "../config/const";
import * as path from "path";
import * as process from "process";

const winston = require('winston');


const dateFormat = () => {
    const date = new Date();
    const addZero = (number: string, count: number = 2) => {
        for (let i = 0; i < count - number.length; i++) {
            number = '0' + number;
        }
        return number;
    }
    const day = addZero(date.getDate().toString());
    const month = addZero(date.getMonth().toString())
    const year = addZero(date.getFullYear().toString());
    const h = addZero(date.getHours().toString())
    const mn = addZero(date.getMinutes().toString());
    const s = addZero(date.getSeconds().toString())

    return `${day}/${month}/${year} -- ${h}:${mn}:${s}`
}

const getLogFile = (...node: string[]) => path.join(logFolder, ...node)

const getFormat = (colorize: boolean) => {
    const formats= [
        winston.format.timestamp({
            format: dateFormat
        }),
        winston.format.prettyPrint(),
    ]

    if (colorize) {
        formats.push(winston.format.colorize({
            all: true, colors: {
                info: "blue",
                error: "red",
                warning: "orange",
                debug: "yellow"
            }
        }));
    }

    return  winston.format.combine(...formats);
};

function getTransports(service: string): Transport[] {
    const transports: Transport[] = [];
    const colorFormat = getFormat(true);
    const noColorFormat =getFormat(false);
    transports.push(
        new winston.transports.File({
            filename: getLogFile(service, 'error.color.log'), level: 'error',
            format: colorFormat
        }),
        new winston.transports.File({
            filename: getLogFile(service, 'combined.color.log'),
            format: colorFormat
        }),
        new winston.transports.File({
            filename: getLogFile(service, 'error.log'), level: 'error',
            format: noColorFormat
        }),
        new winston.transports.File({
            filename: getLogFile(service, 'combined.log'),
            format: noColorFormat
        }),
    )

    if (process.env.NODE_ENV !== "production") {
        transports.push(
            new winston.transports.Console({})
        )
    }

    return transports;
}


export function initLogger(service: string) {


    return winston.createLogger({
        defaultMeta: {service: `@android-windows-link/desktop-${service}`},
        transports: getTransports(service),
    });

}

