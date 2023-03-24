import React, { useState, useEffect } from "react";
import {
    Heading,
    Container,
    Table,
    Thead,
    Tbody,
    Tfoot,
    Tr,
    Th,
    Td,
    TableCaption,
    TableContainer,
    Switch,
    Text,
    Box,
    SimpleGrid,
    useDisclosure,
    useToast,
    Button,
    ButtonGroup,
    Link
} from '@chakra-ui/react'

import { ExternalLinkIcon } from '@chakra-ui/icons'

export default function HomeSecuritySettings() {
    const { isOpen, onOpen, onClose } = useDisclosure()

    return (

        <Container maxW='container.sm'>
            <Box textAlign='center' h='80px'>
                <Heading>Home Security Settings</Heading>
            </Box>
            <Box border='2px' borderRadius='16px'>
                <SimpleGrid columns={2} spacingX='40px' spacingY='20px' padding='4px'>
                    <Text fontSize='lg' textAlign='center'>Activate HomeSecurity</Text>
                    <Switch id='switchMasterActivation' />
                </SimpleGrid>
            </Box>
            <br></br>
            {/*isChecked defaultChecked isFocusable isDisabled isInvalid isReadOnly isRequired*/}
            <TableContainer border='2px' borderRadius='16px'>
                <Table variant='simple'>
                    <TableCaption>Device Activation Settings</TableCaption>
                    <Thead>
                        <Tr>
                            <Th>Device Group</Th>
                            <Th>Will Activate?</Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        <Tr>
                            <Td>Door</Td>
                            <Td><Switch id='switchDoor' /></Td>
                        </Tr>
                        <Tr>
                            <Td>Window</Td>
                            <Td><Switch id='switchWindow' /></Td>
                        </Tr>
                        <Tr>
                            <Td>Alarm</Td>
                            <Td><Switch id='switchAlarm' /></Td>
                        </Tr>
                        <Tr>
                            <Td>Gate</Td>
                            <Td><Switch id='switchGate' /></Td>
                        </Tr>
                    </Tbody>
                </Table>
            </TableContainer>
            <br></br>
            <Box border='2px' borderRadius='16px'>
                <Box overflowY="auto" minHeight="300px" maxHeight="300px" >
                    <TableContainer>
                        <Table variant='simple'>
                            <Tbody>
                                <Tr>
                                    <Td>1</Td>
                                </Tr>
                            </Tbody>
                            <Tbody>
                                <Tr>
                                    <Td>2</Td>
                                </Tr>
                            </Tbody>
                            <Tbody>
                                <Tr>
                                    <Td>3</Td>
                                </Tr>
                            </Tbody>
                            <Tbody>
                                <Tr>
                                    <Td>4</Td>
                                </Tr>
                            </Tbody>
                        </Table>
                    </TableContainer>
                </Box>
                <Text fontSize='sm' textAlign='center'>Device Trigger Logs</Text>
            </Box>
            <br></br>
            <Link href='/intruder' isExternal>
                Intruder Page (temp) <ExternalLinkIcon mx='2px' />
            </Link>
        </Container>
    )
}