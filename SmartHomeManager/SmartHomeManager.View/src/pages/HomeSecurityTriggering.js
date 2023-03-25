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
    Center,
    WrapItem
} from '@chakra-ui/react'

import { ExternalLinkIcon } from '@chakra-ui/icons'
import axios from "axios";

export default function HomeSecurityTriggering() {
    const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111")

    const PutSecurityMode = async (accountId, DeviceGroup, ConfigurationKey, ConfigurationValue) => {
        try {
            const response = await fetch(`https://localhost:7140/api/HomeSecurity/PutHomeSecurityTrigger/${accountId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ deviceGroup: DeviceGroup, configurationKey: ConfigurationKey, configurationValue: ConfigurationValue })
            })
            if (response.ok) {
                return true
            } else {
                console.error(response.statusText)
            }
        } catch (error) {
            console.error(error)
        }
    };

    function OnClickCamera(event) {
        PutSecurityMode(accountId, "camera", "motion", 1);
    }

    function OnClickMicrophone(event) {
        PutSecurityMode(accountId, "microphone", "audio", 1);
    }

    return (
        <Container maxW='container.sm'>
            <Box textAlign='center' h='80px'>
                <Heading>Home Security Triggers</Heading>
            </Box>
            <Box borderRadius='16px'>
                <SimpleGrid columns={1} spacingX='40px' spacingY='20px' padding='4px'>
                    <Center>
                        <WrapItem>
                            <Button id='btnCamera' colorScheme='blue' onClick={OnClickCamera} >Trigger Camera</Button>
                        </WrapItem>
                    </Center>
                </SimpleGrid>
                <SimpleGrid columns={1} spacingX='40px' spacingY='20px' padding='4px' >
                    <Center>
                        <WrapItem>
                            <Button id='btnMicrophone' colorScheme='blue' onClick={OnClickMicrophone} >Trigger Microphone</Button>
                        </WrapItem>
                    </Center>
                </SimpleGrid>
            </Box>
        </Container>
    )
}