import React from "react";
import ReactDOM from "react-dom";
import { Route, Router, Link, browserHistory } from "react-router";
import { Provider } from "react-redux";

import Home from "./components/home";
import RegistrationFrom from "./components/registration/registrationForm";
import Confirm from "./components/confirmation";
import NotFound from "./components/not_found";
import configureStore from "./configureStore";

import RegistrationState from "./const/registration";

const store = configureStore();

ReactDOM.render(
    <Provider store = {store}>
        <Router history={browserHistory}>
            <Route path="/" component={Home} />
            <Route path="/register" component={RegistrationFrom} />
            <Route path="/confirmations/:confirmation_id" component={Confirm} />

            <Route path="*" component={NotFound} />
        </Router>
    </Provider>,
    document.getElementById("main")
);