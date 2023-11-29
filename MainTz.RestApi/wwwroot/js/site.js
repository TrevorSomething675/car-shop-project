const channelTokenBroadcast = new BroadcastChannel('channelToken');
var accessToken;
channelTokenBroadcast.onmessage = function async (event) {
    accessToken = event.data.accessToken;
    const response = SendRequestWithToken(event, accessToken);
}

self.addEventListener("fetch", event => {
    const response = SendRequestWithToken(event, accessToken);
    if (response.status == 401) {

    }
});

function SendRequestWithToken(event, token) {
    const headers = new Headers(event.request.headers);

    headers.set('Authorization', token);

    const newRequest = new Request(event.request, {
        headers: headers
    })
    return fetch(newRequest);
}

function GetCookie(key) {
    var cookies = document.cookie.split(';');
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.indexOf(key + '=') === 0) {
            return cookie.substring(key.length + 1);
        }
    }
}