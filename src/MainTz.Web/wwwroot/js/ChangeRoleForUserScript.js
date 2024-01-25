function ChangeFavoriteCarById(id) {
    $(document).ready(function () {
        console.log(id);
        $.ajax({
            url: '/User/ChangeUserRole',
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(id),
            success: function (data) {
                let userContainer = $(`#userContainer_${id}`);
                userContainer.html(data);
            },
            error: function () {
                alert('error');
            }
        })
    })
}