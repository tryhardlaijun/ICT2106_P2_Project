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

function troublershooterToTable({ troublershootersData }) {
    const DisplayData = troublershootersData.map((info) => {
        return (
            <Tr key={info.troubleshooterId}>
                <Td>{info.recommendation}</Td>
                <Td>{info.deviceType}</Td>
                <Td>{info.configurationKey}</Td>
            </Tr>

        );
    });
    return (
        <TableContainer>
            <Table variant='striped' colorScheme='gray'>
                <Thead>
                    <Tr>
                        <Th>Recommendation</Th>
                        <Th>Device Type</Th>
                        <Th>Configuration Key</Th>
                    </Tr>
                </Thead>
                <Tbody>{DisplayData}</Tbody>
            </Table>
        </TableContainer>
    );
}

export default troublershooterToTable;
