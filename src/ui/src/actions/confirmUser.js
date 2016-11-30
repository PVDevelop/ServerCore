import * as routes from "../routes";
import { httpGet } from "../utils/http";

export const CONFIRMING = "CONFIRM_USER_CONFIRMING";
function onConfirming() {
    return {
        type: CONFIRMING
    }
}

export const CONFIRMED = "CONFIRM_USER_CONFIRMED";
function onConfirmed() {
    return {
        type: CONFIRMED
    };
}

export const FAILURE = "CONFIRM_USER_FAILURE";
function onFailure(error, repeat) {
    return {
        type: FAILURE,
        error: error,
        repeat: repeat
    }
}

export function confirm(key) {
    return dispatch => {
        dispatch(onConfirming());

        httpGet(routes.ConfirmUser + "/" + key)
            .then(response => {
                if (response.status === 200) {
                    dispatch(onConfirmed());
                }
                else if (response.status === 400) {
                    response
                        .json()
                        .then(j => {
                            dispatch(onFailure(j.message, false));
                        });
                }
                else {
                    throw new Error("Unepected status code: " + response.status);
                }
            })
            .catch(err => {
                console.error(err, true);
                dispatch(onFailure("Возникла непредвиденная ошибка. Повторите попытку.", true));
            });
    }
}