import {
    Accordion,
    AccordionItem,
    AccordionPanel,
    AccordionIcon,
    AccordionButton,
    Box,
    Container,
    Heading
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



    return (
        <Container mt="3%">
            <Heading>
                Console History
            </Heading>

            <Accordion mt="10%" allowMultiple>
                {historyList ? historyList.map((history, i) => {
                    return (
                        <AccordionItem>
                            <Heading as='h2' >
                                <AccordionButton>
                                    <Box as="span" flex='1' textAlign='left'>
                                        {history.message}
                                    </Box>
                                    <AccordionIcon />
                                </AccordionButton>
                            </Heading>
                            <AccordionPanel pb="4">
                                text
                            </AccordionPanel>
                        </AccordionItem>

                    );
                })
                : null}                
            </Accordion>
        </Container>
    )
}