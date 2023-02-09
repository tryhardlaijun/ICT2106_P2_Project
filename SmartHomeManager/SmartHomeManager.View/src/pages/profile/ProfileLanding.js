import { React, useState } from 'react';
import { CardComponent, } from "components/Card";
import { Grid, GridItem, Box } from "@chakra-ui/react"

export default function ProfileLanding() {
    return (
        <>
            <Grid templateColumns='repeat(3, 1fr)' gap={6} paddingTop = "5em" paddingLeft = "2.5em">
                <Box w="100%" m="0 auto" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent imgSrc="https://bit.ly/sage-adebayo">
                    </CardComponent>
                </Box>

                <Box w="100%" m="0 auto" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent imgSrc="https://cdn.i-scmp.com/sites/default/files/styles/1200x800/public/d8/images/canvas/2022/05/19/4eb393f3-d671-4d58-8680-89ccd6607acf_5e3be874.jpg?itok=CEBu_LXQ&v=1652966452">
                    </CardComponent>
                </Box>

                <Box w="100%" m="0 auto" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent imgSrc="https://i.ytimg.com/vi/DJu4zGX8YmY/maxresdefault.jpg">
                    </CardComponent>
                </Box>

                <Box w="100%" m="0 auto" mt="10em" maxW="400px" h="100px" bg="gray.50">
                    <CardComponent imgSrc="https://upload.wikimedia.org/wikipedia/commons/1/18/Mark_Zuckerberg_F8_2019_Keynote_(32830578717)_(cropped).jpg">
                    </CardComponent>
                </Box>
            </Grid>
        </>

        
    )
}
