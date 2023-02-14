import React from "react";
import {
    Heading, Center, Container, Button, Box, Text, VStack,
    Modal,
    ModalOverlay,
    ModalContent,
    ModalFooter,
    ModalHeader,
    ModalCloseButton,
    useDisclosure
} from '@chakra-ui/react'

export default function Intruder() {
    const { isOpen, onOpen, onClose } = useDisclosure()

    return (
        <Container maxW='container.sm'>
            <Box textAlign='center' h='300px'>
                <Center h='200px'>
                    <Heading>Intruder Detected!</Heading>
                </Center>
                <VStack spacing={8}>
                    <Button>Lockdown</Button>
                    <Button>Don&apos;t lockdown</Button>
                    <Button onClick={onOpen} onClose={onClose}>Contact police</Button>
                </VStack>
            </Box>

            <Modal isOpen={isOpen} onClose={onClose}>
                <ModalOverlay />
                <ModalContent>
                    <ModalCloseButton />
                    <ModalHeader>
                        <Text>Confirm to call the police?</Text>
                    </ModalHeader>

                    <ModalFooter>
                        <Button colorScheme='blue' mr={3} onClick={onClose}>
                            Close
                        </Button>
                        <Button variant='ghost'>Confirm</Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </Container>


    )
}
