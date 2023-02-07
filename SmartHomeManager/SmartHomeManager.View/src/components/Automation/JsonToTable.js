import {
    Button,
    Table,
    Thead,
    Tbody,
    Tfoot,
    Tr,
    Th,
    Td,
    TableContainer,
    Box
} from '@chakra-ui/react'
import React from "react";
import JsonData from "./dummyData.json";
import Buttons from "./Buttons" 

function jsonToTable() {
    const DisplayData = JsonData.map((info) => {
        return (
            <Tr key={info.RuleID}>
                <Td>{info.RuleName}</Td>
                <Td>{info.DeviceName}</Td>
                <Td>{info.Description}</Td>
                <Buttons />
            </Tr>
        );
    });
    return (
        <TableContainer>
            <Table variant='striped' colorScheme='gray'>
                <Thead>
                    <Tr>
                        <Th>Rule Name</Th>
                        <Th>Device Name</Th>
                        <Th>Descrption</Th>
                        <Th>Options</Th>
                    </Tr>
                </Thead>
                <Tbody>{DisplayData}</Tbody>
            </Table>
        </TableContainer>
    );
}

export default jsonToTable;
