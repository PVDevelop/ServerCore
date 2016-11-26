import { SignOut } from "../routes";
import { httpPut } from "../utils/http";

export const SIGNING_OUT = "SIGN_OUT_SIGNING_OUT"
function onSigningOut() {
    return {
        type: SIGNING_OUT
    }
}

export const SIGNED_OUT = "SIGN_OUT_SUCCESS";
function onSignedOut() {
    return {
        type: SIGNED_OUT
    };
}

export const FAILURE = "SIGN_OUT_FAILURE";
function onFailed() {
    alert("Ошибка при попытке выйти из системы");
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
                }
            })
            .catch(err => {
                dispatch(onFailed());
            })
    };
}