import React, { useRef, useState, useEffect } from "react";
import { TextField } from "@mui/material";
import Form from "react-bootstrap/Form";
import autAPI from "../../util/authAPI";
import { useNavigate, useParams } from "react-router-dom";
import instance, { AxiosPut } from "../../util/requestURL";
import { Outlet } from "react-router-dom";
import API from "../../util/APIConstanst";
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import Button from '@mui/material/Button';
import styled from 'styled-components';

const TextButton = styled(Button)`
display: 'inline-flex';
padding: 30px;
`;

const FormAdd = styled.div`
padding: 20px;
`;

export default function MachineAdd() {
  const navigate = useNavigate();

  const { id } = useParams();
  const [machine, setMachine] = useState("");
  const [update, setUpdate] = useState("");
  const [name, setName] = useState("");
  const [type, setType] = useState("");
  const [branch, setBranch] = useState("");
  const [size, setSize] = useState("");
  const [status, setStatus] = useState("");

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

  useEffect(() => {
    const getData = async () => {
      try {
        const response = await autAPI.machineId(id);
        setMachine(response);
        console.log("machine: ", response);
        setName(response.data.MachineName);
        setType(response.data.MachineType);
        setBranch(response.data.Branch);
        setSize(response.data.Size);
        setStatus(response.data.Status);
      } catch (error) {
        console.log("Liste Failed:", error);
      }
    };
    getData();
  }, []);

  const updateMachine = async () => {
    try {
      // const response = await autAPI.machineUpdate(id, name, type, branch, size, status);
      const response = await AxiosPut(API.MACHINE_UPDATE, {
        id: id,
        MachineName: name,
        MachineType: type,
        Branch: branch,
        Size: size,
        Status: status,
        // id:id,
        // MachineName:name,
        // MachineType:type,
        // Branch:branch,
        // Size:size,
        // Status:status
      });
      console.log("Update successfully: ", response);
      setUpdate(response);
      navigate("/machine");
    } catch (error) {
      console.log("Update Failed:", error);
    }
  };

  return (
    <div>
      <legend>Update</legend>
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
        <Button variant="contained" type="button" onClick={()=>navigate("/")} sx={{mr: '150px'}} >
          Back
        </Button>
        <Button variant="contained" type="button" onClick={updateMachine} sx={{ml: '150px'}} >
          Update
        </Button>
        </TextButton>
      </Form>
    </div>
  );
}
