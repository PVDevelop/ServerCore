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
            return Object.assign({},
                state, {
                    validating: true
                });
        case validateTokenActions.FAILURE:
            return Object.assign({},
                state, {
                    validating: false,
                    everValidated: true
                });
        case validateTokenActions.VALIDATED:
            return Object.assign({},
                state, {
                    validating: false,
                    everValidated: true,
                    currentUser: {}
                });
        case signOutActions.SIGNED_OUT:
            return Object.assign({},
                state, {
                    currentUser: null
                });
        case validateTokenActions.VALIDATED:
        case signInActions.SIGNED_IN:
            return Object.assign({},
                state, {
                    currentUser: {}
                });
        default:
            return state;
    };
}