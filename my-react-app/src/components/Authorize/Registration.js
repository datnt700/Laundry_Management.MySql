import React, { Fragment } from 'react';
import {useState} from 'react';
import { button } from 'react-bootstrap';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import "../Authorize/indexAuth.css"


function Registration() {
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');
  const [password, setPassword] = useState('');
  console.log({ name, phone, password });

  const handleName = (e) => {
    setName(e.target.value);
  };

  const handlePhone = (e) => {
    setPhone(e.target.value);
  };

  const handlePassword = (e) => {
    setPassword(e.target.value);
  };

  const handleRegisterApi = () => {
    console.log({ name, phone, password });
    axios
      .post('https://localhost:7273/api/Auth/register', {
        username: name,
        phone: phone,
        password: password,
      })
      .then((result) => {
        // saveTokenInLocalStorage(result.data)
        console.log(result);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <div className="main">
      <input type="checkbox" id="chk" aria-hidden="true" />
          <div className="signup">
            <form>
             <label htmlFor="chk" aria-hidden="true" oncl>
            Sign Up
              </label>
             <input value={name}
                      type="text"

                      placeholder="Username"
                      onChange={handleName} required />
            <input value={phone}
                      type="text"

                      placeholder="Phone Number"
                      onChange={handlePhone} required />
            <input value={password}
                      type="text"
                      placeholder="Password"
                      onChange={handlePassword} required />
          </form>
            <button onClick={handleRegisterApi}>Register</button>
        </div>
      <div className="login" >
        <form>
          <label htmlFor="chk" aria-hidden="false" >
            Login
            
          </label>
          <input
            value={phone}
         
            type="text"
            placeholder="Phone Number"
          />
          <input
            value={password}
           
            type="text"
            placeholder="Password"
          />
        </form>
        <button>Login</button>
      </div>
    </div>
  );
}

export default Registration;
