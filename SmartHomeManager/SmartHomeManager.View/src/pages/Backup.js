import React from "react";
import {
    Heading, Center, Container, Button, Box, Text,
    useToast,
    Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Checkbox, Stack
} from '@chakra-ui/react'
import { useEffect, useState } from 'react';

export default function Backup() {

    const [backupScenarioList, getBackupScenarioList] = useState([]);
    //const backupRulesList = useState();
    var backupId;
    var versionSelected;

    var checkedItems = []

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

        //dummy data to test multiple scenarios and checkboxes
        /*getBackupScenarioList([
            {
                "backupId": "7dfab49a-2fac-48ab-b30d-c880a484a198",
                "scenarioId": "ac38af14-9a57-4df3-89f3-78f9ce9f4983",
                "scenarioName": "Default",
                "profileId": "22222222-2222-2222-2222-222222222222",
                "createdAt": "2023-03-13T14:49:00.2565626"
            }, {
                "backupId": "7dfab49a-2fac-48ab-b30d-c880a484a198",
                "scenarioId": "ac38af14-9a57-4df3-89f3-78f9ce9f4983",
                "scenarioName": "Default2",
                "profileId": "22222222-2222-2222-2222-222222222222",
                "createdAt": "2023-03-13T14:49:00.2565626"
            }, {
                "backupId": "7dfab49a-2fac-48ab-b30d-c880a484a198",
                "scenarioId": "ac38af14-9a57-4df3-89f3-78f9ce9f4983",
                "scenarioName": "Default3",
                "profileId": "22222222-2222-2222-2222-222222222222",
                "createdAt": "2023-03-13T14:49:00.2565626"
            },
            {
                "backupId": "d20d25b5-68b7-4298-aa5f-6828e6b52a60",
                "scenarioId": "ac38af14-9a57-4df3-89f3-78f9ce9f4983",
                "scenarioName": "Default",
                "profileId": "22222222-2222-2222-2222-222222222222",
                "createdAt": "2023-03-13T14:15:37.2749117"
            },
            {
                "backupId": "bdec64f4-1abe-4376-a815-7a39e5ef6188",
                "scenarioId": "ac38af14-9a57-4df3-89f3-78f9ce9f4983",
                "scenarioName": "Default",
                "profileId": "22222222-2222-2222-2222-222222222222",
                "createdAt": "2023-03-12T22:10:51.2581587"
            }

        ])*/
    }

    useEffect(() => {
        fetchBackup();
    }, []);


   

    function buttonClicked(scenario, versionNo) {
        versionSelected = versionNo;

        //console.log(scenario);

        var ts = new Date(scenario.createdAt)

        backupId = scenario.backupId;

        document.getElementById("versionSelected").innerText = "Version selected: " + "v" + versionNo + " - " + ts.toLocaleDateString('en-GB') + ' ' + ts.toLocaleTimeString('en-GB')
        document.getElementById("showSelected").style.display = "block";

        document.getElementById("scenariosSelected").innerText = "";
        checkedItems[versionSelected - 1].forEach(item => {
            if (checkedItems[versionSelected - 1].indexOf(item) == 0) {
                document.getElementById("scenariosSelected").innerText += item.scenarioName;
            }
            else {
                document.getElementById("scenariosSelected").innerText += ", " + item.scenarioName;
            }
        })
    }

    function checkBoxChanged(checked, scenario) {
        
        //console.log(checked);
        if (checked == true) {
            checkedItems[versionSelected - 1].push(scenario);
        }
        else {
            const index = checkedItems[versionSelected - 1].indexOf(scenario);
            checkedItems[versionSelected - 1].splice(index, 1);
        }
        //console.log("checkedItems: ", checkedItems);

        document.getElementById("scenariosSelected").innerText = "";
        checkedItems[versionSelected - 1].forEach(item => {
            if (checkedItems[versionSelected - 1].indexOf(item) == 0) {
                document.getElementById("scenariosSelected").innerText += item.scenarioName;
            }
            else {
                document.getElementById("scenariosSelected").innerText += ", " + item.scenarioName;
            }
        })
        
        document.getElementById("showScenariosSelected").style.display = "block";
    }

    function onSubmit() {
        //console.log(backupId, "and", versionSelected)
        var scenarioIDList = []
        checkedItems[versionSelected - 1].forEach(item => {
            scenarioIDList.push(item.scenarioId);
        })

        //console.log("scenarioIDList: ", scenarioIDList);

        fetch("https://localhost:7140/api/Backup/restoreBackup", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ "profileId": localStorage.getItem('profileId'), "backupId": backupId, "scenarioIdList": scenarioIDList })
        }).then(async (response) => {

            //console.log(await response.text())

            if (response.ok) {
                toast({
                    title: "Success",
                    description: "Backup v" + versionSelected + " has been restored successfully.",
                    status: "success",
                    duration: 9000,
                    isClosable: true,
                });
            }
            else {
                toast({
                    title: "Error",
                    description: "Backup v" + versionSelected + " has failed.",
                    status: "error",
                    duration: 9000,
                    isClosable: true,
                });
            }
        });
                

    }

    //console.log(backupScenarioList);

    let uniqueBackupList = []
    var noOfUniqueRows = [...new Set(backupScenarioList.map(b=> b.backupId))].length

    return (
        <Box>
            <Box textAlign='center' marginBottom={6} >
                <Center h='200px'>
                    <Heading>Backup</Heading>
                </Center>
                <Button onClick={onSubmit}>Restore Backup</Button>

                <Box minH='20'>
                    <Box id="showSelected" style={{ display: "none" }} >
                        <Text id='versionSelected'>Version selected: (version number) - (timestamp)</Text>
                        <Text>Scenarios selected:</Text>
                    </Box>
                
                    <Box id="showScenariosSelected" style={{ display: "none" }}>
                        <Container id='scenariosSelected' maxW='xl'></Container>
                    </Box>
                </Box>
            </Box>
            <Box>

                <Accordion>
                    {
                        backupScenarioList.reverse().map((backup, count) => {
                            let ts = new Date(backup.createdAt)
                            //if have multiple scenarios in one backup, itll show multiple accordions for the same backup, so we need to only show the first one in the accordion
                            if (uniqueBackupList.indexOf(backup.backupId) == -1) {
                                uniqueBackupList.push(backup.backupId);
                                //initialize checkedItems to empty list for each accordion row
                                checkedItems.push([]);
                                //console.log(checkedItems);

                                return (
                                    <AccordionItem key={noOfUniqueRows - count}>
                                        <h2>
                                            <AccordionButton onClick={() => buttonClicked(backup, noOfUniqueRows-count)}>
                                                <Box as="span" flex='1' textAlign='left'>
                                                    v{noOfUniqueRows - count} - {ts.toLocaleDateString('en-GB') + ' ' + ts.toLocaleTimeString('en-GB')}
                                                </Box>
                                                <Box>{backup.backupId}</Box>
                                                <AccordionIcon />
                                            </AccordionButton>
                                        </h2>

                                        <AccordionPanel pb={4}>
                                            <Box width="90%" justifyContent="center" margin="0 auto">
                                                {
                                                    //and put the the individual scenarios into the accordion panel based on their backupid
                                                    backupScenarioList.map((scenario, count) => {
                                                        if (scenario.backupId == backup.backupId) {
                                                            return (
                                                                <Stack key={count} pl={6} mt={1} spacing={1}>
                                                                    <Checkbox
                                                                        onChange={(e) => checkBoxChanged(e.target.checked, scenario)}                                                                    >
                                                                        {scenario.scenarioName}
                                                                    </Checkbox>
                                                                </Stack>
                                                            )
                                                        }
                                                    })
                                                }
                                                

                                                
                                            </Box>

                                        </AccordionPanel>
                                    </AccordionItem>
                                )
                            }
                        })
                    
                    }
                
                </Accordion>


                
            </Box>
        </Box>




    )
}
