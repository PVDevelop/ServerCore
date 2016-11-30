import * as confirmUserActions from "../actions/confirmUser";

const initialState = {
    confirming: false,
    error: null,
    repeat: false
};

export default function confirmation(state = initialState, action) {
    switch (action.type) {
        case confirmUserActions.CONFIRMING:
            return {
                ...state,
                confirming: true,
                error: null
            }
        case confirmUserActions.CONFIRMED:
            return {
                ...state,
                confirming: false,
                error: null
            }
        case confirmUserActions.FAILURE:
            return {
                ...state,
                confirming: false,
                error: action.error,
                repeat: action.repeat
            }
        default:
            return state;
    }
}