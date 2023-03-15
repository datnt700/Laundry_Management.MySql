import React, { useRef, useState } from 'react'
import { TextField } from '@mui/material';
import Button from '@mui/material/Button';
import Form from "react-bootstrap/Form";
import autAPI from '../../util/authAPI';
import { useNavigate, Outlet } from 'react-router-dom';
import instance, { AxiosPost } from '../../util/requestURL';
import API from '../../util/APIConstanst';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import styled from 'styled-components';
import { BsFillPencilFill, BsFillXSquareFill, BsPlusSquare,BsSearch } from "react-icons/bs";
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
const TextButton = styled(Button)`
position: relative;
    left: 190px;
    padding-top: 20px;

`;

const FormAdd = styled(Form)`
padding:0 120px;
`;

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 800,
  bgcolor: 'background.paper',
  borderRadius:1,
  boxShadow: 24,
  p: 4,
};


export default function MachineAdd() {
  const [name,setName] = useState('');
  const [type, setType]= useState('');
  const [branch,setBranch] = useState('');
  const [size, setSize]= useState('');
  const [status,setStatus] = useState('');
 
  const [isShow, invokeModel] = React.useState(false);
    const initModel = () => invokeModel(!isShow);
    const handleClose = () => {
      invokeModel(false);
      setName("");
      setType("");
      setBranch("");
      setSize("");
      setStatus("");
    };
  const [add, setAdd] = useState("")
  
  const handleName = (e) => {
    setName(e.target.value);
  };

  const handleType = (e) => {
    setType(e.target.value);
  };
  const handleBranch = (e) => {
    setBranch(e.target.value);
  };
  const handleSize = (e) => {
    setSize(e.target.value);
  };
  const handleStatus = (e) => {
    setStatus(e.target.value);
  };

  const navigate = useNavigate();


  const addMachine = async () =>{
    const postData = {

      MachineName: name,
      MachineType: type,
      Branch: branch,
      Size: size,
      Status: status
    };
    
    try{    
      const response = await AxiosPost(API.MACHINE_ADD,postData);
      console.log(postData);
      console.log('Add successfully: ', response);
      setAdd(response)
      navigate("/machine");
    }catch (error) {
      console.log('Add Failed:', error)
    }
  }

  return (
    <>
    <BsPlusSquare className="btnadd"   onClick={initModel}/>
    <div>
    <Modal
        open={isShow}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
      <legend>Create</legend>
      <Form>
      <FormAdd>
        <Form.Group className="mb-3" controlId="formName">
          <Form.Label>Name</Form.Label>
          <Form.Control type="text" value={name} onChange={handleName} />
        </Form.Group>
        <FormControl sx={{ width: 500, textAlign: 'center' }}>
          <InputLabel id="demo-simple-select-label">Type</InputLabel>
          <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            label="Type"
            value={type}
            onChange={handleType}
          >
            <MenuItem value={1}>Laundry</MenuItem>
            <MenuItem value={2}>Dryer</MenuItem>
          </Select>
        </FormControl>
        {/* <Form.Group className="mb-3" controlId="formQuanity">
          <Form.Label>Type</Form.Label>
          <Form.Control type="text" ref={MachineType} />
        </Form.Group> */}
        <Form.Group className="mb-3" controlId="formPrice">
          <Form.Label>Branch</Form.Label>
          <Form.Control type="text" value={branch} onChange={handleBranch} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formImageUrl">
          <Form.Label>Size</Form.Label>
          <Form.Control type="text" value={size} onChange={handleSize} />
        </Form.Group>
        <FormControl sx={{ width: 500, textAlign: 'center' }}>
          <InputLabel id="demo-simple-select-label">Status</InputLabel>
          <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            label="Status"
            value={status}
            onChange={handleStatus}
          >
            <MenuItem value={3}>close</MenuItem>
            <MenuItem value={4}>loading</MenuItem>
            <MenuItem value={5}>done</MenuItem>
          </Select>
        </FormControl>
      </FormAdd>
      <TextButton>

        <Button variant="contained" type="button" onClick={addMachine} sx={{ml: '150px'}}>
        Add
        </Button>
        </TextButton>
      </Form>
      </Box>
      </Modal>
    </div>
    </>
  );
}
