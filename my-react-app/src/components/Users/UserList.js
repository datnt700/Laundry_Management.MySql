
import ReactPaginate from "react-paginate";
import { useEffect, useState } from "react";
import autAPI from "../../util/authAPI";
import instance, { AxiosGet } from "../../util/requestURL";
import axios from "axios";
import PostList from ".";
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import { BsFillPencilFill, BsFillXSquareFill, BsPlusSquare,BsSearch } from "react-icons/bs";
import TableRow from '@mui/material/TableRow';
import API from "../../util/APIConstanst"

import Input from '@mui/joy/Input';


const User = () => {

  const [items, setItem] = useState([]);
  const [size, setSize] = useState(5)
  const [index, setIndex] = useState(1)
  const [searching,setSearch] =useState()
  console.log(size);
  console.log(index);
  const [pageCountUser, setpageCountUser] = useState(0)

  
    const getList = async () => {
      await AxiosGet(API.GET_FILTER_USER,{PageIndex:index,PageSize : size,search : searching}).then((response) => {
        console.log(response)                                                                                                                                             
        setpageCountUser(Math.ceil(response.TotalCount/size))
        console.log(response.ListData);
        setItem(response.LisData)
        
      }).catch((error) => {
        console.log(error);
      });                                                                                                                                                            
  };
  
  useEffect(() => {
    getList();
  },[searching]);
                                                                                                                                                                          
                                                                                                      
                                                  
  // const fetchComments = async (currentPage) => {
  //   const response = await instance.get(
  //     // `http://localhost:3004/comments?_page=${currentPage}&_limit=12`
  //     `User/GetAll?PageIndex=${currentPage}&PageSize=5`
  //     );
  //     const {data} = response
  //     console.log(data);
  //     return data
  //   }; 

  const handlePageClick = async (data) =>{
    console.log(data.selected);
    let currentPage = data.selected +1;
    setIndex(currentPage);
    getList();
  }
  return (
    <div className="container">
      <TableContainer component={Paper} sx={{ mt: 10 }}>
        <Input
          placeholder="Type in here…"
          onChange={(e) => setSearch(e.target.value.toLowerCase())}
          value={searching}
        />
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell align="right" sx={{ fontWeight: "bold" }}>
                Id
              </TableCell>
              <TableCell align="right" sx={{ fontWeight: "bold" }}>
                {" "}
                Name
              </TableCell>
              <TableCell align="right" sx={{ fontWeight: "bold" }}>
                Phone
              </TableCell>
              <TableCell align="right" sx={{ fontWeight: "bold" }}></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {items.length ? (
              items.map((row, key) => (
                <TableRow
                  key={key}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                >
                  <TableCell align="right">{key}</TableCell>

                  <TableCell align="right">{row.UserName}</TableCell>
                  <TableCell align="right">{row.Phone}</TableCell>
                  <TableCell align="right">
                    <BsFillPencilFill
                      // id="btnupdate"
                      // onClick={() => navigate(`Update/${item.MachineId}`)}
                    />

                    <BsFillXSquareFill
                      // onClick={() => {
                      //   showConfirmDeleteHandler(item.MachineId);
                      // }}
                    />
                  </TableCell>
                </TableRow>
              ))
            ) : (
              <tr>
                <td>{items.Message}</td>
              </tr>
            )}
          </TableBody>
        </Table>
      </TableContainer>

      <ReactPaginate
        previousLabel={"previous"}
        nextLabel={"next"}
        breakLabel={"..."}
        pageCount={pageCountUser}
        marginPagesDisplayed={3}
        pageRangeDisplayed={3}
        onPageChange={handlePageClick}
        containerClassName={"pagination justify-content-center"}
        pageClassName={"page-item"}
        pageLinkClassName={"page-link"}
        previousClassName={"page-item"}
        previousLinkClassName={"page-link"}
        nextClassName={"page-item"}
        nextLinkClassName={"page-link"}
        breakClassName={"page-item"}
        breakLinkClassName={"page-link"}
        activeClassName={"active"}
      />
    </div>
  );
}


export default User;