//var token = localStorage.getItem('token');
//var doc = $(document);

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
//            return response/*.json()*/;
//        }
//    }).then(data => {
//        console.log(data);
//    }).catch(error => {
//        console.error('There has been a problem with your fetch operation:', error);
//    });
//};