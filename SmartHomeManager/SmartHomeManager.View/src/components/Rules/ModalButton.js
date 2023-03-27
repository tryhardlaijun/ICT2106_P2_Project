import { Button, Modal, ModalOverlay, ModalContent, ModalHeader, ModalBody, ModalFooter, ModalCloseButton, useDisclosure } from "@chakra-ui/react";
import React from "react";
import { Link } from "react-router-dom";
import Troubleshooter from "pages/troubleshooter/Troubleshooter";

function ModalButton(props) {
    const { isOpen, onOpen, onClose } = useDisclosure();
    const redirectTo = {
        pathname: "/troubleshooters",
        search: `?deviceType=${props.deviceType}&configMsg=${props.configMsg}`,
    };

        // Initialize the isOpen state to true
    //const [isModalOpen, setIsModalOpen] = useState(true);
    return (
        <>

            <Button onClick={onOpen} ml={2} colorScheme={"blackAlpha"}>{props.title}</Button>

            <Modal isOpen={isOpen} onClose={onClose}>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>{props.title}</ModalHeader>

                    <ModalCloseButton />
                    <ModalBody>
                        {props.text+"\n"}

                    </ModalBody>
                    <ModalBody>
                    </ModalBody>
                    <ModalFooter>
                        <Button colorScheme='blue' mr={3} onClick={onClose}>
                            Close
                        </Button>
                        <Button variant='solid' colorScheme="orange"><Link to={redirectTo}>Troubleshoot</Link></Button>

                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    )
}
export default ModalButton;
