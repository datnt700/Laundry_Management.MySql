import { Link, Outlet, useNavigate } from "react-router-dom";
import {  Button } from "@mui/material";
import removeCookie from "../hooks/removeCookie";

export default function MainLayout() {
    let navigate = useNavigate();
    const logOut = () =>{
        removeCookie("usrin", { path: '/'});
        navigate("/");
    }
  return (
    <>
      <nav className="nav-bar">
        <ul>
          <li>
            <Link to="/"> Home</Link>
          </li>
          <li>
            <Link to="signup"> Sign Up</Link>
          </li>
          <li>
            <Link to="login"> Log In</Link>
          </li>
          <li>
            <Link to="machines"> Machine</Link>
          </li>
          <li>
              <Button onClick={logOut}> Log Out</Button>
            
          </li>
        </ul>
      </nav>
      <Outlet />
    </>
  );
}
