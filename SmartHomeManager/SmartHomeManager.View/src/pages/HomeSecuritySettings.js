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
    Link,
    AlertDialog,
    AlertDialogBody,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogContent,
    AlertDialogOverlay
} from '@chakra-ui/react'

import { ExternalLinkIcon } from '@chakra-ui/icons'
import axios from "axios";
import { tab } from "@testing-library/user-event/dist/tab";

export default function HomeSecuritySettings() {
    const { isOpen, onOpen, onClose } = useDisclosure()
    const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111")
    const [alertedState, setAlertedState] = useState(false)
    const [lockdownState, setLockdownState] = useState(false)
    const [currentSecurityMode, setCurrentSecurityMode] = useState(false)
    const [homeSecuritySettings, setHomeSecuritySettings] = useState([])
    const [deviceLog, setDeviceLog] = useState([])
    const [dataLoaded, setDataLoaded] = useState(false);

    async function getAlertedState() {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/IsAccountAlerted?AccountId=${accountId}`)
            if (response.status == 200) {
                setAlertedState(await response.json())
            }
        }
        catch (error) {
            console.log(error);
        }
    }

    async function getLockdownState() {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/IsAccountLockedDown?AccountId=${accountId}`)
            if (response.status == 200) {
                setLockdownState(await response.json())
            }
        }
        catch (error) {
            console.log(error);
        }
    }

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

    async function getAllDeviceLogs() {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/GetAllTriggeredDeviceLogs?AccountId=${accountId}`)
            if (response.status == 200) {
                setDeviceLog(await response.json())
            }
        }
        catch (error) {
            console.log(error);
        }
    }

    useEffect(() => {
        setInterval(async () => {
            getAllDeviceLogs()
        }, 10000);
    }, []);

    useEffect(() => {
        getAlertedState()
            .then(() => getLockdownState())
            .then(() => getHomeSecurity())
            .then(() => getHomeSecuritySettings())
    }, []);

    const PutLockdownState = async (accountId, securityMode) => {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/PutLockDownState/${accountId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ securityMode: securityMode })
            })
            if (response.ok) {

            } else {
                console.error(response.statusText)
            }
        } catch (error) {
            console.error(error)
        }
    };

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

    function renderSomeList() {
        const tableRows = []
        deviceLog.forEach(function (obj, index) {
            console.log(obj)
            tableRows.push(
                <Tbody>
                    <Tr>
                        <Td>{index + 1}</Td>
                        <Td>{obj}</Td>
                    </Tr>
                </Tbody>
            )
        })

        return tableRows
    }

    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    function AlertDialogExample() {
        const { isOpen, onOpen, onClose } = useDisclosure({ defaultIsOpen: true })
        const cancelRef = React.useRef()

        if (!dataLoaded) {
            return (<></>)
        }

        if (dataLoaded && !alertedState) {
            return (<></>)
        }

        if (dataLoaded && alertedState && lockdownState) {
            return (
                <>
                    <AlertDialog
                        closeOnOverlayClick={false}
                        isOpen={isOpen}
                        leastDestructiveRef={cancelRef}
                        onClose={onClose}
                    >
                        <AlertDialogOverlay>
                            <AlertDialogContent>
                                <AlertDialogHeader fontSize='lg' fontWeight='bold'>
                                    Intruder Detected!
                                </AlertDialogHeader>

                                <AlertDialogBody>
                                    Select &#34;Cancel Alert&#34; to lower alert.
                                </AlertDialogBody>
                                <AlertDialogBody>
                                    &#34;Contact Police&#34; will contact the authorities.
                                </AlertDialogBody>

                                <AlertDialogFooter>
                                    <Button colorScheme='yellow' ref={cancelRef} onClick={() => { onClose(); PutLockdownState(accountId, false); location.reload(); }} ml={3}>
                                        Cancel Alert
                                    </Button>
                                    <Button colorScheme='blue' onClick={onClose} ml={3}>
                                        Contact Police
                                    </Button>
                                </AlertDialogFooter>
                            </AlertDialogContent>
                        </AlertDialogOverlay>
                    </AlertDialog>
                </>
            )
        }

        return (
            <>
                <AlertDialog
                    closeOnOverlayClick={false}
                    isOpen={isOpen}
                    leastDestructiveRef={cancelRef}
                    onClose={onClose}
                >
                    <AlertDialogOverlay>
                        <AlertDialogContent>
                            <AlertDialogHeader fontSize='lg' fontWeight='bold'>
                                Intruder Detected!
                            </AlertDialogHeader>

                            <AlertDialogBody>
                                Select &#34;Lockdown&#34; to enter lockdown mode!
                            </AlertDialogBody>
                            <AlertDialogBody>
                                Select &#34;Cancel Alert&#34; to lower alert.
                            </AlertDialogBody>

                            <AlertDialogFooter>
                                <Button colorScheme='red' onClick={() => { onClose(); PutLockdownState(accountId, true); location.reload(); }}>
                                    Lockdown
                                </Button>
                                <Button colorScheme='yellow' ref={cancelRef} onClick={() => { onClose(); PutLockdownState(accountId, false); location.reload(); }} ml={3}>
                                    Cancel Alert
                                </Button>
                            </AlertDialogFooter>
                        </AlertDialogContent>
                    </AlertDialogOverlay>
                </AlertDialog>
            </>
        )
    }

    return (
        <Container maxW='container.sm'>
            {AlertDialogExample()}
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
                            {renderSomeList()}
                        </Table>
                    </TableContainer>
                </Box>
                <Text fontSize='sm' textAlign='center'>Device Trigger Logs</Text>
            </Box>
            <br></br>
            <Link href='/homesecuritytriggering' isExternal>
                Triggering Page (temp) <ExternalLinkIcon mx='2px' />
            </Link>
        </Container>
    )

}