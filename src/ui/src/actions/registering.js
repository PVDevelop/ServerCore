export const registrationEmail = "REGISTRATION_EMAIL";
export const registrationPassword = "REGISTRATION_PASSWORD";
export const registrationState = "REGISTRATION_STATE";

export function setEmail(email) {
    return {
        type: registrationEmail,
        email: email
    };
}

export function setPassword(password) {
    return {
        type: registrationPassword,
        password: password
    };
}

export function setState(state) {
    return {
        type: registrationState,
        state: state
    };
}