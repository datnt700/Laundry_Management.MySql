import { useEffect, useState } from "react";

import "../Machine/index.css";
import {machineStatus, machineTypes} from "./Enum"
import autAPI from "../../util/authAPI";
import instance, { AxiosDelete, AxiosGet } from "../../util/requestURL";
import { useNavigate, useParams } from "react-router-dom";
import MachineDelete from "./MachineDelete";
import getCookie from "../../hooks/getCookie";
import ReactPaginate from "react-paginate";
import React, { Component } from 'react';
import { BsFillPencilFill, BsFillXSquareFill, BsPlusSquare,BsSearch } from "react-icons/bs";
import API from "../../util/APIConstanst"
import SearchIcon from '@mui/icons-material/Search';
import ModalUp from "./UpdateMachine";
import Button from '@mui/material/Button';
import MachineAdd from "./MachineAdd";

const MachineList = () => {
  //set gia tri ban dau la mot mang rong
  const [api, setAPI] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [itemToDeleteId, setItemToDeleteId] = useState(0);
  const [itemUpdateId, setItemUpdateId] = useState(0);
  const [pageCount, setpageCount] = useState(0)
  const [size, setSize] = useState(6)
  const [index, setIndex] = useState(1)
  const [searching,setSearch] =useState()
  const [open, setOpen] = React.useState(false);
 

 
  const token = getCookie("token")
  const arr = [];
  const navigate = useNavigate();
  const getList =  () => {
    if(!token){ navigate("/login")}
    else {  
      let param =  {PageIndex:index,PageSize : size};
      if (searching && searching.trim().length > 0) {
        param.search = searching
      } 
      AxiosGet(API.GET_FILTER_MACHINE,param).then((data) => {
        console.log(data);
        console.log(data.ListData);
        setpageCount(Math.ceil(data.TotalCount/size))
        console.log(pageCount);
        setAPI(data.ListData)
      }).catch((error) => {
        console.log(error);
      });
    }
  }; 
  useEffect(() => {
    getList();
  },[searching, index]);
  
  
  
    // const getData =  () => {
    //   try {
    //     console.log("itemUpdateId: ",itemUpdateId)
    //     const response = AxiosGet(API.MACHINE_ID,{id:itemUpdateId})
    //     console.log("machine: ", response);
    //     setName(response.data.MachineName);

    //     setType(response.data.MachineType);
    //     setBranch(response.data.Branch);
    //     setSize(response.data.Size);
    //     setStatus(response.data.Status);
    //   } catch (error) {
    //     console.log("Liste Failed:", error);
    //   }
    // };
   
  // const handleOpen = (id) => {
  //   console.log("Id: ",id)

  //   setOpen(!open); 
  //   setItemUpdateId(id);
  //   console.log("setItemUpdateId: ",itemUpdateId)

  //   getData()
  // }

   

    const showConfirmDeleteHandler = (id) => {
      console.log("Id: ",id)
      setShowModal(true);
      setItemToDeleteId(id);
    }
    


  const confirmDeleteHandler = () => {
    try {
      console.log("itemToDeleteId: ",itemToDeleteId)
       AxiosDelete(API.DELETE,{data:{id:itemToDeleteId}});
      
      setShowModal(false);
      setAPI(
        api.filter((p) => {
           return p.MachineId !== itemToDeleteId;
        }));
      setItemToDeleteId(0);
    } catch (error) {
      console.log("Delete Failed:", error);
    }
  }





 
  function hideConfirmDeleteHandler() {
    setShowModal(false);
    setItemToDeleteId(0);
  }
  
  function handleClickAdd() {
    navigate('Add');
   
  }

  const handlePageClick = async (data) =>{
    let currentPage = data.selected +1;
    console.log(currentPage);
    setIndex(currentPage);
    // getList();
  }
  var machines = [];
  var message="";

  if (machines.keys(api).length === 0)
  {
    return (
    <div class="spinner-border" role="status">
    <span class="sr-only">Loading...</span>
    </div>

    )
  }
  else
  {
    return (
  
      <><MachineDelete
        shows={showModal}
        title="Delete Confirmation"
        body="Are you want delete this itme?"
        confirmDeleteHandler={confirmDeleteHandler}
        hideConfirmDeleteHandler={hideConfirmDeleteHandler}
      ></MachineDelete>
     
      {/* {open.toString()} */}
      <div className="row-machine">
          <div className="col-md-offset-1 col-md-10" >
            <div className="panel">
              <div className="panel-heading">
                <div className="row">
                  <div className="col col-sm-3 col-xs-12">
                    <h4 className="title">
                      Machine <span></span>
                    </h4>
                    
                  </div>
                  <div className="col-sm-9 col-xs-12 text-right">
                    <div className="btn_group">
                      <input
                        type="text"
                        className="form-control"
                        placeholder="Search" 
                        value={searching}
                        onChange={e => setSearch(e.target.value)}/>
                    </div>
                  </div>
                  <MachineAdd>
                    </MachineAdd>                      
                </div>
              </div>
              <div className="panel-body table-responsive">
                <table className="table">
                  <thead>
                    <tr>
                      <th>#</th>
                      <th>Full Name</th>
                      <th>Type</th>
                      <th>Branch</th>
                      <th>Size</th>
                      <th>Status</th>
                      <th>Action</th>
                    </tr>
                  </thead>
                  <tbody>
                    {api  ? (api.map((item, key) => (
                      <tr key={key} className={item.isComplete ? "done" : ""}>
                        <td>{key}</td>
                        <td>{item.MachineName}</td>
                        <td>{item.MachineType === 1 ? machineTypes.WashingMachine :
                            item.MachineType === 2 ? machineTypes.Dryer : 'null'}</td>
                        <td>{item.Branch}</td>
                        <td>{item.Size}</td>
                        <td>{item.Status === 3 ? machineStatus.CLOSE :
                          item.Status === 4 ? machineStatus.LOADING :
                            item.Status === 5 ? machineStatus.DONE : ''} </td>
                        <td className="btn">
                          
                            <ModalUp             
                            id={item.MachineId}
                            name={item.MachineName}
                            type={item.MachineType}
                            branch={item.Branch}
                            size={item.Size}
                            status={item.Status}
                            >
                            </ModalUp>
                          <BsFillXSquareFill className="btnDelete" onClick={() => {showConfirmDeleteHandler(item.MachineId)}}/>
                        </td>
                       
                      </tr>

                    )
                    )) :( <tr><td>{message} = {api.Message}</td></tr>)}



                  </tbody>
                </table>
              </div>

            </div>
          </div>
          <ReactPaginate
        previousLabel={"<=="}
        nextLabel={"==>"}
        breakLabel={"..."}
        pageCount={pageCount}
        marginPagesDisplayed={2}
        pageRangeDisplayed={5}
        onPageChange={handlePageClick}
        containerClassName={"pagination"}
        subContainerClassName={"pages pagination"}
        activeClassName={"active"}
      /> 
        </div></>
      
    );
  } 
};

export default MachineList;
