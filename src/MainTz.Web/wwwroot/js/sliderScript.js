$(document).ready(function () {
    $(".owl-carousel").owlCarousel({
        nav: true,
        margin: 20
    });
});

var user = "founder";
var clanName = "superClan";
var SuspensionDate = Date.now;
$.ajax({
    url: '@Url.Action("SuspendUserFromClan", "ChatMethods")',
    type: "POST",
    contentType: "application/json; charset=utf-8",
    data: { 'ClanName': clanName, 'UserToSuspend': userToAdd, 'DateSuspendedTill': SuspensionDate },
    dataType: "json",
    traditional: true,
    success: function (data, status, xhr) {
        alert(data);
    },
    error: function () {
        alert("An error has occured!!!");
    }
});

1

var SuspensionDate = new Date();
$('#SuspendUser').on("click", function () {

    $.ajax({
        url: '@Url.Action("SuspendUserFromClan", "ChatMethods")',
        type: "POST",
        // contentType: "application/json; charset=utf-8",
        data: { 'ClanName': clanName, 'UserToSuspend': userToAdd, 'DateSuspendedTill': SuspensionDate.toUTCString() },
        dataType: "json",
        traditional: true,
        success: function (data, status, xhr) {
            alert(data);
        },
        error: function () {
            alert("An error has occured!!!");
        }
    });
});