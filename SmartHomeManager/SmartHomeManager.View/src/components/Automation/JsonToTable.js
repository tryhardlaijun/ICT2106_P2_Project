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

function jsonToTable({ruleData}) {
    console.log(ruleData);
    const DisplayData = ruleData.map((info) => {
        return (
            <Tr key={info.ruleId}>
                <Td>{info.RuleName}</Td>
                <Td>{info.deviceId}</Td>
                <Td>{info.startTime}</Td>
                <Buttons props={info} />
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
