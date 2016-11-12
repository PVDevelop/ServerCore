import { combineReducers, createStore } from "redux";
import registering from "./reducers/registering";

export default function configureStore() {
    const combinedReducer = combineReducers({registering});
    const store = createStore(combinedReducer);
    return store;
}