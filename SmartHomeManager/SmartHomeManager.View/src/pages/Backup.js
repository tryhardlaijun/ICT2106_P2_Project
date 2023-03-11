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

    function fetchBackup() {
        //to set profileId(/accountId) in Login.js, using this because it already has rules/scenarios in db
        //not sure if accountId is the same as the profileId saved in Scenarios
        localStorage.setItem('profileId', '22222222-2222-2222-2222-222222222222')

        fetch("https://localhost:7140/api/Backup/loadBackupScenario/" + localStorage.getItem('profileId')).then(response =>
            response.json().then(data => ({
                data: data,
                status: response.status
            })
            ).then(res => {
                //console.log(res.data);

                Object.entries(res.data).forEach(([key, value]) => {
                    console.log(`BackupScenario ${key}: ${value.scenarioID}`);

                    fetch("https://localhost:7140/api/Backup/loadBackupRule/" + value.scenarioID).then(response =>
                        response.json().then(data => ({
                            data: data,
                            status: response.status
                        })
                        ).then(res =>

                            Object.entries(res.data).forEach(([key, value]) => {
                                console.log(`BackupRule ${key}: ${value.rulesID}`);
                            })
                        )
                    )
                });

                
            }))
            

        
    

    }

    fetchBackup();

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
