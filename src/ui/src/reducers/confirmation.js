import * as confirmationActions from "../actions/confirmation";

const initialState = {
    isConfirming: false
};

export default function confirmation(state = initialState, action) {
    switch (action.type) {
        case confirmationActions.isConfirming:
            console.debug("setting confirmation state to " + action.isConfirming);
            return {...state, isConfirming: action.isConfirming };
        default:
            return state;
    }
}