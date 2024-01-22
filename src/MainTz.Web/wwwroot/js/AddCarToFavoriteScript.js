let ChangeCarInFavorite = (id) => {
    $(document).ready(function () {
        $.ajax({
            url: '/FavoriteCar/ChangeFavoriteCar',
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(id),
            success: function () {
                let btn = $(`#ChangeCarInFavorite_${id}`);
                btn.removeClass('car-favorite-offer-btn');
                btn.addClass('car-red-offer-btn');
            },
            error: function () {
                alert('error');
            }
        })
    })
}