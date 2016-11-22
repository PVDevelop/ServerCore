import { combineReducers, createStore } from "redux";
import registration from "./reducers/registration";
import confirmation from "./reducers/confirmation";
import signIn from "./reducers/signIn";

export default function configureStore() {
    const combinedReducer = combineReducers({ registration, confirmation, signIn });
    const store = createStore(combinedReducer);
    return store;
}