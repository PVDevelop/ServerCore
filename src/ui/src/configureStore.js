import { combineReducers, createStore } from "redux";
import registration from "./reducers/registration";
import confirmation from "./reducers/confirmation";
import signingIn from "./reducers/signingIn";

export default function configureStore() {
    const combinedReducer = combineReducers({ registration, confirmation, signingIn });
    const store = createStore(combinedReducer);
    return store;
}