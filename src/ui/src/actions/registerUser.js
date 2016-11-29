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

export const CONFIRM_PASSWORD = "REGISTER_USER_CONFIRM_PASSWORD";
export function onConfirmPasswordChanged(password) {
    return {
        type: CONFIRM_PASSWORD,
        confirmPassword: password
    }
}

export const REGISTER_COMPONENT_WILL_MOUNT = "REGISTER_USER_REGISTER_COMPONENT_WILL_MOUNT";
export function onRegisterComponentWillMount() {
    return {
        type: REGISTER_COMPONENT_WILL_MOUNT
    }
}

export const REGISTERING = "REGISTER_USER_REGISTERING";
function onRegistering() {
    return {
        type: REGISTERING
    }
}

export const FAILURE = "REGISTER_USER_FAILURE";
function onFailure(error) {
    return {
        type: FAILURE,
        error: error
    }
}

export const REGISTERED = "REGISTER_USER_REGISTERED";
function onRegistered() {
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
                if (response.status === 200) {
                    console.log("Success");
                    dispatch(onRegistered());
                }
                else if (response.status === 400) {
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