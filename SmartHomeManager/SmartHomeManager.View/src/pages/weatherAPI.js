import React from "react";
import {
    Heading, Center, Container, Button, Box, Text,
    useToast,
    Table,
    Thead,
    Tbody,
    Tr,
    Th,
    Td,
    TableContainer,
    Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Checkbox, Stack
} from '@chakra-ui/react'
import { useEffect, useState } from 'react';

export default function WeatherAPI() {

    const [APIList, getApiData] = useState([]);
    //const backupRulesList = useState();


    const toast = useToast();


    function fetchAPI() {
        fetch("https://localhost:7140/api/API/getAPIData/")
            .then((response) => response.json())
            .then((data) => getApiData(data));
    }


    useEffect(() => {
        fetchAPI();
    }, []);

    return (
        <Box>
            <Box textAlign='center' marginBottom={6} >
                <Center h='200px'>
                    <Heading>APIData</Heading>
                </Center>
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
                                    APIList.map((item_type, count) => {
                                        let ts = new Date(item_type.TimeStamp)

                                    return (
                                        <Tr key={count+1}>
                                            <Td>{ts.toLocaleDateString('en-GB') + ' ' + ts.toLocaleTimeString('en-GB')}</Td>
                                            <Td>v{count}</Td>
                                            <Td display="none">{item_type.APIDataId}</Td>
                                            
                                        </Tr>
                                    )
                                })}
                            </Tbody>
                    </Table>
                    </TableContainer>
                </Box>
            </Box>
        </Box>
    )



   

    







    
}