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
            console.debug("setting signingIn email to " + action.email);
            return {...state, email: action.email };
        case signInActions.signInPassword:
            console.debug("setting signingIn password to " + action.password);
            return {...state, password: action.password };
        case signInActions.signInIsCreating:
            console.debug("setting isCreating to " + action.isCreating);
            return {...state, isCreating: action.isCreating };
        case signInActions.signInIsSignedIn:
            console.debug("setting isSignedIn to " + action.isSignedIn);
            return {...state, isSignedIn: action.isSignedIn };
        default:
            return state;
    }
}