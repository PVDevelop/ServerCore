export const START_REQUEST = "START_REQUEST";
function startRequest() {
    return {
        type: START_REQUEST
    }
}

export const PROCESS_RESPONSE = "PROCESS_RESPONSE";
function processResopnse(result) {
    return {
        type: PROCESS_RESPONSE,
        userName: result.userName
    }
}

export function fetchTestAsync() {
    return dispatch => {
        // оповещаем о начале запроса
        dispatch(startRequest());

        // ожидаем определенное время
        return sleep(10000).then(() => {
            var data = {
                userName: "some"
            };
            dispatch(processResopnse(data));
        });
    }
}

function sleep(time) {
    return new Promise((resolve) => setTimeout(resolve, time));
}