import thunkMiddleware from "redux-thunk";
import createLogger from "redux-logger";
import { createStore, applyMiddleware } from "redux";
import root from "./reducers/root";
import { fetchTestAsync } from "./actions/test";

const loggerMiddleware = createLogger();

export default function configureStore() {
    const store = createStore(
        root,
        applyMiddleware(
            thunkMiddleware
            //loggerMiddleware
        ));

    store.dispatch(fetchTestAsync()).then(() =>
        console.log(store.getState())
    )

    return store;
}