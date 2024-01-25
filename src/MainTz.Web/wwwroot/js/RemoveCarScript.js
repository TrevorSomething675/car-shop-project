function RemoveCarById(id) {
    $(document).ready(function () {
        $.ajax({
            url: '/Car/RemoveCarById',
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(id),
            success: function () {
                document.location.href = '/Car/GetCars'
            }
        })
    });
}