import * as registrationActions from "../actions/registration";

const initialState = {
    email: "",
    password: "",
    waitingForResponse: false
};

export default function registration(state = initialState, action) {
    switch (action.type) {
        case registrationActions.registrationEmail:
            console.debug("setting registration email to " + action.email);
            return {...state, email: action.email };
        case registrationActions.registrationPassword:
            console.debug("setting registration password to " + action.password);
            return {...state, password: action.password };
        case registrationActions.registrationWaitingForResponse:
            console.debug("setting registration state to " + action.waitingForResponse);
            return {...state, waitingForResponse: action.waitingForResponse };
        default:
            return state;
    }
}