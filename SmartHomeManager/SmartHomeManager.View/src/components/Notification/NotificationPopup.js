import React, { useState, useEffect } from "react";
import { Box, Text, Spinner, Flex } from "@chakra-ui/react";
import NotificationBorder from "./NotificationBorder";

export default function NotificationPopup({ notifications }){

    const SPINNER_TIMEOUT = 2000;
    const [isDataLoaded, setIsDataLoaded] = useState(false);

    // On Component Load
    useEffect(() => {
        // Let spinner load for 1000ms
        setTimeout(() => setIsDataLoaded(true), SPINNER_TIMEOUT);
    }, [])

    function getMinutesNotificationTime(notificationSentTime) {
        let sentTime = new Date(notificationSentTime);
        let currentTime = new Date();
        var difference = currentTime.getTime() - sentTime.getTime(); // This will give difference in milliseconds
        return Math.round(difference / 60000);
    }


    return(
        <>
            <Flex flexDirection="column">
            {
                notifications && isDataLoaded ? 
                notifications.reverse().map((noti, i) => {
                    return (
                        <NotificationBorder 
                            key={i}
                            message={noti.notificationMessage} 
                            sentTime={
                                getMinutesNotificationTime(noti.sentTime)
                            }
                        />   
                    )
                }) :
                <Spinner alignSelf="center" my="4" />
            }
            </Flex>
        </>
    )
}