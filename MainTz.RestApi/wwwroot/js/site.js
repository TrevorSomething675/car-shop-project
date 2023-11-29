function SendRequestWithToken(event, token) {
    const headers = new Headers(event.request.headers);

    headers.set('Authorization', token);

    const newRequest = new Request(event.request, {
        mode: 'same-origin',
        credentials: 'include',
        headers: headers
    })
    return fetch(newRequest);
}