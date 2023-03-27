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

function jsonToTable({ruleData, deleteRule, editButton}) {
    const DisplayData = ruleData.map((info) => {
        return (
            <Tr key={info.ruleId}>
                <Td>{info.ruleName}</Td>
                <Td>{returnDeviceName(info.deviceId)}</Td>
                <Td>{info.configurationKey}</Td>
                <Buttons props={info} deleteRule= {deleteRule} editButton={editButton} deviceName={returnDeviceName(info.deviceId)} />
            </Tr>
        );
    });
    return (
        <TableContainer>
            <Table variant='striped' colorScheme="blackAlpha">
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

function returnDeviceName(deviceID){
    if(deviceID == "33333333-3333-3333-3333-333333333333"){
        return "FAN Room 1"
    }
}

export default jsonToTable;
