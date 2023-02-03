import React, {useState} from "react";
import { Box, Text } from "@chakra-ui/react";





export default function NotificationBorder({text1,text2}){


    
    return(
     <>
        <Box border="1px" borderColor="gray.500" boxShadow="xl" rounded="sm" mb="5px">
            <Text fontWeight="bold" fontSize="xl">{text1}</Text>
            <Text fontSize="s" color="gray.500">{text2}</Text>
        </Box>
    </>
    )
}