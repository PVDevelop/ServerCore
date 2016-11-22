import React from "react";
import ReactDOM from "react-dom";
import { IndexRoute, Route, Router, Link, browserHistory } from "react-router";
import { Provider } from "react-redux";

import Header from "./components/header";
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
            <Route path="/" component={Header}>
                <IndexRoute component={Home} />
                <Route path="/signin" component={SignIn} />
                <Route path="/register" component={Registration} />
                <Route path="/confirmations/:key" component={Confirmation} />
            </Route>

            <Route path="*" component={NotFound} />
        </Router>
    </Provider>,
    document.getElementById("main")
);