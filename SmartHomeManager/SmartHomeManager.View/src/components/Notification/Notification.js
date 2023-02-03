import React, { useState } from "react";
import {BellIcon} from '@chakra-ui/icons'
import NotificationPopup from "./NotificationPopup";
import TestNotification from "./TestNotification";
import {
    Popover,
    PopoverTrigger,
    PopoverContent,
    PopoverHeader,
    PopoverBody,
    PopoverFooter,
    PopoverArrow,
    PopoverCloseButton,
    PopoverAnchor,
    Flex,
    Box, 
    useDisclosure,
    Button
} from '@chakra-ui/react'


export default function Notification({open}) {

    const {isOpen, onClose, onToggle} = useDisclosure();

    return (
        <>

            {/* Test notification modal */}
            <Popover>
                <PopoverTrigger>
                    <Button mr="25px">
                        Test Notification
                    </Button>
                </PopoverTrigger>

                <PopoverContent mt={5}>
                    <PopoverHeader ml={2} fontWeight="bold" fontSize={25}>Notification Testing</PopoverHeader>
                    <PopoverBody>
                        <TestNotification/>
                    </PopoverBody>
                </PopoverContent>
            </Popover>
            
            {/* Notification bell modal */}
            <Popover>
                <PopoverTrigger>                  
                    <Button w={12} h={12} mr={4} onClick={isOpen ? onClose : onToggle}>
                        <BellIcon w={8} h={8}/>    
                    </Button> 
                </PopoverTrigger>
            
            <PopoverContent mt={5}>
                
                <PopoverHeader ml={2} fontWeight="bold" fontSize={25}>Welcome back xxxx</PopoverHeader>
                <PopoverBody>
                    <NotificationPopup/>
                </PopoverBody>
            </PopoverContent>
            </Popover>
            
        </>
        )
        
}