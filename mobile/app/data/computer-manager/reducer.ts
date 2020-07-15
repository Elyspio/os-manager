import {createReducer} from "@reduxjs/toolkit";
import {setComputers, setSelectedComputer} from "./actions";

export interface Computer {
    host: string,
    name: string,
    id: string
}

export type ComputerIdentifier = Pick<Computer, "id">

export interface ComputerManagerState {
    current?: Computer,
    all: Computer[]
}

const defaultState: ComputerManagerState = {
    all: []
}
export const computerManagerReducer = createReducer<ComputerManagerState>(defaultState, builder => {
    builder.addCase(setSelectedComputer, (state, action) => {
        state.current = action.payload
    })

    builder.addCase(setComputers, (state, action) => {
        action.payload.forEach(value => {
            if (state.all.find(computer => computer.name !== value.name) === undefined) {
                state.all.push(value);
            }
        })
    })
})
