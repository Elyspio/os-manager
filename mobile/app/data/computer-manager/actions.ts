import {createAction} from "@reduxjs/toolkit";
import {Computer} from "./reducer";
import {fetchServer} from "../../core/server";

export const setSelectedComputer = createAction<Computer>("setSelectedComputer")
export const setComputers = createAction<Computer[]>("setComputers");
export const getComputers = () => {
    return (dispatch: Function) => {
        fetchServer("/", "GET")?.then(res => dispatch(setComputers(res.data)));
    }
}
