import * as confirmUserActions from "../actions/confirmUser";

const initialState = {
    confirming: false,
    error: null,
    repeat: false
};

export default function confirmation(state = initialState, action) {
    switch (action.type) {
        case confirmUserActions.CONFIRMING:
            return Object.assign({}, state, {
                confirming: true,
                error: null
            });
        case confirmUserActions.CONFIRMED:
            return Object.assign({},
                state, {
                    confirming: false,
                    error: null
                });
        case confirmUserActions.FAILURE:
            return Objec.assign({},
                state, {
                    confirming: false,
                    error: action.error,
                    repeat: action.repeat
                });
        default:
            return state;
    }
}