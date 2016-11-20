import { combineReducers, createStore } from "redux";
import registering from "./reducers/registering";
import confirming from "./reducers/confirming";
import signingIn from "./reducers/signingIn";

export default function configureStore() {
    const combinedReducer = combineReducers({ registering, confirming, signingIn });
    const store = createStore(combinedReducer);
    return store;
}