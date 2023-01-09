import Register from "./components/Authorize/Registration";
import React, { Component } from 'react';
import { BrowserRouter as Router, Switch, Route, Link, Routes, useRoutes  } from 'react-router-dom';
import Registration from "./components/Authorize/Registration";
import SideBar from "./components/SideBar";
import Header from "./components/Header";
import DashBoard from "./components/DashBoard";
import Footer from "./components/Footer";
import Login from "./components/Authorize/Login";
import PublicRoutes from "./routes";
import "./style.css"


function App() {
  return (
    <Router>
    <div className="App">
          <PublicRoutes/>
      </div>

    </Router>
    
   );
}

export default App;

