export function httpGet(resource) {
    var options = {
        method: "get",
        credentials: "same-origin",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    };

    return fetch(resource, options);
}

export function httpPost(resource, data) {
    var options = {
        method: "post",
        credentials: "same-origin",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    };

    if (data) {
        options.body = JSON.stringify(data);
    }

    return fetch(resource, options);
}

export function httpPut(resource, data) {
    var options = {
        method: "put",
        credentials: "same-origin",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    };

    if (data) {
        options.body = JSON.stringify(data);
    }

    return fetch(resource, options);
}