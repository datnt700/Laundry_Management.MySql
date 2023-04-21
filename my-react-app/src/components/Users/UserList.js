import ReactPaginate from 'react-paginate';
import { useEffect, useState } from 'react';
import autAPI from '../../util/authAPI';
import instance, {
  AxiosDelete,
  AxiosGet,
  AxiosPost,
} from '../../util/requestURL';
import axios from 'axios';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import {
  BsFillPencilFill,
  BsFillXSquareFill,
  BsPlusSquare,
  BsSearch,
} from 'react-icons/bs';
import TableRow from '@mui/material/TableRow';
import API from '../../util/APIConstanst';
import UserDelete from './UserDelete';
import Input from '@mui/joy/Input';
import UserAdd from './UserAdd';
import Button from '@mui/material/Button';
import * as React from 'react';
import { UserUpdate } from './UserUpdate';
import getCookie from '../../hooks/getCookie';
import { useNavigate } from 'react-router-dom';

const User = () => {
  const [items, setItem] = useState([]);
  const [size, setSize] = useState(5);
  const [index, setIndex] = useState(1);
  const [searching, setSearch] = useState('');

  const [pageCountUser, setpageCountUser] = useState(0);
  const [showModal, setShowModal] = useState(false);
  const [itemToDeleteId, setItemToDeleteId] = useState(0);

  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  const navigate = useNavigate();

  const token = getCookie('token');
  const getList = () => {
    if (!token) {
      navigate('/login');
    }
    let param = { PageIndex: index, PageSize: size };
    if (searching && searching.trim().length > 0) {
      param.search = searching;
    }
    AxiosGet(API.GET_FILTER_USER, param)
      .then((response) => {
        console.log(response);
        setpageCountUser(Math.ceil(response.TotalCount / size));
        console.log(response.ListData);
        setItem(response.LisData);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  useEffect(() => {
    getList();
  }, [searching, index]);

  const showConfirmDeleteHandler = (id) => {
    console.log('Id: ', id);
    setShowModal(true);
    setItemToDeleteId(id);
  };

  function hideConfirmDeleteHandler() {
    setShowModal(false);
    setItemToDeleteId(0);
  }

  const refresDelete = () => {
    confirmDeleteHandler();
    window.location.reload();
  };

  const confirmDeleteHandler = () => {
    try {
      console.log('itemToDeleteId: ', itemToDeleteId);
      const response = AxiosDelete(API.USER_DELETE, {
        data: { id: itemToDeleteId },
      });
      setShowModal(false);
      console.log(response);

      setItem(
        items.filter((e) => {
          return e.UserId !== itemToDeleteId;
        })
      );
      setItemToDeleteId(0);
    } catch (error) {
      console.log('Delete failed: ', error);
    }
  };

  const handlePageClick = async (data) => {
    console.log(data.selected);
    let currentPage = data.selected + 1;
    setIndex(currentPage);
  };
  return (
    <>
      <UserDelete
        showModal={showModal}
        title="Delete Confirmation"
        body="Are you want delete this itme?"
        confirmDeleteHandler={refresDelete}
        hideConfirmDeleteHandler={hideConfirmDeleteHandler}
      ></UserDelete>

      <div className="container">
        <TableContainer component={Paper} sx={{ mt: 10 }}>
          <Input
            placeholder="Type in here…"
            onChange={(e) => setSearch(e.target.value.toLowerCase())}
            value={searching}
          />
          <UserAdd></UserAdd>
          <Table sx={{ minWidth: 650 }} aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell align="right" sx={{ fontWeight: 'bold' }}>
                  Id
                </TableCell>
                <TableCell align="right" sx={{ fontWeight: 'bold' }}>
                  {' '}
                  Name
                </TableCell>
                <TableCell align="right" sx={{ fontWeight: 'bold' }}>
                  Phone
                </TableCell>
                <TableCell align="right" sx={{ fontWeight: 'bold' }}>
                  Action
                </TableCell>

                <TableCell
                  align="right"
                  sx={{ fontWeight: 'bold' }}
                ></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {items.length ? (
                items.map((row, key) => (
                  <TableRow
                    key={key}
                    sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                  >
                    <TableCell align="right">{row.Id}</TableCell>

                    <TableCell align="right">{row.UserName}</TableCell>
                    <TableCell align="right">{row.Phone}</TableCell>

                    <TableCell className="btnUser" align="right">
                      <UserUpdate
                        id={row.Id}
                        name={row.UserName}
                        phone={row.Phone}
                      ></UserUpdate>
                      <BsFillXSquareFill
                        className="btnDeleteUser"
                        onClick={() => {
                          showConfirmDeleteHandler(row.Id);
                        }}
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
          previousLabel={'<=='}
          nextLabel={'==>'}
          breakLabel={'...'}
          pageCount={pageCountUser}
          marginPagesDisplayed={2}
          pageRangeDisplayed={5}
          onPageChange={handlePageClick}
          containerClassName={'pagination'}
          subContainerClassName={'pages pagination'}
          activeClassName={'active'}
        />
      </div>
    </>
  );
};

export default User;
