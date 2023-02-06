import {
    Button,
    Table,
    Thead,
    Tbody,
    Tfoot,
    Tr,
    Th,
    Td,
    TableCaption,
    TableContainer,
    Box
} from '@chakra-ui/react'
import React from "react";

export default function Scenarios() {
    return <Box padding='4'>
        <TableContainer>
        <Table variant='striped' colorScheme='teal'>
            <TableCaption>Imperial to metric conversion factors</TableCaption>
            <Thead>
                <Tr>
                    <Th>Rule Name</Th>
                    <Th>Device Name</Th>
                    <Th>Descrption</Th>
                   <Th>Options</Th>
                </Tr>
            </Thead>
            <Tbody>
                <Tr>
                    <Td>inches</Td>
                    <Td>millimetres (mm)</Td>
                    <Td >25.4</Td>
                    <Td>
                        <Button ml={2} colorScheme='green' >
                            Add
                        </Button>
                        <Button ml={2} colorScheme='blue' >
                            Edit
                        </Button>
                        <Button ml={2} colorScheme='red' >
                            Delete
                        </Button>
                    </Td>   
                </Tr>
                <Tr>
                    <Td>feet</Td>
                    <Td>centimetres (cm)</Td>
                    <Td>30.48</Td>
                    <Td>
                        <Button ml={2} colorScheme='green' >
                            Add
                        </Button>
                        <Button ml={2} colorScheme='blue' >
                            Edit
                        </Button>
                        <Button ml={2} colorScheme='red' >
                            Delete
                        </Button>
                    </Td>
                </Tr>y>

            </Tbody>
        </Table>
        </TableContainer>
    </Box>;
}
