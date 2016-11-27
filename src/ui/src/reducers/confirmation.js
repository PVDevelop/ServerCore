import * as confirmUserActions from "../actions/confirmUser";

const initialState = {
    confirming: false,
};

export default function confirmation(state = initialState, action) {
    switch (action.type) {
        case confirmUserActions.CONFIRMING:
            return {
                ...state,
                confirming: true
            }
        case confirmUserActions.CONFIRMED:
            return {
                ...state,
                confirming: false
            }
        case confirmUserActions.FAILURE:
            return {
                ...state,
                confirming: false
            }
        default:
            return state;
    }
}