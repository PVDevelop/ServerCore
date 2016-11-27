import * as registerUserActions from "../actions/registerUser";

const initialState = {
    email: "",
    password: "",
    confirmPassword: "",
    registering: false,
};

export default function registration(state = initialState, action) {
    switch (action.type) {
        case registerUserActions.EMAIL:
            return {
                ...state,
                email: action.email
            };
        case registerUserActions.PASSWORD:
            return {
                ...state,
                password: action.password
            };
        case registerUserActions.CONFIRM_PASSWORD:
            return {
                ...state,
                confirmPassword: action.confirmPassword
            };
        case registerUserActions.REGISTERING:
            return {
            ...state,
                registering: true
            }
        case registerUserActions.FAILURE:
            return {
                ...state,
                registering: false
            }
        case registerUserActions.REGISTERED:
            return {
                ...state,
                registering: false
            }
        default:
            return state;
    }
}