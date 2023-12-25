$(document).ready(function(){
    var backToCarsFromBigCardButton = $('#BackToCarsFromBigCardButton');
    var pageNumber = localStorage.getItem('LastPageNumber')
    console.log(pageNumber);
    backToCarsFromBigCardButton.on('click', function () {
        window.location.href =`/Car/GetCars?pageNumber=${pageNumber}`;
    });
});