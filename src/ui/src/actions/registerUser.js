import { browserHistory } from "react-router";
import { httpPost } from "../utils/http";
import * as routes from "../routes";

export const EMAIL = "REGISTER_USER_EMAIL";
export function onEmailChanged(email) {
    return {
        type: EMAIL,
        email: email
    }
}

export const PASSWORD = "REGISTER_USER_PASSWORD";
export function onPasswordChanged(password) {
    return {
        type: PASSWORD,
        password: password
    }
}

export const REGISTERING = "REGISTER_USER_REGISTERING";
function onRegistering() {
    return {
        type: REGISTERING
    }
}

export const FAILURE = "REGISTER_USER_FAILURE";
function onFailure() {
    alert("Ошибка регистрации");
    return {
        type: FAILURE
    }
}

export const REGISTERED = "REGISTER_USER_REGISTERED";
function onRegistered() {
    alert("Пользователь зарегистрирован");
    browserHistory.push("/");
    return {
        type: REGISTERED
    }
}

export function register(email, password) {
    return dispatch => {
        dispatch(onRegistering());

        var data = {
            email: email,
            password: password
        };

        httpPost(routes.RegisterUser, data)
            .then(response => {
                if (response.status == 200) {
                    dispatch(onRegistered());
                }
                else {
                    dispatch(onFailure());
                }
            })
            .catch(err => {
                dispatch(onFailure());
            });
    };
}