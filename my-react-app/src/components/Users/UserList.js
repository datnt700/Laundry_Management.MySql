
import ReactPaginate from "react-paginate";
import { useEffect, useState } from "react";
import autAPI from "../../util/authAPI";
import instance from "../../util/requestURL";
import axios from "axios";
import PostList from ".";

const User = () => {

  const [items, setItem] = useState([]);
  const [size, setSize] = useState(5)
  const [index, setIndex] = useState(1)
  console.log(size);
  console.log(index);
  const [pageCountUser, setpageCountUser] = useState(0)

  useEffect(() => {
    const getList = async () => {
      await instance.get(
        `User/GetAll?PageIndex=${index}&PageSize=${size}`
      ).then((response) => {
        console.log({response})                                                                                                                                             
        const {data, PageSize} = response;                        
        console.log(data);
        setItem(data)
        return data;  
      }).catch((error) => {
        console.log(error);
      });
    }; 
    getList();                                                                                                                                                             
  },[size,index]);                                  
                                                                                                                                                                          
                                                                                                      
                                                  
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
    console.log(data.selected)

    console.log(data);
    let currentPage = data.selected +1
    setIndex(currentPage)
    console.log(currentPage);
    
  }
  return (
    <div className="container">
      <div className="row m-2">
        <h1>List Users</h1>
        <PostList posts={items} />
      
      </div>
   
       <ReactPaginate
        previousLabel={"previous"}
        nextLabel={"next"}
        breakLabel={"..."}
        pageCount={25}
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