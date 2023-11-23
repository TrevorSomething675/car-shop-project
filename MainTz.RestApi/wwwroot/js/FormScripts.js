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
                    console.log('Ошибка при получении запроса');
                }
            })
            .then(data => {
                if (data != '') {
                    document.cookie = `accessToken=${data.value.accessToken}`;
                    document.cookie = `refreshToken=${data.value.refreshToken}`;
                    localStorage.setItem('role', data.value.role);
                    console.log(`Access token: ${data.value.accesstoken} Refresh token: ${data.value.refreshtoken} Role: ${data.value.role}`);
                    window.location.href = window.location.href + `${data.value.role}/Index`; //Перенаправление на страницу, в зависимости от роли
                } else {
                    console.log('Данные не найдены в ответе сервера')
                }
            })
            .catch(error => {
                console.error('Произошла ошибка:', error);
            });
    });
});