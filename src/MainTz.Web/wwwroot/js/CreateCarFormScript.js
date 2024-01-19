$(document).ready(function()
    {
        const createCarForm = document.querySelector('#CreateCarForm');
        createCarForm.addEventListener('submit', function (event) {
            event.preventDefault();

            fetch("/Car/CreateCarCommand", {
                method: createCarForm.method,
                body: new FormData(createCarForm),
            })
        })
    })