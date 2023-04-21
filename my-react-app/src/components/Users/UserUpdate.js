import React from 'react';
import { useState } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import styled from 'styled-components';
import Modal from '@mui/material/Modal';
import { BsFillPencilFill } from 'react-icons/bs';
import '../Users/Userindex.css';
import { AxiosPost, AxiosPut } from '../../util/requestURL';
import API from '../../util/APIConstanst';

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const Input = styled(TextField)`
  margin: 10px 20px;
`;

const TextButton = styled(Button)`
  display: block;
  margin: 20px;
`;

export const UserUpdate = (props) => {
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(!open);
  const handleClose = () => setOpen(false);
  const [id, setId] = useState(props.id);
  const [name, setName] = useState(props.name);
  const [phone, setPhone] = useState(props.phone);
  const [password, setPassord] = useState(props.password);

  const handleName = (e) => {
    setName(e.target.value);
  };

  const handlePhone = (e) => {
    setPhone(e.target.value);
  };

  const handlePassWord = (e) => {
    setPassord(e.target.value);
  };

  function refreshPage() {
    console.log('hello');
    updateUser();

    window.location.reload();
  }

  const updateUser = async () => {
    const params = {
      Id: id,
      UserName: name,
      Phone: phone,
    };
    try {
      console.log('hello');
      const response = await AxiosPut(API.USER_UPDATE, params);
      console.log('Update successfully: ', response);
    } catch (error) {
      console.log('Update Failed:', error);
    }
  };

  return (
    <>
      <BsFillPencilFill className="btnupdateUser" onClick={handleOpen}>
        Open modal
      </BsFillPencilFill>
      <div>
        <Modal
          open={open}
          onClose={handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <legend>Update</legend>
            <Input
              id="outlined-basic"
              label="Name"
              variant="outlined"
              value={name}
              onChange={handleName}
            />
            <Input
              id="outlined-basic"
              label="Phone Number"
              variant="outlined"
              value={phone}
              onChange={handlePhone}
            />
            <Button variant="contained" type="button" onClick={refreshPage}>
              Update
            </Button>
          </Box>
        </Modal>
      </div>
    </>
  );
};
