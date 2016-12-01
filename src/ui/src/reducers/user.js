import * as validateTokenActions from "../actions/validateToken";
import * as signInActions from "../actions/signIn";
import * as signOutActions from "../actions/signOut";

const initialState = {
    validating: false,
    currentUser: null,
    everValidated: false
};

export default function user(state = initialState, action) {
    switch (action.type) {
        case validateTokenActions.VALIDATING:
            return {
                ...state,
                validating: true
            }
        case validateTokenActions.FAILURE:
            return {
                ...state,
                validating: false,
                everValidated: true
            }
        case validateTokenActions.VALIDATED:
            return {
                ...state,
                validating: false,
                everValidated: true,
                currentUser: {}
            }
        case signOutActions.SIGNED_OUT:
            return {
                ...state,
                currentUser: null
            }
        case validateTokenActions.VALIDATED:
        case signInActions.SIGNED_IN:
            return {
                ...state,
                currentUser: {}
            }
        default:
            return state;
    };
}