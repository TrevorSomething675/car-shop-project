const loginForm = document.querySelector('#LoginForm');
var accessToken = GetCookie('accessToken');

loginForm.addEventListener('submit', function (event) {
    event.preventDefault();
    const loginField = document.querySelector('#loginField').value;

    let url;
    if (/\S+@\S+\.\S+/.test(loginField)) {
        url = '/Auth/LoginMail';
    } else {
        url = '/Auth/LoginNormal';
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
            if (data.errorMessage !== '') {
                console.log(`Возникла ошибка: ${data.errorMessage}`);
                $.ajax({
                    url: '/Message/ErrorViewPartial',
                    type: 'GET',
                    data: { error: data.errorMessage },
                    success: function (response) {
                        $('#LoginErrorContainer').html(response);
                        return
                    },
                })
            }
            if (data != '') {
                document.cookie = `accessToken=${data.value.accessToken}; path=/`;
                document.cookie = `refreshToken=${data.value.refreshToken}; path=/`;
                document.cookie = `role=${data.value.role}; path=/`;
                console.log(`Access token: ${data.value.accessToken} Refresh token: ${data.value.refreshToken} Role: ${data.value.role}`);
                let lastOpenedCarCard = GetCookie('LastOpenedCarCard');
                if (lastOpenedCarCard != undefined) {
                    console.log(document.location.href);
                    document.location.href = `/Car/GetBigCarCard?id=${lastOpenedCarCard}`;
                } else {
                    document.location.href = `/Car/GetCars`;
                }
            } else {
                console.log('Данные не найдены в ответе сервера')
            }
        })
        .catch(error => {
            console.error('Произошла ошибка:', error);
        });
});