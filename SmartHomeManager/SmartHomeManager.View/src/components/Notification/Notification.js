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
    useDisclosure
} from '@chakra-ui/react'


export default function Notification({open}) {

    const {isOpen, onClose, onToggle} = useDisclosure()

    return (
        <>
            
            <Popover>
                <PopoverTrigger>                  
                    <BellIcon w={7} h={7} mr={4} onClick={onToggle}/>
                </PopoverTrigger>
            
            <PopoverContent>
                
                <PopoverHeader>Welcome back xxxx</PopoverHeader>
                <PopoverBody>
                    <NotificationPopup/>
                </PopoverBody>
            </PopoverContent>
            </Popover>
            
        </>
        )
        
}