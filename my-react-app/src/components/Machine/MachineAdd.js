import React, { useRef, useState } from 'react'
import { TextField } from '@mui/material';
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import autAPI from '../../util/authAPI';
import { useNavigate, Outlet } from 'react-router-dom';
import instance, { AxiosPost } from '../../util/requestURL';
import API from '../../util/APIConstanst';
export default function MachineAdd() {


  const MachineName = useRef("");
  const MachineType = useRef("");
  const Branch = useRef("");
  const Size = useRef("");
  const Status = useRef("");
  const [add, setAdd] = useState("")
  

  const navigate = useNavigate();


  const addMachine = async () =>{
    const postData = {

      MachineName :MachineName.current.value,
      MachineType : MachineType.current.value,
      Branch : Branch.current.value,
      Size : Size.current.value,
      Status: Status.current.value
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
        <Form.Group className="mb-3" controlId="formName">
          <Form.Label>Name</Form.Label>
          <Form.Control type="text" ref={MachineName} />
          
        </Form.Group>
        <Form.Group className="mb-3" controlId="formQuanity">
          <Form.Label>Type</Form.Label>
          <Form.Control type="text" ref={MachineType} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formPrice">
          <Form.Label>Branch</Form.Label>
          <Form.Control type="text" ref={Branch} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formImageUrl">
          <Form.Label>Size</Form.Label>
          <Form.Control type="text" ref={Size} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formImageUrl">
          <Form.Label>Status</Form.Label>
          <Form.Control type="text" ref={Status} />
        </Form.Group>
        <Button variant="primary" type="button" onClick={addMachine}>
          Add
        </Button>
      </Form>
      
    </div>
  )
}
