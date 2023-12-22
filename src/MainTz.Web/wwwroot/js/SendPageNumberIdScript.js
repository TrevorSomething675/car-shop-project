function SendCarsPageNumber(pageNumber) {
    console.log(pageNumber);
    $(document).ready(function () {
        $.ajax({
            url: '/Car/GetCarsPartial',
            type: 'POST',
            dataType: 'html',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(pageNumber),
            success: function (response) {
                $('#CarsContainer').html(response);
            },
            error: function () {
                alert('error');
            }
        })
    });
}