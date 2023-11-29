//const refreshTimerForJwtToken = setInterval(RefreshTokens, 50000);

function RefreshTokens() {
    var role = GetCookie('role');
    var accessToken = GetCookie('accessToken');
    var refreshToken = GetCookie('refreshToken');
    if (role !== null) {

        fetch('../Auth/GetToken', {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(role)
        })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                console.log('Ошибка при получении запроса');
            }
        })
        .then(data => {
            if (data) {
                document.cookie = "";
                document.cookie = `accessToken=${data.accessToken}`;
                document.cookie = `refreshToken=${data.refreshToken}`;
                document.cookie = `role=${data.value.role}`;
            } else {
                console.log('Данные не найдены в ответе сервера');
            }
        })
        .catch(error => {
            console.log('Произошла ошибка:', error);
        });
    }
}



