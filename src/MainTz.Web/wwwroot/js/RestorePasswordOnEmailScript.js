const restorePasswordForm = document.querySelector('#RestorePasswordForm');

restorePasswordForm.addEventListener('submit', function (event) {
    event.preventDefault();
    fetch(restorePasswordForm.action, {
        method: restorePasswordForm.method,
        body: new FormData(restorePasswordForm),
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                console.log('Ошибка при получении запроса');
            }
        })
        .then(data => {
            if (data.errorMessage != '' && data.errorMessage !== undefined) {
                $.ajax({
                    url: "ErrorViewPartial",
                    type: 'GET',
                    data: { error: data.errorMessage },
                    success: function (response) {
                        $('#RestoreMessageContainer').html(response);
                        return;
                    },
                })
            }
            if (data.message !== '' && data.message !== undefined) {
                $.ajax({
                    url: "InfoViewPartial",
                    type: 'GET',
                    data: { message: data.message },
                    success: function (response) {
                        $('#RestoreMessageContainer').html(response);
                        return;
                    }
                })
            }
        })
});