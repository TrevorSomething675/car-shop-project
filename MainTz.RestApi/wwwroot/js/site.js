//var token = localStorage.getItem('token');

//const button = document.querySelector('.adminbtn');
//button.addEventListener('click', (event) => {
//    event.preventDefault();
//    const url = button.getAttribute('href');
//    const requestToken = token;
//    const xhr = new XMLHttpRequest();

//    xhr.open('GET', url);
//    xhr.setRequestHeader('Authorization', 'Bearer ' + token);

//    xhr.send();
//    xhr.onload = function (event) {
//        document.body.innerHTML = xhr.responseText;
//    }
//})

//function SendGetRequest(url){
//    fetch(url, {
//        method: 'GET',
//        headers: {
//            'Authorization': 'Bearer ' + token,
//            'Content-Type': 'application/json'
//        }
//    }).then(response => {
//        if (!response.ok) {
//            throw new Error('Network response was not ok');
//        } else {
//            console.log(response.body);
//            return response.json();
//        }
//    }).then(data => {
//        console.log(data);
//    }).catch(error => {
//        console.error('There has been a problem with your fetch operation:', error);
//    });
//};