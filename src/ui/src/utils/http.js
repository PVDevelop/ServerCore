export function httpGet(resource) {
    var options = {
        method: "get",
        credentials: "same-origin",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    };

    console.log("Requesting resource from " + resource);

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

    console.log("Sending post resource at " + resource + ", data: " + options.body);

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

    console.log("Sending put resource at " + resource + ", data: " + options.body);

    return fetch(resource, options);
}