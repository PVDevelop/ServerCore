import * as registrationActions from "../actions/registration";

const initialState = {
    email: "",
    password: "",
    waitingForResponse: false
};

export default function registration(state = initialState, action) {
    switch (action.type) {
        case registrationActions.registrationEmail:
            return {...state, email: action.email };
        case registrationActions.registrationPassword:
            return {...state, password: action.password };
        case registrationActions.registrationWaitingForResponse:
            return {...state, waitingForResponse: action.waitingForResponse };
        default:
            return state;
    }
}