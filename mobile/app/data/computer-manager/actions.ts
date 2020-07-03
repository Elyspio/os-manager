import {createAction} from "@reduxjs/toolkit";
import {Computer} from "./reducer";

export const setSelectedComputer = createAction<Computer>("setSelectedComputer")
