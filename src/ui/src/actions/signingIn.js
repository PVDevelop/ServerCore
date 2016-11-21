export const signingInEmail = "SIGNINGIN_EMAIL";
export const signingInPassword = "SIGNINGIN_PASSWORD";
export const signingInIsCreating = "SIGNINGIN_IS_CREATING"; 

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

export function setIsCreating(isCreating){
    return{
        type: signingInIsCreating,
        isCreating: isCreating
    }
}