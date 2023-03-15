import React from 'react';
import Button from "react-bootstrap/Button";
import { Modal } from 'react-bootstrap';
import { Outlet } from 'react-router-dom';

 function MachineDelete(props) {
  return (
    <>
    <Modal
      show={props.shows}
      onHide={() => {
        props.hideConfirmDeleteHandler();
      }}
      keyboard={false}
    >
      <Modal.Header closeButton>
        <Modal.Title>{props.title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>{props.body}</Modal.Body>
      <Modal.Footer>
        <Button
          variant="secondary"
          onClick={() => {
            props.hideConfirmDeleteHandler();
           }}
           className='btnConfirmDelete'
        >
          Close
        </Button>
        <Button className='btnConfirmDelete'
          variant="danger"
          onClick={() => {
            props.confirmDeleteHandler();
          }}
        >
          Confirm Delete
        </Button>
      </Modal.Footer>
    </Modal>
    
  </>
  )
}
export default MachineDelete;