var notificationBtn = $('#notifications-item-btn');
var notificationEventBtn = $('#notifications-event-btn');
var favoriteBtn = $('#favorite-item-btn');
var accountBtn = $('#account-item-btn');
var getCarsHeaderBtn = $('#GetCarsHeaderBtn');
$(document).ready(function () {
    notificationEventBtn.click(function () {
        sessionStorage.setItem('IsNotificationsPage', true);
        sessionStorage.setItem('IsFavoritePage', false);
        sessionStorage.setItem('IsAccountPage', false);
    })
    favoriteBtn.click(function () {
        sessionStorage.setItem('IsNotificationsPage', false);
        sessionStorage.setItem('IsFavoritePage', true);
        sessionStorage.setItem('IsAccountPage', false);
    })
    accountBtn.click(function () {
        sessionStorage.setItem('IsNotificationsPage', false);
        sessionStorage.setItem('IsFavoritePage', false);
        sessionStorage.setItem('IsAccountPage', true);
    })
});

$(document).ready(function () {
    let isNotificationsPage = sessionStorage.getItem('IsNotificationsPage');
    let isFavoritePage = sessionStorage.getItem('IsFavoritePage');
    let isAccountPage = sessionStorage.getItem('IsAccountPage');
    if (isNotificationsPage == 'true') {
        notificationBtn.hide();
    }
    if (isFavoritePage == 'true') {
        favoriteBtn.hide();
    }
    if (isAccountPage == 'true') {
        accountBtn.hide();
    }
});

getCarsHeaderBtn.click(function () {
    sessionStorage.setItem('IsNotificationsPage', false);
    sessionStorage.setItem('IsFavoritePage', false);
    sessionStorage.setItem('IsAccountPage', false);
});