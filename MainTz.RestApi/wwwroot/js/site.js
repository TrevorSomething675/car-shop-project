var btn = document.querySelector('#AdminBtn')
var token = GetCookie('accessToken');
var result = '';

function ClickCustomButton() {
    fetch("/admin/index", {
        method: 'GET',
        headers: {
            'Authorixation': `Bearer ${token}`
        }})
        .then(response => {
            if (response.ok) {
                result = 'jija';
            }
        })
}


//function SendRequestWithToken(event) {
//    const headers = new Headers(event.request.headers);

//    headers.set('Authorization', token);

//    const newRequest = new Request(event.request, {
//        mode: 'same-origin',
//        credentials: 'include',
//        headers: headers
//    })
//    return fetch(newRequest);
//}