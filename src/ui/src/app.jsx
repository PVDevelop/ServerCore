import React from "react";
import ReactDOM from "react-dom";
import { Route, Router, Link, browserHistory } from "react-router";
import { Provider } from "react-redux";

import Home from "./components/home";
import SignIn from "./components/signIn";
import Registration from "./components/registration";
import Confirmation from "./components/confirmation";
import NotFound from "./components/not_found";
import configureStore from "./configureStore";

const store = configureStore();

ReactDOM.render(
    <Provider store={store}>
        <Router history={browserHistory}>
            <Route path="/" component={Home} />
            <Route path="/signin" component={SignIn} />
            <Route path="/register" component={Registration} />
            <Route path="/confirmations/:key" component={Confirmation} />

            <Route path="*" component={NotFound} />
        </Router>
    </Provider>,
    document.getElementById("main")
);