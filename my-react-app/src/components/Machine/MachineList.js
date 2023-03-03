import { useEffect, useState } from "react";

import "../Machine/index.css";
import {machineStatus, machineTypes} from "./Enum"
import autAPI from "../../util/authAPI";
import instance, { AxiosDelete, AxiosGet } from "../../util/requestURL";
import { Outlet, useNavigate } from "react-router-dom";
import MachineDelete from "./MachineDelete";
import getCookie from "../../hooks/getCookie";
import ReactPaginate from "react-paginate";
import React, { Component } from 'react';
import { BsFillPencilFill, BsFillXSquareFill, BsPlusSquare,BsSearch } from "react-icons/bs";
import API from "../../util/APIConstanst"
import SearchIcon from '@mui/icons-material/Search';

const MachineList = () => {
  //set gia tri ban dau la mot mang rong
  const [api, setAPI] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [itemToDeleteId, setItemToDeleteId] = useState(0);
  const [pageCount, setpageCount] = useState(0)
  const [size, setSize] = useState(20)
  const [index, setIndex] = useState(1)
  const [searching,setSearch] =useState()

  const token = getCookie("token")
  const arr = [];
  const getList =  () => {

    AxiosGet(API.GET_FILTER_MACHINE,{PageIndex:index,PageSize : size,search : searching}).then((data) => {
    
        console.log(data.TotalCount/size);
  
  
        setpageCount(Math.ceil(data.TotalCount/size))
        console.log(pageCount);
        setAPI(data.ListData)
      }).catch((error) => {
        console.log(error);
      });
      
    }; 
    useEffect(() => {
      getList();
    },[searching]);

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
    console.log(data.selected);
    let currentPage = data.selected +1;
    setIndex(currentPage);
    getList();
  }
  const navigate = useNavigate();
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
        showModal={showModal}
        title="Delete Confirmation"
        body="Are you want delete this itme?"
        confirmDeleteHandler={confirmDeleteHandler}
        hideConfirmDeleteHandler={hideConfirmDeleteHandler}
      ></MachineDelete>
      <div className="row-machine">
          <div className="col-md-offset-1 col-md-10">
            <div className="panel">
              <div className="panel-heading">
                <div className="row">
                  <div className="col col-sm-3 col-xs-12">
                    <h4 className="title">
                      Data <span>List</span>
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
                  <BsPlusSquare className="btnadd"   onClick={handleClickAdd}/>
                      
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
                      <th>IsActive</th>
                      <th>Action</th>
                    </tr>
                  </thead>
                  <tbody>
                    {api  ? api.map((item, key) => (
                      <tr key={item.MachineId} className={item.isComplete ? "done" : ""}>
                        <td>{item.MachineId}</td>
                        <td>{item.MachineName}</td>
                        <td>{item.MachineType === 1 ? machineTypes.WashingMachine :
                            item.MachineType === 2 ? machineTypes.Dryer : 'null'}</td>
                        <td>{item.Branch}</td>
                        <td>{item.Size}</td>
                        <td>{item.Status === 3 ? machineStatus.CLOSE :
                          item.Status === 4 ? machineStatus.LOADING :
                            item.Status === 5 ? machineStatus.DONE : ''} </td>
                        <td>{item.IsActive}</td>
                        <td className="btn">
                          <BsFillPencilFill id="btnupdate" onClick={() => navigate(`Update/${item.MachineId}`)}/>

                          <BsFillXSquareFill onClick={() => {showConfirmDeleteHandler(item.MachineId)}}/>
                        </td>
                      </tr>

                    )
                    ) : <tr><td>{message} = {api.Message}</td></tr>}



                  </tbody>
                </table>
              </div>

            </div>
          </div>
          <ReactPaginate
        previousLabel={"previous"}
        nextLabel={"next"}
        breakLabel={"..."}
        pageCount={pageCount}
        marginPagesDisplayed={3}
        pageRangeDisplayed={3}
        onPageChange={handlePageClick}
        containerClassName={"pagination justify-content-center"}
        pageClassName={"page-item active"}
        pageLinkClassName={"page-link"}
        previousClassName={"page-item active"}
        previousLinkClassName={"page-link"}
        nextClassName={"page-item disabled"}
        nextLinkClassName={"page-link"}
        breakClassName={"page-item"}
        breakLinkClassName={"page-link"}
        activeClassName={"active"}
      /> 
        </div></>
      
    );
  } 
};

export default MachineList;
