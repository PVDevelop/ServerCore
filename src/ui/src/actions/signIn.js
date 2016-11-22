export const signInEmail = "SIGN_IN_EMAIL";
export const signInPassword = "SIGN_IN_PASSWORD";
export const signInIsCreating = "SIGN_IN_IS_CREATING";
export const signInIsSignedIn = "SIGN_IN_IS_SIGNED_IN";

export function setEmail(email) {
    return {
        type: signInEmail,
        email: email
    };
}

export function setPassword(password) {
    return {
        type: signInPassword,
        password: password
    };
}

export function setIsCreating(isCreating) {
    return {
        type: signInIsCreating,
        isCreating: isCreating
    }
}

export function setIsSignedIn(isSignedIn) {
    return {
        type: signInIsSignedIn,
        isSignedIn: isSignedIn
    }
}