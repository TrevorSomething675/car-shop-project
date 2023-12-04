const loginForm = document.querySelector('#LoginForm');
var accessToken = GetCookie('accessToken');

loginForm.addEventListener('submit', async e => {
    e.preventDefault();

    const response = await fetch(loginForm.action, {
        method: loginForm.method,
        body: new FormData(loginForm)
    });

    var data = await response.json();

    document.cookie = `accessToken=${data.value.accessToken}`;
    document.cookie = `refreshToken=${data.value.refreshToken}`;
    document.cookie = `role=${data.value.role}`;
    console.log(`Access token: ${data.value.accesstoken} Refresh token: ${data.value.refreshtoken} Role: ${data.value.role}`);

    return document.location.pathname = `${data.value.role}/Index`;
});

//loginForm.addEventListener('submit', function (event) {
//    //event.preventDefault();

//    fetch(loginForm.action, {
//        method: loginForm.method,
//        body: new FormData(loginForm),
//        headers: { 'Accept': 'application/json', 'Authorization': `Bearer ${accessToken}`}
//    })
//    .then(response => {
//        if (response.ok) {
//            return response.json();
//        } else {
//            console.log('Ошибка при получении запроса');
//        }
//    })
//    .then(data => {
//        if (data != '') {
//            document.cookie = `accessToken=${data.value.accessToken}`;
//            document.cookie = `refreshToken=${data.value.refreshToken}`;
//            document.cookie = `role=${data.value.role}`;
//            console.log(`Access token: ${data.value.accesstoken} Refresh token: ${data.value.refreshtoken} Role: ${data.value.role}`);
//            document.location.pathname = `${data.value.role}/Index`;
//        } else {
//            console.log('Данные не найдены в ответе сервера')
//        }
//    })
//    .catch(error => {
//        console.error('Произошла ошибка:', error);
//    });
//});