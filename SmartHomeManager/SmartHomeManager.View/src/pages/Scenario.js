import {
    Heading,
    Box
} from '@chakra-ui/react'   
import React from "react";
import Buttons from "components/Automation/Buttons"
import JsonToTable from "components/Automation/JsonToTable"



export default function Scenarios() {
    return <Box padding='16'>
        <Heading alignContent="center" >Profile : Wen Jun</Heading>
        <Box h='40px'>
        </Box>
        <JsonToTable />
    </Box>;
}
