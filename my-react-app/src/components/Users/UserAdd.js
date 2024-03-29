import * as React from 'react';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';
import styled from 'styled-components';
import Button from '@mui/material/Button';
import { BsPlusSquare } from 'react-icons/bs';
import '../Users/Userindex.css';
import { useState } from 'react';
import { AxiosPost } from '../../util/requestURL';
import API from '../../util/APIConstanst';

const TextButton = styled(Button)`
  display: block;
  margin: 20px;
`;

const Input = styled(TextField)`
  margin: 10px 20px;
`;

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

export default function UserAdd(props) {
  const [open, setOpen] = React.useState(false);
  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');
  const [password, setPassord] = useState('');

  const handleOpen = () => setOpen(!open);
  const handleClose = () => {
    setOpen(false);
    setName('');
    setPhone('');
    setPassord('');
  };

  const handleName = (e) => {
    setName(e.target.value);
  };

  const handlePhone = (e) => {
    setPhone(e.target.value);
  };

  const handlePassWord = (e) => {
    setPassord(e.target.value);
  };

  const addUser = async () => {
    const postData = {
      UserName: name,
      Phone: phone,
      Password: password,
    };
    try {
      const response = await AxiosPost(API.USER_ADD, postData);
      console.log('Add successfully: ', response);
    } catch (error) {
      console.log('Add Failed:', error);
    }
  };

  return (
    <>
      <BsPlusSquare onClick={handleOpen} className="btnAdd">
        Open modal
      </BsPlusSquare>
      <div>
        <Modal
          open={open}
          onClose={handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <legend>Add</legend>
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
            <Input
              id="outlined-basic"
              label="Password"
              variant="outlined"
              value={password}
              onChange={handlePassWord}
            />

            <TextButton variant="contained" type="button" onClick={addUser}>
              Add
            </TextButton>
          </Box>
        </Modal>
      </div>
    </>
  );
}
