function SendNotificationId(id) {
    console.log(id);
    console.log(typeof (id));
    $(document).ready(function () {
        $.ajax({
            url: '/Account/GetNotificationById',
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(id),
            success: function (response) {
                $('#NotificationContainer').html(response);
            },
            error: function () {
                alert('error');
            }
        })
    });
};