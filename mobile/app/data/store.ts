import {combineReducers, configureStore, getDefaultMiddleware} from "@reduxjs/toolkit"
import {computerManagerReducer, ComputerManagerState} from "./computer-manager/reducer";


export interface StoreState {
    computer: ComputerManagerState
}

const reducers = {
    computer: computerManagerReducer
}

export const store = configureStore({
    devTools: true,
    middleware: [...getDefaultMiddleware()],
    reducer: combineReducers(reducers)
})
