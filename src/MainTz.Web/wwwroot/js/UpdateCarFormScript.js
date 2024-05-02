const removeImagebyId = (imageId) => {
    let redirectUrl = location.href;
    $(document).ready(function () {
        $.ajax({
            url: "/Image/RemoveImageFromCar",
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(imageId),
            success: function (data) {
                let newCarId = data;
                document.location.href = redirectUrl;
            }
        })
    });
}


function previewFile() {
    const preview = document.getElementById('previewImage');
    const file = document.querySelector('input[type=file]').files[0];
    const reader = new FileReader();
    reader.onloadend = function () {
        preview.src = reader.result;
    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
        preview.src = "";
    }
}
$(document).ready(function () {
    let createImageForm = document.getElementById('UpdateImageCarForm');
    if (createImageForm != undefined) {

        createImageForm.addEventListener('submit', async function (event) {
            event.preventDefault();

            let carId = document.getElementById('actualCarId').value;
            let formData = new FormData(createImageForm);
            let image = formData.get('Image');
            let reader = new FileReader();

            let readFilePrimise = new Promise((resolve, reject) => {
                reader.onload = function (event) {
                    resolve(event.target.result);
                };
                reader.onerror = function (event) {
                    reject(event.target.result);
                };
            })
            reader.readAsDataURL(image);
            let resultBase64String = await readFilePrimise;

            formData.append('Image.FileBase64String', resultBase64String);
            formData.append('Image.Name', image.name);
            formData.append('carId', carId);

            fetch("/Image/CreateImageFromCar", {
                method: createImageForm.method,
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
                        document.location.href = `/Car/GetUpdateCar?id=${newCarId}`;
                    }
                })
        })
    }
});
$(document).ready(function () {
    let updateForm = document.getElementById('#UpdateCarForm');
    if (updateForm != undefined)
    {
        updateForm.addEventListener('change', function () {
            this.files.forEach((imageFromForm) => {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('PreviewImageUpdateFormContainer').setAttribute('src', e.target.result);
                }
                reader.readAsDataURL(imageFromForm);
            })
        });
    }

    const updateCarForm = document.querySelector('#UpdateCarForm');
    if (updateCarForm != undefined) {
        updateCarForm.addEventListener('submit', function (event) {
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
            let resultBase64String = readFilePromise;

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
    }
})