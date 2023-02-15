import React from "react";
import {
    Heading, Center, Container, Button, Box, Text,
    Table,
    Thead,
    Tbody,
    Tr,
    Th,
    Td,
    TableContainer
} from '@chakra-ui/react'

export default function Backup() {
    function buttonClicked() {
        console.log("button clicked");
        document.getElementById("showSelected").style.display = "block";
    }


    return (
        <Container maxW='container.sm'>
            <Box textAlign='center' h='300px'>
                <Center h='200px'>
                    <Heading>Backup</Heading>
                </Center>
                <Button type="submit">Restore Backup</Button>
                <Text id='showSelected' style={{ display: "none" }} >Version selected: (version number) - (timestamp)</Text>
            </Box>
            <Box>
                <TableContainer>
                    <Table>
                        <Thead>
                            <Tr>
                                <Th>Timestamp</Th>
                                <Th>Version</Th>
                            </Tr>
                        </Thead>
                        <Tbody>
                            <Tr>
                                <Td>timestamp</Td>
                                <Td>version number</Td>
                                <Td>
                                    <Button id="selectButton" onClick={buttonClicked}>Select</Button>
                                </Td>
                            </Tr>
                        </Tbody>
                    </Table>
                </TableContainer>
            </Box>


        </Container>


    )
}
