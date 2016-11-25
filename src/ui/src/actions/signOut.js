import { SignOut } from "../routes";
import { httpPut } from "../utils/http";

export const SIGNING_OUT = "SIGNING_OUT"
function onSigningOut() {
    return {
        type: SIGNING_OUT
    }
}

export const SIGNED_OUT = "SUCCESS";
function onSignedOut() {
    return {
        type: SIGNED_OUT
    };
}

export const FAILURE = "FAILURE";
function onFailed() {
    return {
        type: FAILURE
    };
}

export function signOut() {
    return dispatch => {
        dispatch(onSigningOut());

        httpPut(SignOut)
            .then(response => {
                if (response.status == 200) {
                    dispatch(onSignedOut());
                }
                else {
                    dispatch(onFailed());
                    alert("Ошибка при попытке выйти из системы");
                }
            })
            .catch(err => {
                dispatch(onFailed());
                alert("Ошибка при попытке выйти из системы");
            })
    };
}