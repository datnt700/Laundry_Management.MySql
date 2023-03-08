import React from "react";
import Login from "./components/Authorize/Login";
import MachineList from "./components/Machine/MachineList";
import { useRoutes,Outlet } from "react-router-dom";
import Navbar from "./pages/Navbar";
import MachineAdd from "./components/Machine/MachineAdd";
import MachineUpdate from "./components/Machine/MachineUpdate";
import MachineDelete from "./components/Machine/MachineDelete";
import Home from "./pages/Home";
import UserList from "./components/Users/UserList";
function PublicRoutes() {
  let element = useRoutes([
    
    // {
    //     element: <Navbar />,
    //     children: [
    //       { path: "/", element: <Home /> },
    //         { path: "/login", ‘‘‘‘‘element: <Login /> },
    //         { path: "/login", elv©◊v‘“‘‘ement: <Login /> },
    
    
    //         { path: "/user", element: <User /> },
    //     ],
    //   }
    {
      path: '/',
      element: <Home />
    },
    {
      path:'/login',
      element: <Login />
    },
    {
      path:'/machine',
      children: [
        {element:<MachineList />, index: true},
        { path: 'Add', element: <MachineAdd /> },
        { path: 'Update/:id', element: <MachineUpdate /> },
        { path: "Delete", element: <MachineDelete /> },
    ]
    },
    {
      path:'/user',
      children: [
        {element:<UserList />, index: true},
        // { path: 'Add', element: <MachineAdd /> },
        // { path: 'Update/:id', element: <MachineUpdate /> },
        // { path: "Delete", element: <MachineDelete /> },
    ]
    }
    
  ]);
  return element;
}
export default PublicRoutes;
