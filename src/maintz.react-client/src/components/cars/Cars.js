import {useState, useEffect} from 'react'
import Car from './Car'
import axios from 'axios'


function Cars(){
    useEffect(() => {
        axios.get('https://localhost:7075/Car/GetCars')
        .then((response) => {
            setCars(response.data.carsResponse);
        })
        .catch(error => console.log(error));
    }, [])

    const [contentIsLoad, setIsLoad] = useState(false);
    const [cars, setCars] = useState([]);
    return(
        <div className="row">
            {cars.map((car)=>(
                <Car key={car.id} {...car}/>
            ))}
        </div>
    )
}

export default Cars;