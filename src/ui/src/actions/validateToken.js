import { httpPut } from "../utils/http";
import * as routes from "../routes";

export const VALIDATING = "VALIDATE_USER_VALIDATING";
function onValidating() {
    return {
        type: VALIDATING
    }
}

export const VALIDATED = "VALIDATE_USER_VALIDATED";
function onValidated() {
    return {
        type: VALIDATED
    }
}

export const FAILURE = "VALIDATE_USER_FAILURE";
function onFailure() {
    return {
        type: FAILURE
    }
}

export function validate() {
    return dispatch => {
        dispatch(onValidating());

        httpPut(routes.ValidateToken)
            .then(response => {
                if (response.status == 200) {
                    dispatch(onValidated());
                }
                else {
                    dispatch(onFailure());
                }
            })
            .catch(response => {
                dispatch(onFailure());
            });
    };
}