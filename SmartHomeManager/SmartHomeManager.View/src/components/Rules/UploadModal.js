import { useState } from "react";
import {
  Button,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Input,
} from "@chakra-ui/react";
import React from "react";
import axios from "axios";

function FileUploadModal({ isOpen, onClose, onUpload }) {
  const [selectedFile, setSelectedFile] = useState(null);

  function handleFileChange(e) {
    setSelectedFile(e.target.files[0]);
  }

  function handleUpload() {
    const formData = new FormData();
    formData.append("file", selectedFile);
    axios.post("https://localhost:7140/api/Rules/UploadRules", formData, {
        headers: {
            "Content-Type": "multipart/form-data",
        }
    }).then((response)=>{
        console.log(response);
        onClose();
        onUpload(localStorage.getItem("currentScenarioId"))
    }).catch((error)=>{
        console.error(error)
    })
  }

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Import File</ModalHeader>
        <ModalBody>
          <Input type="file" onChange={handleFileChange} />
        </ModalBody>

        <ModalFooter>
          <Button colorScheme="blue" mr={3} onClick={handleUpload}>
            Upload
          </Button>
          <Button variant="ghost" onClick={onClose}>
            Cancel
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
}

function UploadModalButton({ title, text, action }) {
  const [isOpen, setIsOpen] = useState(false);

  function handleOpen() {
    setIsOpen(true);
  }

  function handleClose() {
    setIsOpen(false);
  }

  return (
    <>
      <Button ml={2} onClick={handleOpen}>{text}</Button>
      <FileUploadModal isOpen={isOpen} onClose={handleClose} onUpload={action}/>
    </>
  );
}

export default UploadModalButton