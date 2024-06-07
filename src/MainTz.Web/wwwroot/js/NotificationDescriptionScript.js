﻿function SendNotificationId(id) {
    console.log(id);
    console.log(typeof (id));
    $(document).ready(function () {
        $.ajax({
            url: '/Account/GetNotificationDescription',
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(id),
            success: function (response) {
                $('#NotificationContainer').html(response);
                var btn = $(`#${id}`);
                btn.removeClass('custom-btn-notification-header'); 
                btn.addClass('custom-btn-notification-header-unactive')
            },
            error: function () {
                alert('error');
            }
        })
    });
};