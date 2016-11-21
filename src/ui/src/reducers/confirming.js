import * as confirmingActions from "../actions/confirming";
import ConfirmationState from "../const/confirmation";

const initialState = {
    state: ConfirmationState.PENDING
};

export default function confirming(state = initialState, action) {
    switch (action.type) {
        case confirmingActions.confirmationState:
            console.debug("setting confirmation state to " + action.state);
            return {...state, state: action.state};
        default:
            console.debug("unknown confirming action type " + action.type);
            return state;
    }
}