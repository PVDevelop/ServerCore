import { httpPut } from "../utils/http";
import * as routes from "../routes";
import { browserHistory } from "react-router";

export const EMAIL = "EMAIL";
export function setEmail(email) {
    return {
        type: EMAIL,
        email: email
    };
}

export const PASSWORD = "PASSWORD";
export function setPassword(password) {
    return {
        type: PASSWORD,
        password: password
    };
}

export const SIGNING_IN = "SIGNING_IN";
function onSigningIn() {
    return {
        type: SIGNING_IN
    }
}

export const FAILURE = "FAILURE";
function onFailure() {
    return {
        type: FAILURE
    }
}

export const SIGNED_IN = "SIGNED_IN";
export function onSignedIn() {
    return {
        type: SIGNED_IN
    }
}

export function signIn(email, password) {
    return dispatch => {
        dispatch(onSigningIn());

        var data = {
            email: email,
            password: password
        };

        httpPut(routes.SignIn, data)
            .then(response => {
                if (response.status == 200) {
                    dispatch(onSignedIn());
                    browserHistory.push("/");
                }
                else {
                    dispatch(onFailure());
                    alert("Ошибка аутентификации");
                }
            })
            .catch(err => {
                dispatch(onFailure());
                alert("Ошибка аутентификации");
            });
    };
}