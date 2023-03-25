import React, { useState, useEffect, useRef } from "react";
import {
    Heading,
    Container,
    Table,
    Thead,
    Tbody,
    Tfoot,
    Tr,
    Th,
    Td,
    TableCaption,
    TableContainer,
    Switch,
    Text,
    Box,
    SimpleGrid,
    useDisclosure,
    useToast,
    Button,
    ButtonGroup,
    Link
} from '@chakra-ui/react'

import { ExternalLinkIcon } from '@chakra-ui/icons'
import axios from "axios";

export default function HomeSecuritySettings() {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111")
    const [currentSecurityMode, setCurrentSecurityMode] = useState(false)
    const [homeSecuritySettings, sethomeSecuritySettings] = useState([])
    const [homeSecuritySettingsEnabled, sethomeSecuritySettingsEnabled] = useState(false,false,false,false)
    const [dataLoaded, setDataLoaded] = useState(false);

    useEffect(() => {
        const getHomeSecurity = async () => {
            try {
                const response = await fetch(`https://localhost:7140/api/HomeSecurity/GetSecurityMode?AccountId=${accountId}`)
                if (response.status == 200) {
                    setCurrentSecurityMode(await response.json())
                }
            }
            catch (error) {
                console.log(error);
            }
        };
        getHomeSecurity();
    }, [currentSecurityMode]);

    useEffect(() => {
        const getHomeSecuritySettings = async () => {
            try {
                const response = await fetch(`https://localhost:7140/api/HomeSecurity/GetHomeSecuritySettings?AccountId=${accountId}`)
                if (response.status == 200) {
                    sethomeSecuritySettings(await response.json())
                }
            }
            catch (error) {
                console.log(error);
            }
            finally {
                setDataLoaded(true)
            }
        };
        getHomeSecuritySettings();
    }, [homeSecuritySettings]);

    const PutSecurityMode = async (accountId, newSecurityMode) => {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/PutSecurityMode/${accountId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ securityMode: !newSecurityMode })
            })
            if (response.ok) {
                setCurrentSecurityMode(!currentSecurityMode)
            } else {
                console.error(response.statusText)
            }
        } catch (error) {
            console.error(error)
        }
    };

    const PutHomeSecuritySettings = async (accountId, deviceGroup, newEnabled) => {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/PutHomeSecuritySettings/${accountId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ deviceGroup: deviceGroup, enabled: newEnabled })
            })
            if (!response.ok) {
                console.error(response.statusText)
            }
        } catch (error) {
            console.error(error)
        }
    };

    const handleAddEnabled = (newEnabled) => {
        const newEnables = homeSecuritySettingsEnabled.concat(newEnabled);
        sethomeSecuritySettingsEnabled(newEnables);
    };

    return (
        <Container maxW='container.sm'>
            <Box textAlign='center' h='80px'>
                <Heading>Home Security Settings</Heading>
            </Box>
            <Box border='2px' borderRadius='16px'>
                <SimpleGrid columns={2} spacingX='40px' spacingY='20px' padding='4px'>
                    <Text fontSize='lg' textAlign='center'>Activate HomeSecurity</Text>
                    {dataLoaded && (<Switch id='switchMasterActivation' defaultChecked={currentSecurityMode} onChange={(e) => PutSecurityMode(accountId, currentSecurityMode)} />)}
                    {/*{console.log("fish " + dataLoaded + " " + currentSecurityMode)}*/}
                </SimpleGrid>
            </Box>

            <br></br>
            {/*isChecked defaultChecked isFocusable isDisabled isInvalid isReadOnly isRequired*/}
            <TableContainer border='2px' borderRadius='16px'>
                <Table variant='simple'>
                    <TableCaption>Device Activation Settings</TableCaption>
                    <Thead>
                        <Tr>
                            <Th>Device Group</Th>
                            <Th>Will Activate?</Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        <Tr>
                            <Td>Door</Td>
                            <Td>{dataLoaded && (<Switch id='switchDoor' defaultChecked={homeSecuritySettings[0].enabled} onChange={(e) => PutHomeSecuritySettings(accountId, homeSecuritySettings[0].deviceGroup)} />)}</Td>
                        </Tr>
                        <Tr>
                            <Td>Window</Td>
                            <Td>{dataLoaded && (<Switch id='switchWindow' defaultChecked={homeSecuritySettings[1].enabled} onChange={(e) => PutHomeSecuritySettings(accountId, homeSecuritySettings[1].deviceGroup)} />)}</Td>
                        </Tr>
                        <Tr>
                            <Td>Alarm</Td>
                            <Td>{dataLoaded && (<Switch id='switchAlarm' defaultChecked={homeSecuritySettings[2].enabled} onChange={(e) => PutHomeSecuritySettings(accountId, homeSecuritySettings[2].deviceGroup)} />)}</Td>
                        </Tr>
                        <Tr>
                            <Td>Gate</Td>
                            <Td>{dataLoaded && (<Switch id='switchGate' defaultChecked={homeSecuritySettings[3].enabled} onChange={(e) => PutHomeSecuritySettings(accountId, homeSecuritySettings[3].deviceGroup)} />)}</Td>
                        </Tr>
                    </Tbody>
                </Table>
            </TableContainer>
            <br></br>
            <Box border='2px' borderRadius='16px'>
                <Box overflowY="auto" minHeight="300px" maxHeight="300px" >
                    <TableContainer>
                        <Table variant='simple'>
                            <Tbody>
                                <Tr>
                                    <Td>1</Td>
                                </Tr>
                            </Tbody>
                            <Tbody>
                                <Tr>
                                    <Td>2</Td>
                                </Tr>
                            </Tbody>
                            <Tbody>
                                <Tr>
                                    <Td>3</Td>
                                </Tr>
                            </Tbody>
                            <Tbody>
                                <Tr>
                                    <Td>4</Td>
                                </Tr>
                            </Tbody>
                        </Table>
                    </TableContainer>
                </Box>
                <Text fontSize='sm' textAlign='center'>Device Trigger Logs</Text>
            </Box>
            <br></br>
            <Link href='/intruder' isExternal>
                Intruder Page (temp) <ExternalLinkIcon mx='2px' />
            </Link>
        </Container>
    )

}