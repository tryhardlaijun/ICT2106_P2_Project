import React, {useState} from "react";
import { Box, Text } from "@chakra-ui/react";
import NotificationBorder from "./NotificationBorder";

export default function NotificationPopup(){


    
    return(
        <>
            
            <NotificationBorder text1="Notification 1" text2="21 minutes ago"/>
            <NotificationBorder text1="Notification 2" text2="33 minutes ago"/>
            <NotificationBorder text1="Notification 3" text2="55 minutes ago"/>

        </>
    )
}