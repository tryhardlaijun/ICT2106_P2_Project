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
import React from "react";

export default function Director() {
    return (
        <Container mt="3%">
            <Heading>
                Console History
            </Heading>
            <Accordion mt= "10%" allowMultiple>
                <AccordionItem>
                    <Heading as='h2' >
                        <AccordionButton>
                            <Box as="span" flex='1' textAlign='left'>
                                Section 1 title
                            </Box>
                            <AccordionIcon />
                        </AccordionButton>
                    </Heading>
                    <AccordionPanel pb="4">
                        text
                    </AccordionPanel>
                </AccordionItem>
            </Accordion>
        </Container>
    )
}