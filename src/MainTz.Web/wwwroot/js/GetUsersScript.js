function GetUsers(isSortedByRole) {
    $(document).ready(function () {
        $.ajax({
            url: '/User/GetUsersPartial',
            type: 'POST',
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ IsSortedByRole: isSortedByRole }),
            success: function (data) {
                console.log(data);
                $('#UsersContainer').html(data);
            }
        });
    });
}