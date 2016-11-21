export const registrationEmail = "REGISTRATION_EMAIL";
export const registrationPassword = "REGISTRATION_PASSWORD";
export const registrationWaitingForResponse = "REGISTRATION_WAITING_FOR_RESPONSE";

export function setEmail(email) {
    return {
        type: registrationEmail,
        email: email,
        waitingForResponse: false
    };
}

export function setPassword(password) {
    return {
        type: registrationPassword,
        password: password
    };
}

export function setWaitingForResponse(waitingForResponse) {
    return {
        type: registrationWaitingForResponse,
        waitingForResponse: waitingForResponse
    };
}