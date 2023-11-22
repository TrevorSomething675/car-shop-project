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
                const token = data.token;
                if (token) {
                    document.cookie = `token=${token}`;
                    console.log('Токен успешно сохранен в Cookie:', token);
                } else {
                    throw new Error('Токен не найден в ответе сервера');
                }
            })
            .catch(error => {
                console.error('Произошла ошибка:', error);
            });
    });
});