import {
    Accordion,
    AccordionItem,
    AccordionPanel,
    AccordionIcon,
    AccordionButton,
    Box,
    Container,
    Heading,
    Table,
    Thead,
    Tbody,
    Tfoot,
    Tr,
    Th,
    Td,
    TableCaption,
    TableContainer,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";

export default function Director() {

    const [historyList, getHistory] = useState([]);
    

    function fetchHistory() {
        fetch("https://localhost:7140/api/History/GetAllHistory/")
            .then((response) => response.json())
            .then((data) => getHistory(data));
    }

    useEffect(() => {
        fetchHistory();
    }, []);

    let historyLength = historyList.length

    return (
        <Container mt="3%">
            <Heading>
                Console History
            </Heading>

            <Accordion mt="10%" allowMultiple>
                {historyList ? historyList.reverse().map((history, i) => {
                    let ts = new Date(history.timestamp)
                    return (
                        <AccordionItem key={history.historyId}>
                            <Heading as='h2' >
                                <AccordionButton>
                                    <Box as="span" flex='1' textAlign='left'>
                                        <strong>Rule #{historyLength - i}: </strong> {history.message}                                                                               
                                    </Box>
                                    <AccordionIcon />
                                </AccordionButton>
                            </Heading>
                            <AccordionPanel pb="4">
                                <TableContainer>
                                    <Table variant='simple'>
                                        <Thead>
                                            <Tr>
                                                <Th>Fields</Th>
                                                <Th>Values</Th>
                                            </Tr>
                                        </Thead>
                                        <Tbody>
                                            <Tr>
                                                <Td>Scenario</Td>
                                                <Td>{history.ruleHistory.scenarioName}</Td>
                                            </Tr>
                                            <Tr>
                                                <Td>Device</Td>
                                                <Td>{history.ruleHistory.deviceName}</Td>
                                            </Tr>
                                            <Tr>
                                                <Td>Timestamp</Td>
                                                <Td>{ts.toLocaleDateString('en-GB') + ' ' + ts.toLocaleTimeString('en-GB')}</Td>
                                            </Tr>
                                            <Tr>
                                                <Td>ActionTrigger</Td>
                                                <Td>{history.ruleHistory.actionTrigger ?? "-"}</Td>
                                            </Tr>
                                            <Tr>
                                                <Td>Config Value</Td>
                                                <Td>{history.ruleHistory.configurationValue}</Td>
                                            </Tr>
                                        </Tbody>
                                    </Table>
                                </TableContainer> 
                            </AccordionPanel>
                        </AccordionItem>
                    );
                })
                : null}                
            </Accordion>
        </Container>
    )
}