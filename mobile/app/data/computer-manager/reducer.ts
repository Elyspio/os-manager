import {createReducer} from "@reduxjs/toolkit";
import {setSelectedComputer} from "./actions";

export interface Computer {
    host: string,
    name: string
}
export interface ComputerManagerState {
    current?: Computer
}
const defaultState: ComputerManagerState = {

}
export const computerManagerReducer = createReducer<ComputerManagerState>(defaultState, builder => {
    builder.addCase(setSelectedComputer, (state, action) => {
        state.current = action.payload
    })
})
