import React, { Component } from 'react';
import { BrowserRouter as Router, Switch, Route, Link, Routes, useRoutes,Outlet  } from 'react-router-dom';
import Navbar from "./pages/Navbar";
import Footer from "./pages/Footer";
import Home from "./pages/Home";
import MachineList from "./components/Machine/MachineList";
import MachineAdd from './components/Machine/MachineAdd';


import PublicRoutes from "./routes";
import Login from "./components/Authorize/Login";

import "./style.css"
import MachineDelete from './components/Machine/MachineDelete';

function App() {
  return (
    <div className="App">
      <Router>
      
        <Navbar />
    
        <PublicRoutes/>
        
        <Footer />
      
      
    </Router>
    </div>
   );
}

export default App;

