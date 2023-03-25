import React from "react";
import { useState ,useEffect } from "react";
import { Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalFooter, ModalBody, useDisclosure } from "@chakra-ui/react";

function ConfirmationScreen({ onConfirm,Close }) {
  const { isOpen, onOpen, onClose } = useDisclosure();
  
  const handleClose =()=>{
    Close()
    onClose()
  }
  const handleConfirm = () => {
    onConfirm();
    onClose();
  };

  useEffect(() => {
      onOpen();
  }, [onOpen]);

  return (
    <>
      <Modal isOpen={isOpen} onClose={onClose}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Delete Rule?</ModalHeader>
          <ModalBody>
            Are you sure you want to proceed with this action?
          </ModalBody>
          <ModalFooter>
            <Button variant="ghost" mr={3} onClick={handleClose}>
              Cancel
            </Button>
            <Button colorScheme="red" onClick={handleConfirm}>
              Confirm
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
}

export default ConfirmationScreen;