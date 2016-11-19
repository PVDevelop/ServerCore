export const confirmationState = "CONFIRMATION_STATE";

export function setState(state) {
    return {
        type: confirmationState,
        state: state
    };
}