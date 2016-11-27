import * as signInActions from "../actions/signIn";
import * as signOutActions from "../actions/signOut";
import * as validateTokenActions from "../actions/validateToken";
import * as registerUserActions from "../actions/registerUser";
import * as confirmUserActions from "../actions/confirmUser";

const initialState = {
    email: "",
    password: "",
    confirmPassword: "",
    signingIn: false,
    signingOut: false,
    registering: false,
    confirming: false,
    failedToSignIn: false,
    failedToSignOut: false,
    validating: false,
    currentUser: null
};

export default function user(state = initialState, action) {
    switch (action.type) {
        case registerUserActions.EMAIL:
        case signInActions.EMAIL:
            return {
                ...state,
                email: action.email
            };
        case registerUserActions.PASSWORD:
        case signInActions.PASSWORD:
            return {
                ...state,
                password: action.password
            };
        case registerUserActions.CONFIRM_PASSWORD:
            return {
                ...state,
                confirmPassword: action.confirmPassword
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
                validating: false,
                currentUser: {}
            }
        case registerUserActions.REGISTERING:
            return {
            ...state,
                registering: true
            }
        case registerUserActions.FAILURE:
            return {
                ...state,
                registering: false
            }
        case registerUserActions.REGISTERED:
            return {
                ...state,
                registering: false
            }
        case confirmUserActions.CONFIRMING:
            return {
                ...state,
                confirming: true
            }
        case confirmUserActions.CONFIRMED:
            return {
                ...state,
                confirming: false
            }
        case confirmUserActions.FAILURE:
            return {
                ...state,
                confirming: false
            }
        default:
            return state;
    };
}