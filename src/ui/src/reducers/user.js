import * as signInActions from "../actions/signIn";
import * as signOutActions from "../actions/signOut";
import * as validateUserActions from "../actions/validateUser";

const initialState = {
    email: "",
    password: "",
    signingIn: false,
    signingOut: false,
    failedToSignIn: false,
    failedToSignOut: false,
    validating: false,
    currentUser: null
};

export default function user(state = initialState, action) {
    switch (action.type) {
        case signInActions.EMAIL:
            return {
                ...state,
                email: action.email
            };
        case signInActions.PASSWORD:
            return {
                ...state,
                password: action.password
            };
        case signInActions.SIGNING_IN:
            return {
                ...state,
                signingIn: true
            };
        case signInActions.SIGNED_IN:
            return {
                ...state,
                signingIn: false,
                failedToSignIn: false,
                currentUser: {}
            }
        case signInActions.FAILURE:
            return {
                ...state,
                signingIn: false,
                failedToSignIn: true
            };
        case signOutActions.SIGNING_OUT:
            return {
                ...state,
                signingOut: true
            };
        case signOutActions.SIGNED_OUT:
            return {
                ...state,
                signingOut: false,
                failedToSignOut: false,
                currentUser: null
            }
        case signOutActions.FAILURE:
            return {
                ...state,
                signingOut: false,
                failedToSignIn: true
            };
        case validateUserActions.VALIDATING:
            return {
                ...state,
                validating: true
            }
        case validateUserActions.FAILURE:
            return {
                ...state,
                validating: false
            }
        case validateUserActions.VALIDATED:
            return {
            ...state,
                validating: false,
                currentUser: {}
            }
        default:
            return state;
    };
}