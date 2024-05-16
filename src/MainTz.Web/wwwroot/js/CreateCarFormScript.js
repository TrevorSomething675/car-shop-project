$(document).ready(function () {
    var creeateCarForm = document.getElementById('SelectImageForCreateCar');
    if (creeateCarForm != undefined) {
        creeateCarForm.addEventListener('change', function () {
            var file = this.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('PreviewImageFormContainer').setAttribute('src', e.target.result);
            }
            reader.readAsDataURL(file);
        });
    }

    const createCarForm = document.querySelector('#CreateCarForm');
    if (createCarForm != undefined) {
        createCarForm.addEventListener('submit', async function (event) {
            event.preventDefault();

            let formData = new FormData(createCarForm);
            let reader = new FileReader();

            let input = document.getElementById('UpdateImageCarInput');
            let files = input.files;

            for (let i = 0; i < files.length; i++) {
                let file = files[i];

                let readFilePromise = new Promise((resolve, reject) => {
                    reader.onload = function (event) {
                        resolve(event.target.result);
                    };
                    reader.onerror = function (event) {
                        reject(event.target.error);
                    };
                });
                reader.readAsDataURL(file);
                let resultBase64String = await readFilePromise;

                formData.append(`carRequest.Images[${i}].Name`, file.name);
                formData.append(`carRequest.Images[${i}].FileBase64String`, resultBase64String);

            }


            fetch("/Car/CreateCarCommand", {
                method: createCarForm.method,
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
                        document.location.href = `/Car/GetBigCarCard?id=${newCarId}`
                    }
                })
        })
    }
});