import React from 'react'
import { Link } from 'react-router-dom'
import BannerImage from '../Assets/Images/background.jpg'
import "../Assets/Styles/css/Home.css"
function Home() {
  return (
    <div className='home'style={{backgroundImage:`url(${BannerImage})`}}>
        <div className='headerContainer' 
        >
            <div>Dat's laundry</div>
            <p>Hello ajinomoto</p>
            <Link to="/menu">
            <button>Order</button>
            </Link>
        </div>
    </div>
  )
}

export default Home