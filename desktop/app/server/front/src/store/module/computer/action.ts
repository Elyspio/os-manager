import {createAction as _createAction} from "@reduxjs/toolkit";
import {Computer, ComputerIdentifier} from "../../../../../../../../mobile/app/data/computer-manager/reducer";

const createAction = <T>(name: string) => _createAction<T>(`light/${name}`);

export const addComputer = createAction<Computer[]>("add");
export const removeComputer = createAction<ComputerIdentifier>("remove");
