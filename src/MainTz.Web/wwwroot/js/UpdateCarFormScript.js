$(document).ready(function () {
    document.getElementById('SelectImageForUpdateCar').addEventListener('change', function () {
        var file = this.files[0];
        var reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('PreviewImageUpdateFormContainer').setAttribute('src', e.target.result);
        }
        reader.readAsDataURL(file);
    });

    const updateCarForm = document.querySelector('#UpdateCarForm');
    updateCarForm.addEventListener('submit', async function (event) {
        event.preventDefault();

        let formData = new FormData(updateCarForm);
        let requestImage = formData.get('CarRequest.Image');
        let reader = new FileReader();

        let readFilePromise = new Promise((resolve, reject) => {
            reader.onload = function (event) {
                resolve(event.target.result);
            };
            reader.onerror = function (event) {
                reject(event.target.error);
            };
        });
        reader.readAsDataURL(requestImage);
        let resultBase64String = await readFilePromise;

        formData.append('carRequest.Images[0].FileBase64String', resultBase64String);
        formData.append('carRequest.Images[0].Name', requestImage.name);

        console.log(resultBase64String);
        fetch("/Car/UpdateCarCommand", {
            method: updateCarForm.method,
            body: formData
        })
            .then(response => {
                if (response) {
                    return response.json();
                }
            })
            .then(data => {
                if (data.errorMessage !== undefined) {
                    console.log(data.errorMessage);
                }
                else {
                    let newCarId = data;
                    document.location.href = `/Car/GetBigCarCard?id=${newCarId}`;
                }
            })
    })
})