export const signingInEmail = "SIGNINGIN_EMAIL";
export const signingInPassword = "SIGNINGIN_PASSWORD";

export function setEmail(email) {
    return {
        type: signingInEmail,
        email: email
    };
}

export function setPassword(password) {
    return {
        type: signingInPassword,
        password: password
    };
}