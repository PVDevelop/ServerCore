import thunkMiddleware from "redux-thunk";
import createLogger from "redux-logger";
import { createStore, applyMiddleware } from "redux";
import root from "./reducers/root";

export default function configureStore() {
    const loggerMiddleware = createLogger();

    return createStore(
        root,
        applyMiddleware(
            thunkMiddleware,
            loggerMiddleware
        ));
}