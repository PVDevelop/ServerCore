import * as registerUserActions from "../actions/registerUser";

const initialState = {
    email: "",
    emailError: "",
    password: "",
    passwordError: "",
    confirmPassword: "",
    confirmPasswordError: "",
    hasErrors: false,
    registering: false,
    registered: false
};

export function validate(state) {
    var hasErrors = false;

    var emailError="";
    if (!state.email) {
        emailError = "Почтовый адрес не задан";
        hasErrors = true;
    }

    var passwordError="";
    if(!state.password){
        passwordError = "Пароль не задан";
        hasErrors = true;
    }

    var confirmPasswordError = "";
    if(state.confirmPassword != state.password){
        confirmPasswordError = "Пароли не совпадают";
        hasErrors = true;
    }

    return {
        ...state,
        emailError: emailError,
        passwordError: passwordError,
        confirmPasswordError: confirmPasswordError,
        hasErrors: hasErrors
    }
}

export default function registration(state, action) {
    switch (action.type) {
        case registerUserActions.EMAIL:
        {
            const newState = {
                ...state,
                email: action.email
            };
            return validate(newState);
        }
        case registerUserActions.PASSWORD:
        {
            const newState = {
                ...state,
                password: action.password
            };
            return validate(newState);
        }
        case registerUserActions.CONFIRM_PASSWORD:
        {
            const newState = {
                ...state,
                confirmPassword: action.confirmPassword
            };
            return validate(newState);
        }
        case registerUserActions.REGISTERING:
            return {
            ...state,
                registering: true
            };
        case registerUserActions.FAILURE:
            return {
                ...state,
                registering: false
            };
        case registerUserActions.REGISTERED:
            return {
                ...state,
                registering: false,
                registered: true
            };
        case registerUserActions.REGISTER_COMPONENT_WILL_MOUNT:
            return {
                ...state,
                registered: false
            };
        default:
            if(!state){
                return validate(initialState);
            }
            return state;
    }
}