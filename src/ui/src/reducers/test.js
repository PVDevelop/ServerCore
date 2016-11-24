import * as testActions from "../actions/test";

const initialState = {
    userName: ""
}

export default function test(state = initialState, action) {
    switch (action.type) {
        case testActions.START_REQUEST:
            console.log("Starting test request");
            return state;
        case testActions.PROCESS_RESPONSE:
            console.log("user received: " + action.userName);
            return {...state, userName: action.userName };
        default:
            return state;
    }
}