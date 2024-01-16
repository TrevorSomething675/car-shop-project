function SendCarsPageNumber(pageNumber) {
    $(document).ready(function () {
        $.ajax({
            url: '/Car/GetCarsPartial',
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(pageNumber),
            success: function (response) {
                $('#CarsContainer').html(response);
                localStorage.setItem('LastPageNumber', pageNumber);
            },
            error: function () {
                alert('error');
            }
        })
    });
}

function SendFavoriteCarsPageNumber(pageNumber) {
    $(document).ready(function () {
        $.ajax({
            url: '/FavoriteCar/GetFavoriteCarsPartial',
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(pageNumber),
            success: function (response) {
                $('#FavoriteCarsContainer').html(response);
                localStorage.setItem('LastPageNumber', pageNumber);
            },
            error: function () {
                alert('error');
            }
        })
    })
}