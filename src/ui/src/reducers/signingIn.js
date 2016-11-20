import { signingInEmail, signingInPassword } from "../actions/signingIn";

const initialState = {
    email: "",
    password: ""
};

export default function signingIn(state = initialState, action) {
    switch (action.type) {
        case signingInEmail:
            console.debug("setting signingIn email to " + action.email);
            return {...state, email: action.email };
        case signingInPassword:
            console.debug("setting signingIn password to " + action.password);
            return {...state, password: action.password };
        default:
            return state;
    }
}