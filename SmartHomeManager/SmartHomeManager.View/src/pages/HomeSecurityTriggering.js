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
    return (
        <Container maxW='container.sm'>
            <Box textAlign='center' h='80px'>
                <Heading>Home Security Triggers</Heading>
            </Box>
            <Box borderRadius='16px'>
                <SimpleGrid columns={1} spacingX='40px' spacingY='20px' padding='4px'>
                    <Center>
                        <WrapItem>
                            <Button id='btnCamera' colorScheme='blue'>Trigger Camera</Button>
                        </WrapItem>
                    </Center>
                </SimpleGrid>
                <SimpleGrid columns={1} spacingX='40px' spacingY='20px' padding='4px' >
                    <Center>
                        <WrapItem>
                            <Button id='btnMicrophone' colorScheme='blue'>Trigger Microphone</Button>
                        </WrapItem>
                    </Center>
                </SimpleGrid>
            </Box>
        </Container>
    )
}