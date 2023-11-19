const token = localStorage.getItem('token');

$.ajax({
    url: 'Admin',
    type: 'GET',
    beforeSend: function (xhr) {
        xhr.setRequestHeader('Authorization', 'Bearer ' + token);
    },
    success: function (data) {
        // Обработка успешного ответа от сервера
        console.log(data);
    },
    error: function (error) {
        // Обработка ошибки
        console.log(error);
    },
    complete: function (xhr, status) {A
        console.log('Request complete. Status:', status);
    },
    statusCode: {
        401: function () {
            console.log('Unauthorized');
        },
        // Добавьте другие коды состояния, которые вам интересны
    }
});