import { combineReducers } from "redux";
import user from "./user";
import registration from "./registration";
import confirmation from "./confirmation";
import signIn from "./signIn";
import tokenValidation from "./tokenValidation";

const root = combineReducers({
    user,
    signIn,
    registration,
    confirmation,
    tokenValidation
});

export default root;