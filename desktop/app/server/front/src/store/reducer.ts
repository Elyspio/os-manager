import {combineReducers} from "redux";

import {ComputerState, reducer as computerReducer} from "./module/computer/reducer";
import {reducer as themeReducer, ThemeState} from "./module/theme/reducer";

export interface RootState {
    computer: ComputerState;
    theme: ThemeState;
}

export const rootReducer = combineReducers<RootState | undefined>({
    computer: computerReducer,
    theme: themeReducer,
});
