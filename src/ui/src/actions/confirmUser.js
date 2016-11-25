import { browserHistory } from "react-router";
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
    alert("Пользователь подтвержден");
    browserHistory.push("/");
    return {
        type: CONFIRMED
    };
}

export const FAILURE = "CONFIRM_USER_FAILURE";
function onFailure() {
    alert("Ошибка подтверждения пользователя");
    browserHistory.push("/");
    return {
        type: FAILURE
    }
}

export function confirm(key) {
    return dispatch => {
        dispatch(onConfirming());

        httpGet(routes.ConfirmUser + "/" + key)
            .then(response => {
                if (response.status == 200) {
                    dispatch(onConfirmed());
                }
                else {
                    dispatch(onFailure());
                }
            })
            .catch(err => {
                dispatch(onFailure());
            });
    }
}