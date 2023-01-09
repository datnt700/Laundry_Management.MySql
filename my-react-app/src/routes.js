import React from "react";
import Login from "./components/Authorize/Login";
import MachineList from "./components/Machine/MachineList";
import Home from "./pages/Home";
import { useRoutes } from "react-router-dom";
import MainLayout from "./layouts/MainLayout";
function PublicRoutes() {
  let element = useRoutes([
    {
        element: <MainLayout />,
        children: [
            { path: "/", element: <Home /> },
            { path: "/login", element: <Login /> },
            { path: "/machines", element: <MachineList /> },
        ]
      }
    
  ]);
  return element;
}
export default PublicRoutes;
