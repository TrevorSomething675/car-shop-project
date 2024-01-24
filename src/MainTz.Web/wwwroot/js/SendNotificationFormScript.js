$(document).ready(function(){

    var sendNotificationForm = document.querySelector('#SendManualNotificationForm');
    
    sendNotificationForm.addEventListener('submit', function (event) {
        event.preventDefault();
    
        
        fetch("/Account/SendNotificationForUsersOnCarId", {
            method: sendNotificationForm.method,
            body: new FormData(sendNotificationForm)
        })
    })
});