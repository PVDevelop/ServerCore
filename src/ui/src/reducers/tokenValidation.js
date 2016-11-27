import * as validateTokenActions from "../actions/validateToken";

const initialState = {
    validating: false
};

export default function tokenValidation(state = initialState, action) {
    switch (action.type) {
        case validateTokenActions.VALIDATING:
            return {
                ...state,
                validating: true
            }
        case validateTokenActions.FAILURE:
            return {
                ...state,
                validating: false
            }
        case validateTokenActions.VALIDATED:
            return {
                ...state,
                validating: false
            }
        default:
            return state;
    };
}