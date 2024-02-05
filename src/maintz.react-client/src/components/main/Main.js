import {useState} from 'react'
import Cars from '../cars/Cars'
import './Main.css'

function Body(props){
    return (
        <main className="container-fluid">
            <Cars />
        </main>
    )
}

export default Body;