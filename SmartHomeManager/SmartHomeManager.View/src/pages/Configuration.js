import React from "react";
import {
    Heading, Center, Container, Text, VStack, HStack, Switch
} from '@chakra-ui/react'

export default function Configuration() {
    return (
        <Container maxW='container.sm'>
            <Center h='200px'>
                    <Heading>Configuration</Heading>
            </Center>
            <VStack>
                <HStack spacing={8}>
                    <Text>Rule 1 text</Text>
                    <Switch></Switch>
                </HStack>
                <HStack spacing={8}>
                    <Text>Rule 2 text</Text>
                    <Switch></Switch>
                </HStack>
                <HStack spacing={8}>
                    <Text>Rule 3 text</Text>
                    <Switch></Switch>
                </HStack>
                <HStack spacing={8}>
                    <Text>Rule 4 text</Text>
                    <Switch></Switch>
                </HStack>
                <HStack spacing={8}>
                    <Text>Rule 5 text</Text>
                    <Switch></Switch>
                </HStack>
                <HStack spacing={8}>
                    <Text>Rule 6 text</Text>
                    <Switch></Switch>
                </HStack>
            </VStack>


        </Container>


    )
}
