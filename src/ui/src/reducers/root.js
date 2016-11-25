import { combineReducers } from "redux";
import registration from "./registration";
import confirmation from "./confirmation";
import user from "./user";

const root = combineReducers({
    registration,
    confirmation,
    user
});

export default root;