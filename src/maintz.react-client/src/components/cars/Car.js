import { useEffect, useState } from "react";
import axios from "axios";
import './Car.css'

function Car({name, description, Price, imagesId}){
    useEffect(() => {
        axios.get(`https://localhost:7075/Image/GetImageById?id=${imagesId}`)
        .then(response => {
            let imageBase64 = `data:image/png;base64, ${response.data.fileBase64String}`;
            setImage(imageBase64);
        }).catch(error => {
            console.log(error);
        });
    })

    const [imageBase64Src, setImage] = useState(null);

    return(
        <div className="col-xxl-3 col-xl-4 col-md-6 col-sm-12 ">
            <div className="product-container m-1 p-1">
                <div>
                    <img src={imageBase64Src}/>
                </div>
                <h1>
                    {name}
                </h1>
                <div className="product-description text-break">
                    {description}
                </div>
            </div>
        </div>
    )
}

export default Car;