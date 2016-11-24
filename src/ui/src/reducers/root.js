import { combineReducers } from "redux";
import registration from "./registration";
import confirmation from "./confirmation";
import signIn from "./signIn";
import test from "./test";

const root = combineReducers({
    registration,
    confirmation,
    signIn,
    test
});

export default root;