import React, { useState } from "react";
import {BellIcon} from '@chakra-ui/icons'
import NotificationPopup from "./NotificationPopup";
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

    const {isOpen, onClose, onToggle} = useDisclosure()

    return (
        <>
            
            <Popover>
                <PopoverTrigger>                  
                    <Button w={7} h={7} mr={4} onClick={isOpen ? onClose : onToggle}>
                        <BellIcon/>    
                    </Button> 
                </PopoverTrigger>
            
            <PopoverContent>
                
                <PopoverHeader fontWeight="bold">Welcome back xxxx</PopoverHeader>
                <PopoverBody>
                    <NotificationPopup/>
                </PopoverBody>
            </PopoverContent>
            </Popover>
            
        </>
        )
        
}