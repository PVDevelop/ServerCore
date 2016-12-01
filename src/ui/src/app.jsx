import React from "react";
import ReactDOM from "react-dom";
import { IndexRoute, Route, Router, browserHistory } from "react-router";
import { Provider } from "react-redux";

import configureStore from "./configureStore";

import MainContainer from "./containers/mainContainer";
import HomeContainer from "./containers/homeContainer";
import SignInContainer from "./containers/signInContainer";
import RegistrationContainer from "./containers/registrationContainer";
import ConfirmationContainer from "./containers/confirmationContainer";
import NotFoundContainer from "./containers/notFoundContainer";

const store = configureStore();

ReactDOM.render(
    <Provider store={store}>
        <Router history={browserHistory}>
            <Route path="/" component={MainContainer}>
                <IndexRoute component={HomeContainer} />
            
                <Route path="/signin" component={SignInContainer} />
                <Route path="/register" component={RegistrationContainer} />
                <Route path="/confirmations/:key" component={ConfirmationContainer} />

                <Route path="*" component={NotFoundContainer} />
            </Route>
        </Router>
    </Provider>,
    document.getElementById("main")
);