import * as registerUserActions from "../actions/registerUser";

const initialState = {
    email: "",
    emailError: "",
    password: "",
    passwordError: "",
    confirmPassword: "",
    confirmPasswordError: "",
    registrationError: "",
    hasInputErrors: false,
    registering: false,
    registered: false
};

export function validate(state) {
    var emailError = "";
    if (!state.email) {
        emailError = "Почтовый адрес не задан";
    } else {
        let regExp = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$/;
        if (!regExp.test(state.email)) {
            emailError = "Потовый адрес указан неверно";
        }
    }

    var passwordError = "";
    if (!state.password) {
        passwordError = "Пароль не задан";
    }
    else {
        let regEp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{7,25}$/;
        if (!regEp.test(state.password)){
            passwordError = "Пароль должен быть длиной от 7 до 25 символов, содержать цифры, латинские заглавные и прописные буквы";
        }
    }

    var confirmPasswordError = "";
    if (state.confirmPassword != state.password) {
        confirmPasswordError = "Пароли не совпадают";
    }

    const hasInputErrors = emailError !== "" || passwordError !== "" || confirmPasswordError !== "";
    return {
        ...state,
        emailError: emailError,
        passwordError: passwordError,
        confirmPasswordError: confirmPasswordError,
        hasInputErrors: hasInputErrors
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
                registering: false,
                registrationError: action.error
            };
        case registerUserActions.REGISTERED:
            return {
                ...state,
                registering: false,
                registered: true,
                registrationError: null
            };
        case registerUserActions.REGISTER_COMPONENT_WILL_MOUNT:
            return {
                ...state,
                registered: false
            };
        default:
            if (!state) {
                return validate(initialState);
            }
            return state;
    }
}