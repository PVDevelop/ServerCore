import React from "react";
import ReactDOM from "react-dom";
import { Route, Router, Link, browserHistory } from "react-router";
import { Provider } from "react-redux";

import Home from "./components/home";
import SignInForm from "./components/signingIn/signInForm";
import RegistrationFrom from "./components/registration/registrationForm";
import Confirmation from "./components/confirmation/confirmation";
import NotFound from "./components/not_found";
import configureStore from "./configureStore";

import RegistrationState from "./const/registration";

const store = configureStore();

ReactDOM.render(
    <Provider store = {store}>
        <Router history={browserHistory}>
            <Route path="/" component={Home} />
            <Route path="/signin" component={SignInForm} />
            <Route path="/register" component={RegistrationFrom} />
            <Route path="/confirmations/:key" component={Confirmation} />

            <Route path="*" component={NotFound} />
        </Router>
    </Provider>,
    document.getElementById("main")
);