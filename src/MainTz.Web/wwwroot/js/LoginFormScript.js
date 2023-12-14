const loginForm = document.querySelector('#LoginForm');
var accessToken = GetCookie('accessToken');

loginForm.addEventListener('submit', function (event) {
    event.preventDefault();
    const loginField = document.querySelector('#loginField').value;

    let url;
    if (/\S+@\S+\.\S+/.test(loginField)) {
        url = '/Auth/LoginMail'; // замените на нужный вам адрес
    } else {
        url = '/Auth/LoginNormal'; // замените на нужный вам адрес
    }

    fetch(url, {
        method: loginForm.method,
        body: new FormData(loginForm),
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                console.log('Ошибка при получении запроса');
            }
        })
        .then(data => {
            if (data != '') {
                document.cookie = `accessToken=${data.value.accessToken}; path=/`;
                document.cookie = `refreshToken=${data.value.refreshToken}; path=/`;
                document.cookie = `role=${data.value.role}; path=/`;
                console.log(`Access token: ${data.value.accesstoken} Refresh token: ${data.value.refreshtoken} Role: ${data.value.role}`);
                document.location.pathname = `${data.value.role}/Index`;
            } else {
                console.log('Данные не найдены в ответе сервера')
            }
        })
        .catch(error => {
            console.error('Произошла ошибка:', error);
        });
});