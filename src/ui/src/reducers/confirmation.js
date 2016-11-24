import * as confirmationActions from "../actions/confirmation";

const initialState = {
    isConfirming: false
};

export default function confirmation(state = initialState, action) {
    switch (action.type) {
        case confirmationActions.isConfirming:
            return {...state, isConfirming: action.isConfirming };
        default:
            return state;
    }
}