import React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import { useRef, useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import autAPI from "../../util/authAPI";
import API from "../../util/APIConstanst";
import { AxiosPut } from "../../util/requestURL";
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import styled from 'styled-components';
import InputLabel from '@mui/material/InputLabel';
import { BsFillPencilFill } from "react-icons/bs";
import "../Machine/index.css";

import Form from "react-bootstrap/Form";
const FormAdd = styled(Form)`
padding:0 120px;
`;

const TextButton = styled(Button)`
position: relative;
    left: 190px;
    padding-top: 20px;

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
export default function ModalUp(props) {
    const [isShow, invokeModel] = React.useState(false);
    const initModel = () => invokeModel(!isShow);
    const handleClose = () => {
      invokeModel(false)
      };

    const navigate = useNavigate();


  const [update, setUpdate] = useState("");
  const [selecttedFile, setSelecttedFile] = useState("");
  const [id, setId] = useState(props.id)
  const [name, setName] = useState(props.name);
  const [type, setType] = useState(props.type);
  const [branch, setBranch] = useState(props.branch);
  const [size, setSize] = useState(props.size);
  const [status, setStatus] = useState(props.status);

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

  function refreshPage(){ 
    updateMachine();
    window.location.reload(); 
}

  const updateMachine = async () => {
    try {
      const response = await AxiosPut(API.MACHINE_UPDATE, {
        id: id,
        MachineName: name,
        MachineType: type,
        Branch: branch,
        Size: size,
        Status: status,      
      });
      console.log("Update successfully: ", response);
      setUpdate(response);
      updateMachine();
    } catch (error) {
      console.log("Update Failed:", error);
    }
  };
    return (
      <>
       <BsFillPencilFill className="btnupdate" onClick={initModel}  >
          Update
        </BsFillPencilFill>
        <div>
      <Modal
        open={isShow}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
        <legend>Update</legend>

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
            <MenuItem value={1}>Washing Machine</MenuItem>
            <MenuItem value={2}>Dryer</MenuItem>
          </Select>
        </FormControl>
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
            {/* </FormAdd> */}
            <TextButton>
        {/* <Button variant="contained" type="button" onClick={()=>navigate("/")} sx={{mr: '150px'}} >
          Back
        </Button> */}
        <Button variant="contained" type="button" onClick={refreshPage}  >
          Update
        </Button>
        </TextButton>
          </FormAdd>
        </Box>
      </Modal>
    </div>
    </>
    )
}