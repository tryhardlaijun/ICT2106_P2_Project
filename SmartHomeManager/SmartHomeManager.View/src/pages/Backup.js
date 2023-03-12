import React from "react";
import {
    Heading, Center, Container, Button, Box, Text,
    Table,
    Thead,
    Tbody,
    Tr,
    Th,
    Td,
    TableContainer,
    useToast
} from '@chakra-ui/react'
import { useEffect, useState } from 'react';

export default function Backup() {

    const [backupScenarioList, getBackupScenarioList] = useState([]);
    //const backupRulesList = useState();
    var backupId;

    const toast = useToast();


    function fetchBackup() {
        //to set profileId(/accountId) in Login.js, using this because it already has rules/scenarios in db
        //not sure if accountId is the same as the profileId saved in Scenarios
        localStorage.setItem('profileId', '22222222-2222-2222-2222-222222222222')
        fetch("https://localhost:7140/api/Backup/loadBackupScenario/" + localStorage.getItem('profileId'))
            .then((response) => response.json())
            .then((data) => {
                //console.log(data);
                getBackupScenarioList(data);
            });
    }

    useEffect(() => {
        fetchBackup();
    }, []);

   

    function buttonClicked(scenario, versionNo) {
        var buttonClickedId = "button" + versionNo
        
        for (var i = 1; i <= backupScenarioList.length; i++) {
            if (buttonClickedId == "button" + i) {
                document.getElementById(buttonClickedId).style.backgroundColor = "darkgrey"
                document.getElementById(buttonClickedId).innerText = "Selected"
            }
            else {
                document.getElementById("button" + i).style.backgroundColor = "#EDF2F7"
                document.getElementById("button" + i).innerText = "Select"
            }
        }

        //console.log(scenario);

        var ts = new Date(scenario.createdAt)

        backupId = scenario.backupId;

        document.getElementById("showSelected").innerText = "Version selected: " + "v" + versionNo + " - " + ts.toLocaleDateString('en-GB') + ' ' + ts.toLocaleTimeString('en-GB')
        document.getElementById("showSelected").style.display = "block";
    }

    function onSubmit(e) {
        //console.log(profileId + " and " + backupId)

        fetch("https://localhost:7140/api/Backup/restoreBackup", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ "profileId": localStorage.getItem('profileId'), "backupId": backupId })
        }).then(async (response) => {

            //console.log(await response.text())

            if (response.ok) {
                toast({
                    title: "Success",
                    description: "Backup has been restored successfully.",
                    status: "success",
                    duration: 9000,
                    isClosable: true,
                });
            }
            else {
                toast({
                    title: "Error",
                    description: "Backup has failed.",
                    status: "error",
                    duration: 9000,
                    isClosable: true,
                });
            }
        });
                

    }

    let listLength = backupScenarioList.length;    

    return (
        <Box>
            <Box textAlign='center' h='300px'>
                <Center h='200px'>
                    <Heading>Backup</Heading>
                </Center>
                <Button onClick={onSubmit}>Restore Backup</Button>

                <Text id='showSelected' style={{ display: "none" }} >Version selected: (version number) - (timestamp)</Text>
            </Box>
            <Box>
                <TableContainer>
                    <Table>
                        <Thead>
                            <Tr>
                                <Th>Timestamp</Th>
                                <Th>Version</Th>
                                <Th></Th>
                            </Tr>
                        </Thead>
                        <Tbody>
                            {
                                backupScenarioList.reverse().map((scenario, count) => {
                                    let ts = new Date(scenario.createdAt)
                                    
                                    return (
                                        <Tr key={listLength - count}>
                                            <Td>{ts.toLocaleDateString('en-GB') + ' ' + ts.toLocaleTimeString('en-GB')}</Td>
                                            <Td>v{listLength - count}</Td>
                                            <Td display="none">{scenario.backupId}</Td>
                                            <Td>
                                                <Button id={"button" + (listLength - count).toString()} onClick={() => buttonClicked(scenario, listLength - count)} width="94px">Select</Button>
                                            </Td>
                                        </Tr>
                                    )
                                })
                            }
                            
                        </Tbody>
                    </Table>
                </TableContainer>
            </Box>
        </Box>




    )
}
