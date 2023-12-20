const sendNotificationButtons = document.querySelectorAll('#sendNotificationDescription');

sendNotificationButtons.forEach(button => {
    button.addEventListener('click', () => {
        const notificationId = button.getAttribute('data-id');

        const xhr = new XMLHttpRequest();
        xhr.open('GET', `/Account/GetNotificationById/${notificationId}`);
        xhr.onload = function () {
            if (xhr.status === 200) {
                console.log(xhr.responseText);
                document.getElementById('NotificationContainer').innerHTML = xhr.responseText;
            } else {
                console.error('Произошла ошибка:', xhr.status);
            }
        };
        xhr.onerror = function () {
            console.error('Произошла ошибка при выполнении запроса.');
        };
        xhr.send();
    });
});