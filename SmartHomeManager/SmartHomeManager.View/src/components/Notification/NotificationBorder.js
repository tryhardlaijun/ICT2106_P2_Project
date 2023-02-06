import React, {useState} from "react";
import { Box, Text } from "@chakra-ui/react";

export default function NotificationBorder({ message, sentTime }){
    
    return(
     <>
        <Box border="1px" borderColor="gray.500" boxShadow="xl" rounded="lg" mb="5px">
            <Text fontWeight="bold" fontSize="xl" ml={3} mt={3}>{message}</Text>
            <Text fontSize="s" color="gray.500" ml={3} mt={3} mb={3}>{sentTime} minutes ago</Text>
        </Box>
    </>
    )
}