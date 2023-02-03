import React, { useState } from "react";
import {BellIcon} from '@chakra-ui/icons'
import { Box } from "@chakra-ui/react";


export default function Notification() {
    return (
        <>
            <div>
                
                <BellIcon w={7} h={7} mr={4}/>
                {/* <Box>
                    <div>
                        heading
                    </div>
                </Box> */}
                
            </div>
        </>
        )
        
}