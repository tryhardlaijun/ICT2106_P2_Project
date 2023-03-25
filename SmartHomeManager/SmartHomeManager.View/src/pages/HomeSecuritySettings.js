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
    const [homeSecuritySettings, setHomeSecuritySettings] = useState([])
    const [dataLoaded, setDataLoaded] = useState(false);

    async function getHomeSecurity() {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/GetSecurityMode?AccountId=${accountId}`)
            if (response.status == 200) {
                setCurrentSecurityMode(await response.json())
            }
        }
        catch (error) {
            console.log(error);
        }
    }

    async function getHomeSecuritySettings() {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/GetHomeSecuritySettings?AccountId=${accountId}`)
            if (response.status == 200) {
                setHomeSecuritySettings(await response.json())
            }
        }
        catch (error) {
            console.log(error);
        }
        finally {
            setDataLoaded(true)
        }
    }

    useEffect(() => {
        getHomeSecurity()
            .then(() => getHomeSecuritySettings())
    }, []);
    //}, [homeSecuritySettings]);

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
                body: JSON.stringify({ deviceGroup: deviceGroup, enabled: !newEnabled })
            })
            if (response.ok) {
                let newArr = [...homeSecuritySettings];

                // locate array index of device group
                var index = newArr.findIndex(function (item, i) {
                    return item.deviceGroup == deviceGroup
                });

                // invert enabled value
                newArr[index].enabled = !(newArr[index].enabled)
            } else {
                console.error(response.statusText)
            }
        } catch (error) {
            console.error(error)
        }
    };

    function renderDeviceGroups() {
        const tableRows = []
        homeSecuritySettings.forEach(function (obj) {
            tableRows.push(<Tr>
                <Td>{capitalizeFirstLetter(obj.deviceGroup)}</Td>
                <Td><Switch id={obj.deviceGroup} defaultChecked={obj.enabled}
                    onChange={(e) => PutHomeSecuritySettings(accountId, obj.deviceGroup, obj.enabled)} />
                </Td>
            </Tr>)
        })

        return tableRows
    }

    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    return (
        <Container maxW='container.sm'>
            <Box textAlign='center' h='80px'>
                <Heading>Home Security Settings</Heading>
            </Box>
            <Box border='2px' borderRadius='16px'>
                <SimpleGrid columns={2} spacingX='40px' spacingY='20px' padding='4px'>
                    <Text fontSize='lg' textAlign='center'>Activate HomeSecurity</Text>
                    {dataLoaded && (<Switch id='switchMasterActivation' defaultChecked={currentSecurityMode} onChange={(e) => PutSecurityMode(accountId, currentSecurityMode)} />)}
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
                        {dataLoaded && renderDeviceGroups()}
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
            <br></br>
            <Link href='/homesecuritytriggering' isExternal>
                Triggering Page (temp) <ExternalLinkIcon mx='2px' />
            </Link>
        </Container>
    )

}