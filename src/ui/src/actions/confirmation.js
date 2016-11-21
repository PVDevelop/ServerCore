export const isConfirming = "CONFIRMATION_IS_CONFIRMING";

export function setState(isConfirming) {
    return {
        type: isConfirming,
        isConfirming: isConfirming
    };
}