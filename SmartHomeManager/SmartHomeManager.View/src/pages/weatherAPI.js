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

    const [getDataList] = useState([]);
    //const backupRulesList = useState();
   

    const toast = useToast();


    function fetchAPI() {
        fetch("https://localhost:7140/api/API/getAPIData/")
            .then((response) => response.json())
            .then((data) => getHistory(data));
    }


    useEffect(() => {
        fetchAPI();
    }, []);

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
            
          )



   

    







    
}