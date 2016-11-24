import * as signInActions from "../actions/signIn";

const initialState = {
    email: "",
    password: "",
    isCreating: false,
    isSignedIn: false
};

export default function signIn(state = initialState, action) {
    switch (action.type) {
        case signInActions.signInEmail:
            return {...state, email: action.email };
        case signInActions.signInPassword:
            return {...state, password: action.password };
        case signInActions.signInIsCreating:
            return {...state, isCreating: action.isCreating };
        case signInActions.signInIsSignedIn:
            return {...state, isSignedIn: action.isSignedIn };
        default:
            return state;
    }
}