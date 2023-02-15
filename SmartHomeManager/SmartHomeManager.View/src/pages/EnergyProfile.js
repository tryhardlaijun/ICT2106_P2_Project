import { Box, Container, Heading } from "@chakra-ui/react";
import React from "react";

export default function EnergyProfile() {
    return (
        <Container mt="3%">
            <Heading>Energy Profile Manager</Heading>
            <Box maxW='sm' borderWidth='1px' borderRadius='lg' overflow='hidden' mt="3%"> 
                <Box p='6'>
                    <Box display='flex' alignItems='center' justifyContent='center'>
                        Choose Energy Profile
                    </Box>

                    <Box p='6' display='flex' alignItems='center' justifyContent='center'>
                        <Box as="button" p={[5, 5, 5, 5]} marginRight="10%" borderWidth='3px' w="25%" h="70px"> 0 </Box>
                        <Box as="button" p={[5, 5, 5, 5]} borderWidth='3px' w="25%" h="70px"> 1 </Box>
                        <Box as="button" p={[5, 5, 5, 5]} marginLeft="10%" borderWidth='3px' w="25%" h="70px"> 2 </Box>
                    </Box>

                    <Box as='span' ml='2' color='gray.500' fontSize='xs' display='flex' alignItems='center' justifyContent='center'>
                        Selecting the above options might help you manage your home&apos;s energy use
                    </Box>
                </Box>
            </Box>
        </Container>
        
    )
}