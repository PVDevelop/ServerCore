import * as signInActions from "../actions/signIn";
import * as signOutActions from "../actions/signOut";

const initialState = {
    email: "",
    password: "",
    signingIn: false,
    signingOut: false
};

export default function signIn(state = initialState, action) {
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
                signingIn: false
            }
        case signInActions.FAILURE:
            return {
                ...state,
                signingIn: false
            };
        case signOutActions.SIGNING_OUT:
            return {
                ...state,
                signingOut: true
            };
        case signOutActions.SIGNED_OUT:
            return {
                ...state,
                signingOut: false
            }
        case signOutActions.FAILURE:
            return {
                ...state,
                signingOut: false
            };
        default:
            return state;
    };
}