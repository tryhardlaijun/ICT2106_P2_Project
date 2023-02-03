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