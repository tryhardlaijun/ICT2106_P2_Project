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
import { useEffect, useState } from 'react';

export default function Backup() {

    const [backupScenarioList, getBackupScenarioList] = useState([]);
    //const backupRulesList = useState();


    function fetchBackup() {
        //to set profileId(/accountId) in Login.js, using this because it already has rules/scenarios in db
        //not sure if accountId is the same as the profileId saved in Scenarios
        localStorage.setItem('profileId', '22222222-2222-2222-2222-222222222222')
        fetch("https://localhost:7140/api/Backup/loadBackupScenario/" + localStorage.getItem('profileId'))
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                getBackupScenarioList(data);
            });
    }

    useEffect(() => {
        fetchBackup();
    }, []);

   

    function buttonClicked(scenario) {
        console.log(scenario);

        var ts = new Date(scenario.createdAt)

        document.getElementById("showSelected").innerText = "Version selected: " + scenario.versionNumber + " - " + ts.toLocaleDateString('en-GB') + ' ' + ts.toLocaleTimeString('en-GB')
        document.getElementById("showSelected").style.display = "block";
    }

    //todo: onsubmit function for the restore backup button -- should load backup rules here
    

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
                            {
                                backupScenarioList.map((scenario) => {
                                    let ts = new Date(scenario.createdAt)

                                    return (
                                        <Tr key={scenario.versionNumber}>
                                            <Td data-title="createdAt">{ts.toLocaleDateString('en-GB') + ' ' + ts.toLocaleTimeString('en-GB')}</Td>
                                            <Td data-title="versionNumber">{scenario.versionNumber}</Td>
                                            <Td>
                                                <Button id="selectButton" onClick={() => buttonClicked(scenario)}>Select</Button>
                                            </Td>
                                        </Tr>
                                    )
                                })
                            }
                            
                            
                            
                        </Tbody>
                    </Table>
                </TableContainer>
            </Box>


        </Container>


    )
}
