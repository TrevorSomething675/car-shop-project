var token = localStorage.getItem('token');

fetch('', {
    method: 'GET',
    headers: {
        'Authorization': 'Bearer ' + token,
    }
});