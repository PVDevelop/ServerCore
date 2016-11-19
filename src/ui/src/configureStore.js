import { combineReducers, createStore } from "redux";
import registering from "./reducers/registering";
import confirming from "./reducers/confirming";

export default function configureStore() {
    const combinedReducer = combineReducers({registering, confirming});
    const store = createStore(combinedReducer);
    return store;
}