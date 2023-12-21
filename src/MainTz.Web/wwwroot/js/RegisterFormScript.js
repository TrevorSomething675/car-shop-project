const registerForm = document.querySelector('#RegisterForm');
registerForm.addEventListener('submit', function (event) {
    event.preventDefault();

    fetch(registerForm.action, {
        method: registerForm.method,
        body: new FormData(registerForm)
    })
        .then(response => {
            return response.json();
        })
        .then(data => {
            if (data.errorMessage != '' && data.errorMessage !== undefined) {
                $.ajax({
                    url: "/Message/ErrorViewPartial",
                    type: 'GET',
                    data: { error: data.errorMessage },
                    success: function (response) {
                        $('#RegisterErrorContainer').html(response);
                        return;
                    },
                })
            }
            if (data != '' && response.Ok) {
                document.cookie = `accessToken=${data.value.accessToken}`;
                document.cookie = `refreshToken=${data.value.refreshToken}`;
                document.cookie = `role=${data.value.role}`;
                console.log(`Access token: ${data.value.accesstoken} Refresh token: ${data.value.refreshtoken} Role: ${data.value.role}`);
                let lastOpenedCarCard = GetCookie('LastOpenedCarCard');
                if (lastOpenedCarCard != undefined) {
                    console.log(document.location.href);
                    document.location.href = `/User/CarBigCard?id=${lastOpenedCarCard}`;
                } else {
                    document.location.pathname = `${data.value.role}/Index`;
                }
            } else {
                console.log('Данные не найдены в ответе сервера')
            }
        })
        .catch(error => {
            console.error('Произошла ошибка:', error);
        });
});