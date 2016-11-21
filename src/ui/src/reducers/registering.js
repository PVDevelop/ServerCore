import * as registeringActions from "../actions/registering";
import RegistrationState from "../const/registration";

const initialState = {
    email: "",
    password: "",
    state: RegistrationState.NONE
};

export default function registering(state = initialState, action) {
    switch (action.type) {
        case registeringActions.registrationEmail:
            console.debug("setting registration email to " + action.email);
            return {...state, email: action.email };
        case registeringActions.registrationPassword:
            console.debug("setting registration password to " + action.password);
            return {...state, password: action.password };
        case registeringActions.registrationState:
            console.debug("setting registration state to " + action.state);
            return {...state, state: action.state };
        default:
            console.debug("unknown registering action type " + action.type);
            return state;
    }
}