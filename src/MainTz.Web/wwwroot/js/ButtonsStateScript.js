//var notificationBtn = $('#notifications-item-btn');
//var notificationEventBtn = $('#notifications-event-btn')
//var favoriteBtn = $('#favorite-item-btn');
//var accountBtn = $('#account-item-btn');

//$(document).ready(function () {
//    notificationEventBtn.click(function () {
//        sessionStorage.setItem('IsNotificationsPage', true);
//        sessionStorage.setItem('IsFavoritePage', false);
//        sessionStorage.setItem('IsAccountPage', false);
//    })
//    favoriteBtn.click(function () {
//        sessionStorage.setItem('IsNotificationsPage', false);
//        sessionStorage.setItem('IsFavoritePage', true);
//        sessionStorage.setItem('IsAccountPage', false);
//    })
//    accountBtn.click(function () {
//        sessionStorage.setItem('IsNotificationsPage', false);
//        sessionStorage.setItem('IsFavoritePage', false);
//        sessionStorage.setItem('IsAccountPage', true);
//    })
//});

//$(document).ready(function () {
//    var isNotificationsPage = sessionStorage.getItem('IsNotificationsPage');
//    var isFavoritePage = sessionStorage.getItem('IsFavoritePage');
//    var isAccountPage = sessionStorage.getItem('IsAccountPage');
//    if (isNotificationsPage == 'true') {
//        notificationBtn.hide();
//    }
//    if (isFavoritePage == 'true') {
//        favoriteBtn.hide();
//    }
//    if (isAccountPage == 'true') {
//        accountBtn.hide();
//    }
//});