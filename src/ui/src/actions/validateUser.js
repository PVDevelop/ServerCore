import { httpPut } from "../utils/http";
import * as routes from "../routes";

export const VALIDATING = "VALIDATING";
function onValidating() {
    return {
        type: VALIDATING
    }
}

export const VALIDATED = "VALIDATED";
function onValidated() {
    return {
        type: VALIDATED
    }
}

export const FAILURE = "FAILURE";
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