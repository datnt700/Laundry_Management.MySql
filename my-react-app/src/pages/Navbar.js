import React, {useState} from 'react'
import Logo from '../Assets/Images/logo.png'
import { Link,Outlet, useNavigate } from 'react-router-dom'
import '../Assets/Styles/css/NavBar.css'
import AddIcon from '@mui/icons-material/Add';
import { Button } from '@mui/material';
import removeCookie from '../hooks/removeCookie';

function Navbar() {
    const[openLinks, setOpenLinks] = useState(false)

    const toggleNavbar = () => {
        setOpenLinks(!openLinks)
    }

    let navigate = useNavigate();

    const logOut = () => {
        removeCookie("token")
        console.log("LogOut Successfully")
        navigate("/")

    }
  return (
    <div className='navbar'>
        <div className='leftSide' id ={openLinks ?  "open" : "close"}>
        <img src={Logo} />
        <div className='hiddenLinks'>
            <Link to="/">Home</Link>
            <Link to="/machine">Machine</Link>
            <Link to="/login">Login</Link>
            <Link to="/" onClick={logOut}>Logout</Link>
        </div>
        </div>
        <div className='rightSide'>
        <Link to="/">Home</Link>
            <Link to="/machine">Machine</Link>
            <Link to="/login">Login</Link>
            <Link to="/" onClick={logOut}>Logout</Link>
            <Button onClick={toggleNavbar}>
                <AddIcon/>
            </Button>
        </div>
        <Outlet />
    </div>

  );
}

export default Navbar;