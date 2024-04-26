document.addEventListener('DOMContentLoaded', function () {
    var images = document.querySelectorAll('#carsCarousel img');

    images.forEach(function (image) {
        image.addEventListener('click', function () {
            var mainCarImage = document.querySelector('#MainCarImage img');
            mainCarImage.src = this.src;
        });
    });
});