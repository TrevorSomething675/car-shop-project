$(document).ready(function () {
    var image1 = $('#LittleImage1').children('img');
    var image2 = $('#LittleImage2').children('img');
    var image3 = $('#LittleImage3').children('img');

    var $image1 = image1.clone().removeClass('big-car-card-little-image').addClass('product-img');
    var $image2 = image2.clone().removeClass('big-car-card-little-image').addClass('product-img');
    var $image3 = image3.clone().removeClass('big-car-card-little-image').addClass('product-img');

    image1.on('click', function () {
        $('#MainCarImage').children('img').remove();
        $('#MainCarImage').append($image1);
    });
    image2.on('click', function () {
        $('#MainCarImage').children('img').remove();
        $('#MainCarImage').append($image2);
    });
    image3.on('click', function () {
        $('#MainCarImage').children('img').remove();
        $('#MainCarImage').append($image3);
    });
});