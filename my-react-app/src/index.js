import React from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom"
import { BrowserRouter as Router, Switch, Route, Link, Routes, useRoutes  } from 'react-router-dom';
import { StrictMode } from "react";
import ReactDOM from "react-dom";

import App from "./App";

const container = document.getElementById("root");
const root = createRoot(container);
root.render(  <App />);
