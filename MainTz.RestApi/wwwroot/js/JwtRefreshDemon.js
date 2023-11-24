function RefreshTokens() {
    var role = localStorage.getItem('role');
    var accessToken = GetCookie('accessToken');
    var refreshToken = GetCookie('refreshToken');



    fetch('../Auth/GetToken', {
        method: "POST",
        headers: {
            "Accept": "application/json", "Content-Type": "application/json"
        },
        body: JSON.stringify({role: role})            
        })
        .then(response => {
            if (response.ok) {
                response.json();
            } else {
                console.log('Ошибка при получении ответа');
            }
        })
        .then(data => {
            if (data) {
                document.cookie = `accessToken=${data.accessToken}`;
                document.cookie = `refreshToken=${data.refreshToken}`;
                localStorage.setItem('role', data.role);
            } else {
                console.log('Данные не найдены в ответе сервера');
            }
        })
        .catch(error => {
            console.log('Произошла ошибка:', error);
        });
}

function GetCookie(key) {
    var cookies = document.cookie.split(';');
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.indexOf(key + '=') === 0) {
            return cookie.substring(key.length + 1);
        }
    }
}