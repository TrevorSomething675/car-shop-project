document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('#AuthForm');

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        fetch(form.action, {
            method: form.method,
            body: new FormData(form)
        })
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Ошибка при отправке формы');
                }
            })
            .then(data => {
                if (data != '') {
                    document.cookie = `accessToken=${data.accessToken}`;
                    document.cookie = `refreshToken=${data.refreshToken}`;
                    console.log(`Access token: ${data.accesstoken} Refresh token: ${data.refreshtoken}`);
                    if (data.role != '') {
                        window.location.href = window.location.href + `${data.role}/Index`;
                    }
                } else {
                    throw new Error('Токен не найден в ответе сервера');
                }
            })
            .catch(error => {
                console.error('Произошла ошибка:', error);
            });
    });
});