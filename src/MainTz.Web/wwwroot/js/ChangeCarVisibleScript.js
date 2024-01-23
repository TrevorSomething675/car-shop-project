function ChangevisibleCar(id) {
    console.log(id);
    fetch("/Car/ChangeCarVisible", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(id)
    });
}