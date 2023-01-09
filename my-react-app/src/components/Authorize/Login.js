import React, { useEffect } from "react";
import { useState } from "react";
import axios from "axios";
import { Logout, saveTokenInLocalStorage } from "../../util/requestURL";
import Cookies from "js-cookie";
import setCookie from "../../hooks/setCookie";
import getCookie from "../../hooks/getCookie";
import removeCookie from "../../hooks/removeCookie";
import useCookies from "react-cookie/cjs/useCookies";
import "../Authorize/indexAuth.css"
import styled from 'styled-components';
import { Box, Button, TextField } from "@mui/material";
import getLoginAPI from "../../util/router";
import instance from "../../util/requestURL";
import autAPI from "../../util/authAPI";
import { useNavigate } from "react-router-dom";


const Wrapper = styled.div`
height:100vh;
    position : relative;
`;

const BoxLogin = styled.div`
height : 50%;
width:25%;
position: relative;
left: 33%;
top: 29%;
border: 1px solid;
display: flex;
flex-direction: column;
`


const TextInput = styled(TextField)`
position: absolute;
top: 30px;

`

const TextButton = styled(Button)`
top: 80px;


`

export default function Login() {
  const [phone, setPhone] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");
  
  const [isLogin,setIsLogin] = useState(true);
  

  // useEffect(() => {
  //   const fetchData = async ()=>{
  //     const dataLogin = await getLoginAPI();
  //     console.log(dataLogin);
  //   };
  //   fetchData()
  // },[]);

  // useEffect(() => {
  //   async function fetchData() {
  //     const request = await axios.post('login')
  //   }
  //   fetchData();
  // },[])

  
  
  const [login, setLogin] = useState("");
  const [register, setRegister] = useState("");
  
  console.log({name, phone, password });

  const handleName = (e) => {
    setName(e.target.value);
  };

  const handlePhone = (e) => {
    setPhone(e.target.value);
  };

  const handlePassword = (e) => {
    setPassword(e.target.value);
  };

  const handleLoginApi = () => {
    fetchLogin()
  };

  const fetchLogin = async () => {
    try{    
      const response = await autAPI.login(phone, password);
      console.log('Login successfully: ', response);
      setLogin(response);
      setPhone('');
      setPassword('');
      setCookie("usrin", JSON.stringify(response));

    }catch (error) {
      console.log('Login Failed:', error)
    }
    
    // instance
    //   .post('Auth/login', {
    //     phone: phone,
    //     password: password,
    //   })
    //   .then((result) => {
    //     // saveTokenInLocalStorage(result.data)
    //     setCookie("usrin", JSON.stringify(result.data));

    //     console.log(result);
    //   })
    //   .catch((error) => {
    //     console.log(error);
    //   });
  };

  const handleRegisterApi = () => {
    fetchRegister()
  };

  const fetchRegister = async () => {
    try{
      const response = await autAPI.register(name,phone,password);
      console.log('Register Successfully', response);
      setRegister(response);
      setCookie("usrin", JSON.stringify(response));
      setName('');
    }catch(error){
      console.log('Login Failed:', error)
    }
  }


  
  return (
    <Wrapper>
      {isLogin ?<BoxLogin>
     LOGIN
     <TextInput value={phone} type="text" onChange={handlePhone} id="input-with-sx" label="Phone Number" variant="standard" autoFocus/>
     <TextInput value={password} onChange={handlePassword} type="text" id="input-with-sx" label="Passwrod" variant="standard" />
     <TextButton onClick={ () => {handleLoginApi();setIsLogin(false)}}>dang nhap</TextButton>

   </BoxLogin>:<BoxLogin>
   Register   
   
     <TextInput value={name} onChange={handleName} type="text" id="input-with-sx" label="User Name" variant="standard" />
     <TextInput value={phone} onChange={handlePhone} type="text" id="input-with-sx" label="Phone Number" variant="standard" />

     <TextInput value={password} onChange={handlePassword} type="text" id="input-with-sx" label="Passwrod" variant="standard" />
     <TextButton onClick={() =>{handleRegisterApi();setIsLogin(true)}}>dang ki</TextButton>
   </BoxLogin> }
      
    </Wrapper>
  );
}
