import * as singInActions from "../actions/signingIn";

const initialState = {
    email: "",
    password: "",
    isCreating: false
};

export default function signingIn(state = initialState, action) {
    switch (action.type) {
        case singInActions.signingInEmail:
            console.debug("setting signingIn email to " + action.email);
            return {...state, email: action.email };
        case singInActions.signingInPassword:
            console.debug("setting signingIn password to " + action.password);
            return {...state, password: action.password };
        case singInActions.signingInIsCreating:
            console.debug("setting isCreating to " + action.isCreating);
            return {...state, isCreating: action.isCreating };
        default:
            console.debug("unknown signingIn action type " + action.type);
            return state;
    }
}