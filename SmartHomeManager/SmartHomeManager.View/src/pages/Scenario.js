import {
    Heading,
    Box,
    Menu,
    MenuItem,
    MenuButton,
    Divider,
    MenuList,
    Button,
    Input
} from '@chakra-ui/react'   
import React from "react";
import Buttons from "components/Automation/Buttons"
import JsonToTable from "components/Automation/JsonToTable"



export default function Scenarios() {
    return <Box padding='16'>
        <Heading alignContent="center" >Profile : Wen Jun</Heading>
        <Input placeholder='Voice Control' display="inline-block" />

        <Box h='60px'>
            <Menu isLazy>
                <MenuButton as={Button} variant="solid" backgroundColor="gray.300" color="black"> Default</MenuButton>
                <MenuList>
                    {/* MenuItems are not rendered unless Menu is open */}
                    <MenuItem>Default</MenuItem>
                    <MenuItem>Romantic</MenuItem>
                    <MenuItem>Chinese New Year</MenuItem>
                    <MenuItem>Add More (+) </MenuItem>
                </MenuList>
            </Menu>
        </Box>
        <JsonToTable />
        <Box padding='3' display="flex">
            <Box width="50%" display="flex" justifyContent="flex-end">
                <Button ml={2} colorScheme="whatsapp">
                    Add Scenario
                </Button>
                <Button ml={2} colorScheme="whatsapp">
                    Add Rule
                </Button>
            </Box>
            <Box width="50%" display="flex" justifyContent="flex-start">
                <Button ml={2} colorScheme="whatsapp">
                    Export Scenario
                </Button>
                <Button ml={2} colorScheme="whatsapp">
                    Import Scenario
                </Button>
            </Box>
        </Box>
    </Box>;
}
