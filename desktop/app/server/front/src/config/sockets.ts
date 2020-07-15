import {conf} from "./conf";

export const socketEvents = {
    updateAll: "UPDATE_ALL",
    remove: "REMOVE"
};

export const minDelay = 100;

export const socketServerUrl = conf.endpoints["socket-io"];


