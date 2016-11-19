import { registrationEmail, registrationPassword, registrationState } from "../actions/registering";
import RegistrationState from "../const/registration";

const initialState = {
    email: "",
    password: "",
    state: RegistrationState.NONE
};

export default function registering(state = initialState, action) {
    switch (action.type) {
        case registrationEmail:
            console.debug("setting email to " + action.email);
            return {...state, email: action.email };
        case registrationPassword:
            console.debug("setting password to " + action.password);
            return {...state, password: action.password };
        case registrationState:
            console.debug("setting registration state to " + action.state);
            return {...state, state: action.state};
        default:
            return state;
    }
}