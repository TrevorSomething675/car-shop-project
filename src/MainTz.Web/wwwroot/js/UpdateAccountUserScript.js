const updateLoginForm = document.querySelector('#UpdateLoginForm');
const updatePasswordForm = document.querySelector('#UpdatePasswordForm');
updateLoginForm.addEventListener('submit', function (event) {
    event.preventDefault();

    fetch(updateLoginForm.action, {
        method: updateLoginForm.method,
        body: new FormData(updateLoginForm)
    })
    .then(response => {
        if (response.status == 200) {
            document.location.href = "/Auth/LogOut";
        }
        return response.json();
    })
    .then(data => {
        if (data.errorMessage != '' && data.errorMessage !== undefined) {
            $.ajax({
                url: "/Message/ErrorViewPartial",
                type: 'GET',
                data: { error: data.errorMessage },
                success: function (response) {
                    $('#UpdateLoginErrorContainer').html(response);
                    return;
                },
            })
        }
    })
    .catch(error => {
        console.error('Произошла ошибка:', error);
    });
});

updatePasswordForm.addEventListener('submit', function (event) {
    event.preventDefault();

    fetch(updatePasswordForm.action, {
        method: updatePasswordForm.method,
        body: new FormData(updatePasswordForm)
    })
        .then(response => {
            if (response.status == 200) {
                document.location.href = "/Auth/LogOut";
            }
            return response.json();
    })
    .then(data => {
        if (data.errorMessage != '' && data.errorMessage !== undefined) {
            $.ajax({
                url: "/Message/ErrorViewPartial",
                type: 'GET',
                data: { error: data.errorMessage },
                success: function (response) {
                    $('#UpdatePasswordErrorContainer').html(response);
                    return;
                },
            })
        }
    })
    .catch(error => {
        console.error('Произошла ошибка:', error);
    });
});