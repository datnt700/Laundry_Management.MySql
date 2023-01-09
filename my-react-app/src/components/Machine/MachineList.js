import { useEffect, useState } from "react";
import { getAPI } from "../../api/APIS";
import "./index.css";
import { status } from "./Enum";
import autAPI from "../../util/authAPI";
import instance from "../../util/requestURL";

const MachineList = () => {
  //set gia tri ban dau la mot mang rong
  const [api, setAPI] = useState([]);
  console.log(api);


  useEffect (() => {
    const fetchData = async () => {
        try{    
          const response = await autAPI.machine();;
          console.log('Get successfully: ', response);
          setAPI(response);
        }catch (error) {
          console.log('Get Failed:', error)
        }
      };
      fetchData();
  },[]);


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
  
      
        <div className="row">
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
                      />
                      <button className="btn btn-default" title="Reload">
                        <i className="fa fa-sync-alt" />
                      </button>
                      <button className="btn btn-default" title="Pdf">
                        <i className="fa fa-file-pdf" />
                      </button>
                      <button className="btn btn-default" title="Excel">
                        <i className="fas fa-file-excel" />
                      </button>
                    </div>
                  </div>
                </div>
              </div>
              <div className="panel-body table-responsive">
                <table className="table">
                  <thead>
                    <tr>
                      <th>#</th>
                      <th>Full Name</th>
                      <th>Age</th>
                      <th>Job Title</th>
                      <th>City</th>
                      <th>Action</th>
                      <th>Status</th>
                    </tr>
                  </thead>
                  <tbody>     
                  {
                    api.status === 200 ? {machines} = api.data.map((item,key) => (
                      <tr key={key} className={item.isComplete ? "done" : ""} >
                      <td>{key}</td>
                      <td>{item.MachineName}</td>
                      <td>{item.MachineType}</td>   
                      <td>{item.Branch}</td>  
                      <td>{item.Size}</td>   
                      <td>{item.Status}</td> 
                    </tr>
                    
                    )
                  ) : <tr><td>{message} = {api.Message}</td></tr>
                } 
   
                  </tbody>
                </table>
              </div>
              
            </div>
          </div>
        </div>
      
    );
  } 
};

export default MachineList;
