import React from 'react'
import { Instagram, Facebook, Twitter } from '@mui/icons-material'
import '../Assets/Styles/css/Footer.css'
function Footer() {
  return (
    <div className="container-footer">
      <div className='footer'>
        <div className='socialMedia'>
            <Instagram/>
            <Facebook/>
            <Twitter/>
            </div> 
        <p>&copy; 2023 datbeo</p>
    </div>
    </div>
  )
}

export default Footer