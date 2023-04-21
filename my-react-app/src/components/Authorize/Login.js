import React, { useEffect } from 'react';
import { useState } from 'react';
import axios from 'axios';
import { Logout, saveTokenInLocalStorage } from '../../util/requestURL';
import Cookies from 'js-cookie';
import setCookie from '../../hooks/setCookie';
import getCookie from '../../hooks/getCookie';
import removeCookie from '../../hooks/removeCookie';
import useCookies from 'react-cookie/cjs/useCookies';
import '../Authorize/indexAuth.css';
import styled from 'styled-components';
import { Box, TextField } from '@mui/material';
import instance from '../../util/requestURL';
import autAPI from '../../util/authAPI';
import { useNavigate } from 'react-router-dom';
import API from '../../util/APIConstanst';
import { AxiosPost } from '../../util/requestURL';
import Button from '@mui/material/Button';
import ErrorModal from './Error';
const Wrapper = styled.div`
  height: 450px;
  position: relative;
`;

const BoxLogin = styled.div`
  height: 50%;
  width: 25%;
  position: relative;
  left: 33%;
  top: 29%;
  border: 1px solid;
  display: flex;
  flex-direction: column;
`;

const TextInput = styled(TextField)`
  position: absolute;
  top: 30px;
`;

const TextButton = styled(Button)`
  top: 10px;
  left: 120px;
  width: 90px;
  height: 40px;
`;

export default function Login() {
  const [phone, setPhone] = useState('');
  const [password, setPassword] = useState('');
  const [username, setName] = useState('');
  const [errorMessages, setErrorMessages] = useState({});

  const [isLogin, setIsLogin] = useState(true);

  const [login, setLogin] = useState('');
  const [register, setRegister] = useState('');

  const errors = {
    message: 'Login failed, please try again',
  };

  const handleName = (e) => {
    setName(e.target.value);
  };

  const handlePhone = (e) => {
    setPhone(e.target.value);
  };

  const handlePassword = (e) => {
    setPassword(e.target.value);
  };

  const handleLoginApi = async () => {
    await fetchLogin();
  };

  const navigate = useNavigate();

  const fetchLogin = async () => {
    try {
      const response = await AxiosPost(API.LOGIN, {
        Phone: phone,
        Password: password,
      });
      if (!response) {
        console.log('login looxi');
        setErrorMessages({ message: errors.message });
      }
      console.log('Login successfully: ', response);
      setLogin(response);
      console.log(response);
      setCookie('token', response.Token);
      setPhone('');
      setPassword('');
      navigate('/');
    } catch (error) {
      console.log('Login Failed:', error);
    }
  };

  const handleRegisterApi = () => {
    fetchRegister();
  };

  const fetchRegister = async () => {
    try {
      const response = await AxiosPost(API.REGISTER, {
        username,
        phone,
        password,
      });
      console.log('Register Successfully', response);
      setRegister(response);
      setCookie('token', response.Token);
      setName('');
      setPhone('');
      setPassword('');
      navigate('/login');
    } catch (error) {
      console.log('Login Failed:', error);
    }
  };

  const renderErrorMessage = () => (
    <div className="error">{errorMessages.message}</div>
  );

  return (
    <Wrapper>
      {isLogin ? (
        <div className="container mt-5">
          <div className="row-login">
            <div className="col">
              <div className="card mx-auto">
                <div className="card-body">
                  <h1
                    className="card-title"
                    style={{ borderBottom: '1px solid #efefef' }}
                  >
                    React Login Form
                  </h1>
                  <form>
                    <div className="form-group">
                      <label htmlFor="exampleInputEmail1">Phone Number</label>
                      <input
                        type="number"
                        name="phone"
                        className="form-control"
                        id="exampleInputEmail1"
                        aria-describedby="phoneHelp"
                        placeholder="Enter email"
                        value={phone}
                        onChange={handlePhone}
                      />
                      <small id="phoneHelp" className="form-text text-muted">
                        We'll never share your number with anyone else.
                      </small>
                    </div>
                    <div className="form-group">
                      <label htmlFor="exampleInputPassword1">Password</label>
                      <input
                        type="password"
                        name="password"
                        className="form-control"
                        id="exampleInputPassword1"
                        placeholder="Password"
                        value={password}
                        onChange={handlePassword}
                      />
                      {renderErrorMessage()}
                    </div>
                    <TextButton
                      type="button"
                      variant="contained"
                      color="success"
                      onClick={handleLoginApi}
                    >
                      Login
                    </TextButton>
                    <button
                      type="button"
                      className="btn btn-secondary right"
                      onClick={() => {
                        setIsLogin(false);
                      }}
                    >
                      Register
                    </button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      ) : (
        <div className="container mt-5">
          <div className="row-register">
            <div className="col">
              <div className="card mx-auto">
                <div className="card-body">
                  <h1
                    className="card-title"
                    style={{ borderBottom: '1px solid #efefef' }}
                  >
                    React Register Form
                  </h1>
                  <form className="needs-validation">
                    <div className="form-group">
                      <label htmlFor="exampleInputPassword1">Username</label>
                      <input
                        type="text"
                        name="name"
                        className="form-control"
                        id="exampleInputPassword1"
                        required
                        placeholder="Name"
                        value={username}
                        onChange={handleName}
                      />
                    </div>
                    <div className="form-group">
                      <label htmlFor="exampleInputEmail1">Phone Number</label>
                      <input
                        type="number"
                        name="phone"
                        className="form-control"
                        id="exampleInputEmail1"
                        aria-describedby="phoneHelp"
                        required
                        placeholder="Enter email"
                        value={phone}
                        onChange={handlePhone}
                      />
                      <small id="phoneHelp" className="form-text text-muted">
                        We'll never share your number with anyone else.
                      </small>
                    </div>
                    <div className="form-group">
                      <label htmlFor="exampleInputPassword1">Password</label>
                      <input
                        type="password"
                        name="password"
                        className="form-control"
                        id="exampleInputPassword1"
                        required
                        placeholder="Password"
                        value={password}
                        onChange={handlePassword}
                      />
                    </div>
                    <Button
                      type="button"
                      variant="contained"
                      onClick={() => {
                        handleRegisterApi();
                        setIsLogin(true);
                      }}
                    >
                      Submit
                    </Button>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </Wrapper>
  );
}
