$(document).ready(function () {
    $.ajax({
        url: '/FavoriteCar/ChangeFavoriteCar',
        type: 'POST',
        dataType: 'html',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(id),
        success: function (data) {
            
        },
        error: function () {
            alert('error');
        }
    })
});