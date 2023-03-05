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

const TextButton = styled(Button)`
display: 'inline-flex';
padding: 30px;
`;
const FormAdd = styled.div`
padding: 20px;
`;




export default function MachineAdd() {
  const [name,setName] = useState('');
  const [type, setType]= useState('');
  const [branch,setBranch] = useState('');
  const [size, setSize]= useState('');
  const [status,setStatus] = useState('');
  // const MachineName = useRef("");
  // const MachineType = useRef("");
  // const Branch = useRef("");
  // const Size = useRef("");
  // const Status = useRef("");
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
    <div>
      <legend>Create</legend>
      <Form>
      <FormAdd>
        <Form.Group className="mb-3" controlId="formName">
          <Form.Label>Name</Form.Label>
          <Form.Control type="text" value={name} onChange={handleName} />
        </Form.Group>
        <FormControl sx={{ width: 500 }}>
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
        <FormControl sx={{ width: 500 }}>
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
        <Button variant="contained" type="button" onClick={()=>navigate("/")} sx={{mr: '150px'}}>
          Back
        </Button>
        <Button variant="contained" type="button" onClick={addMachine} sx={{ml: '150px'}}>
        Add
        </Button>
        </TextButton>
      </Form>
    </div>
  );
}
