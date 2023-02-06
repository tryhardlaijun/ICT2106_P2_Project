import React, {useState} from "react";
import { Box, Text } from "@chakra-ui/react";
import NotificationBorder from "./NotificationBorder";
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
    useDisclosure,
    Button
} from '@chakra-ui/react'

export default function TestNotificationModal(){


    
    return(
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
        </>
    )
}