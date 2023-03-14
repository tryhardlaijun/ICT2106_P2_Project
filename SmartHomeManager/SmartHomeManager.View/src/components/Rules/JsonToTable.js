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
import Buttons from "./Buttons" 

function jsonToTable({ruleData, deleteRule}) {
    const DisplayData = ruleData.map((info) => {
        return (
            <Tr key={info.ruleId}>
                <Td>{info.ruleName}</Td>
                <Td>{info.deviceId}</Td>
                <Td>{info.configurationKey}</Td>
                <Buttons props={info} deleteRule= {deleteRule} />
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
