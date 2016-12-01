import * as signInActions from "../actions/signIn";
import * as signOutActions from "../actions/signOut";

const initialState = {
    email: "",
    password: "",
    signingIn: false,
    signInError: "",
    signingOut: false
};

export default function signIn(state = initialState, action) {
    switch (action.type) {
        case signInActions.EMAIL:
            return Object.assign({},
                state, {
                    email: action.email
                });
        case signInActions.PASSWORD:
            return Object.assign({},
                state, {
                    password: action.password
                });
        case signInActions.SIGNING_IN:
            return Object.assign({},
                state, {
                    signingIn: true
                });
        case signInActions.SIGNED_IN:
            return Object.assign({},
                state, {
                    signingIn: false,
                    signInError: null
                });
        case signInActions.FAILURE:
            return Object.assign({},
                state, {
                    signingIn: false,
                    error: action.error
                });
        case signOutActions.SIGNING_OUT:
            return Object.assign({},
                state, {
                    signingOut: true
                });
        case signOutActions.SIGNED_OUT:
            return Object.assign({},
                state, {
                    signingOut: false
                });
        case signOutActions.FAILURE:
            return Object.assign({},
                state, {
                    signingOut: false
                });
        default:
            return state;
    };
}