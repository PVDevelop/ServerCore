import { browserHistory } from "react-router";
import { httpPut } from "../utils/http";
import * as routes from "../routes";

export const EMAIL = "SIGN_IN_EMAIL";
export function setEmail(email) {
    return {
        type: EMAIL,
        email: email
    };
}

export const PASSWORD = "SIGN_IN_PASSWORD";
export function setPassword(password) {
    return {
        type: PASSWORD,
        password: password
    };
}

export const SIGNING_IN = "SIGN_IN_SIGNING_IN";
function onSigningIn() {
    return {
        type: SIGNING_IN
    }
}

export const FAILURE = "SIGN_IN_FAILURE";
function onFailure(error) {
    return {
        type: FAILURE,
        error: error
    }
}

export const SIGNED_IN = "SIGN_IN_SIGNED_IN";
function onSignedIn() {
    browserHistory.push("/")
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
                }
                else if (response.status == 400) {
                    response
                        .json()
                        .then(j => {
                            dispatch(onFailure(j.message));
                        });
                }
                else {
                    throw new Error("Unepected status code: " + response.status);
                }
            })
            .catch(err => {
                console.error(err);
                dispatch(onFailure("Возникла непредвиденная ошибка. Повторите попытку."));
            });
    };
}