import { Box, Container, Heading } from "@chakra-ui/react";
import React, { useState, useEffect } from "react";
import axios from 'axios';

export default function EnergyProfile() {
    // Hardcoded accountId 3fa85f64-5717-4562-b3fc-2c963f66afa6
    const [accountId, setAccountId] = useState("3fa85f64-5717-4562-b3fc-2c963f66afa6");
    const [energyProfile, setEnergyProfile] = useState(null);

    useEffect(() => {
        const getEnergyProfile = async () => {
            const response = await fetch(`https://localhost:7140/api/EnergyProfile/GetEnergyProfile?AccountId=${accountId}`);
            console.log(response)
            
            if (response.status == 200) {
              const data = await response.json();
              setEnergyProfile(data);
            } 
            
        };
        getEnergyProfile();
    }, []);

    function handleClick(event) {
        // This function handles the click event
        console.log('Box clicked:', event.target.innerText);
        // All hardcoded except Config Value 
        const newEnergyProfile = {
            //EnergyProfileId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            ConfigurationValue: parseInt(event.target.innerText),
            ConfigurationDesc: "string",
            AccountId: accountId,
            Account: {
                "accountId": accountId,
                "email": "string",
                "username": "string",
                "address": "string",
                "timezone": 0,
                "password": "string",
                "devicesOnboarded": 0
                
            }
        };
        setEnergyProfile(newEnergyProfile);
        if (energyProfile == null) {
            postData(newEnergyProfile)
        } else {
            putData(newEnergyProfile)
        }
        
    }

    const postData = async (energyProfile) => {
        try {
            const response = await axios.post('https://localhost:7140/api/EnergyProfile/PostEnergyProfile', energyProfile, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            return response.data;
        } catch (error) {
            console.error(error);
        }
    };

    const putData = async (energyProfile) => {
        try {
            const response = await axios.put('https://localhost:7140/api/EnergyProfile/PutEnergyProfile', energyProfile, {
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            return response.data;
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <Container mt="3%">
            <Heading>Energy Profile Manager</Heading>
            <Box maxW='sm' borderWidth='1px' borderRadius='lg' overflow='hidden' mt="3%"> 
                <Box p='6'>
                    <Box display='flex' alignItems='center' justifyContent='center'>
                        Choose Energy Profile
                    </Box>

                    <Box p='6' display='flex' alignItems='center' justifyContent='center'>
                        <Box as="button" p={[5, 5, 5, 5]} marginRight="10%" borderWidth='3px' w="25%" h="70px" onClick={handleClick}> 0 </Box>
                        <Box as="button" p={[5, 5, 5, 5]} borderWidth='3px' w="25%" h="70px" onClick={handleClick}> 1 </Box>
                        <Box as="button" p={[5, 5, 5, 5]} marginLeft="10%" borderWidth='3px' w="25%" h="70px" onClick={handleClick}> 2 </Box>
                    </Box>

                    <Box as='span' ml='2' color='gray.500' fontSize='xs' display='flex' alignItems='center' justifyContent='center'>
                        Selecting the above options might help you manage your home&apos;s energy use 
                    </Box>
                </Box>
            </Box>
        </Container>
        
    )
}