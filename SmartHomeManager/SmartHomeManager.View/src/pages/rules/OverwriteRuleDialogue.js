import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    useDisclosure,
} from "@chakra-ui/react";

import {
    Button,
} from "@chakra-ui/react";


export default function OverwriteRuleDialogue(props) {
    const { Close, OverwriteCallBack , Title } = props;
    const { isOpen, onOpen, onClose } = useDisclosure();


    const handleButtonClick = () => {
        Close();
        onClose();
        OverwriteCallBack()
    };

    useEffect(() => {
        onOpen();
    }, [onOpen]);

    const handleClose = () => {
        Close();
        onClose();
    };

    return (
        <>
            <Modal isOpen={isOpen} >
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>
                        This rule seems to clash with another active rule ({Title}). Would you like to overwrite it?
                    </ModalHeader>

                    <ModalFooter>
                        <Button colorScheme="red" mr={3} onClick={handleClose}>
                            No
                        </Button>
                        <Button colorScheme="green" onClick={handleButtonClick}>
                            Yes
                        </Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    );
}
