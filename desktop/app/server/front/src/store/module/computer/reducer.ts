import {ActionReducerMapBuilder, createReducer} from "@reduxjs/toolkit";
import {addComputer} from "./action";
import {socket} from "../../../core/socket";
import {socketEvents} from "../../../config/sockets";
import store from "../../index";
import {ComputerService} from "../../../core/ComputerService";
import {Computer} from "../../../../../../../../mobile/app/data/computer-manager/reducer";

export interface ComputerState {
    computers: Computer[];
    current?: Computer;
}

const defaultState: ComputerState = {
    computers: [],
    current: undefined,
};

export const reducer = createReducer<ComputerState>(
    defaultState,
    (builder: ActionReducerMapBuilder<ComputerState>) => {
        builder.addCase(addComputer, (state, action) => {
            for (const light of action.payload) {
                let index = state.computers.findIndex((l) => l.host === light.host);
                if (index === -1) {
                    state.computers.push(light);
                } else {
                    state.computers[index] = light;
                }
            }
        });


    }
);

socket.on(socketEvents.updateAll, async (names: string[]) => {
    console.log("UPDATE ALL from server", names);

    const lights = await Promise.all(names.map(name => ComputerService.instance.get({name: name})))

    store.dispatch(addComputer(lights));
});

