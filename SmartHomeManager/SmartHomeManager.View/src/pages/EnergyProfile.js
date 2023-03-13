import { Box, Container, Heading, Button, useToast } from "@chakra-ui/react";
import React, { useState, useEffect } from "react";
import axios from 'axios';

export default function EnergyProfile() {
    const toast = useToast();
    // Hardcoded accountId 3fa85f64-5717-4562-b3fc-2c963f66afa6
    const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");
    const [energyProfile, setEnergyProfile] = useState(null);
    var valueSelected = "";
    var configValue = -1;
    const [newValue, setNewValue] = useState('EnergyProfile')
    const [energyProfiles, setEnergyProfiles] = useState([])

    useEffect(() => {
        const getEnergyProfile = async () => {
            const response = await fetch(`https://localhost:7140/api/EnergyProfile/GetEnergyProfile?AccountId=${accountId}`);
            console.log(response)
            
            if (response.status == 200) {
              const data = await response.json();
              setEnergyProfile(data);

                console.log(data.configurationValue);
                document.getElementById("previouslySelected").innerText = data.configurationValue;
                

                //set currently selected button to grey
                if (data != null) {
                    if (data.configurationValue == "0") {
                        document.getElementById("button0").style.backgroundColor = "#E2E8F0";
                    }
                    else if (data.configurationValue == "1") {
                        document.getElementById("button1").style.backgroundColor = "#E2E8F0";
                    }
                    else if (data.configurationValue == "2") {
                        document.getElementById("button2").style.backgroundColor = "#E2E8F0";
                    }
                }
            } 
            
        };
        getEnergyProfile();
    }, []);

    

    function handleClick(event) {
        // This function handles the click event
        console.log('Box clicked:', event.target.innerText);
        // All hardcoded except Config Value

        configValue = parseInt(document.getElementById("previouslySelected").innerText);
        console.log("HELLO: ", configValue);

        //set text of newlySelected
        document.getElementById("newlySelected").innerText = "Newly selected: " + event.target.innerText;

        console.log("WHY", configValue);
            
        //"highlight" box when clicked
        if (event.target.innerText == "0") {
            valueSelected = "0";
            document.getElementById("button0").style.backgroundColor = "yellow";
            if (configValue == 0) {
                    document.getElementById("button1").style.backgroundColor = "white";
                    document.getElementById("button2").style.backgroundColor = "white";
            }
            else if (configValue == 1) {
                document.getElementById("button1").style.backgroundColor = "#E2E8F0";
                document.getElementById("button2").style.backgroundColor = "white";
            }
            else if (configValue == 2) {
                document.getElementById("button2").style.backgroundColor = "#E2E8F0";
                document.getElementById("button1").style.backgroundColor = "white";
            }
            
        }
        else if (event.target.innerText == "1") {
            valueSelected = "1";
            document.getElementById("button1").style.backgroundColor = "yellow";
            if (configValue == 0) {
                document.getElementById("button0").style.backgroundColor = "#E2E8F0";
                document.getElementById("button2").style.backgroundColor = "white";
            }
            else if (configValue == 1) {
                document.getElementById("button0").style.backgroundColor = "white";
                document.getElementById("button2").style.backgroundColor = "white";
            }
            else if (configValue == 2) {
                document.getElementById("button0").style.backgroundColor = "white";
                document.getElementById("button2").style.backgroundColor = "#E2E8F0";
            }
            
        }
        else if (event.target.innerText == "2") {
            valueSelected = "2";
            document.getElementById("button2").style.backgroundColor = "yellow";
            if (configValue == 0) {
                document.getElementById("button0").style.backgroundColor = "#E2E8F0";
                document.getElementById("button1").style.backgroundColor = "white";
            }
            else if (configValue == 1) {
                document.getElementById("button0").style.backgroundColor = "white";
                document.getElementById("button1").style.backgroundColor = "#E2E8F0";
            }
            else if (configValue == 2) {
                document.getElementById("button0").style.backgroundColor = "white";
                document.getElementById("button1").style.backgroundColor = "white";
            }
            
        }
        
        
        
    }

    function submit() {
        console.log("value selected: ", valueSelected);

        if (valueSelected != "") {
            console.log()
            putData("11111111-1111-1111-1111-111111111111", valueSelected)

            //setEnergyProfile(newEnergyProfile);
            configValue = parseInt(valueSelected);
            document.getElementById("previouslySelected").innerText = valueSelected;
            console.log("new configValue: ", configValue);

            if (configValue == 0) {
                document.getElementById("button0").style.backgroundColor = "#E2E8F0";
                document.getElementById("button1").style.backgroundColor = "white";
                document.getElementById("button2").style.backgroundColor = "white";
            }
            else if (configValue == 1) {
                document.getElementById("button0").style.backgroundColor = "white";
                document.getElementById("button1").style.backgroundColor = "#E2E8F0";
                document.getElementById("button2").style.backgroundColor = "white";
            }
            else if (configValue == 2) {
                document.getElementById("button0").style.backgroundColor = "white";
                document.getElementById("button1").style.backgroundColor = "white";
                document.getElementById("button2").style.backgroundColor = "#E2E8F0";
            }

            toast({
                title: "Saved energy profile successfully",
                description:
                    "Chose " + valueSelected + " as new energy profile.",
                status: "success",
                duration: 3000,
                isClosable: true,
            });
            document.getElementById("newlySelected").innerText = "";
        }
        else {
            toast({
                title: "Failed to save energy profile",
                description:
                    "Please select an energy profile and try again.",
                status: "error",
                duration: 3000,
                isClosable: true,
            });
            document.getElementById("newlySelected").innerText = "";
        }
        };
        

    const putData = async (id, newValue) => {
        try {
            const response = await fetch(`https://localhost:7140/api/EnergyProfile/PutEnergyProfile/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ configValue: newValue })
            })
            if (response.ok) {
                const updatedEnergyProfile = energyProfiles.map((energyprofile) => {
                    if (energyprofile.energyprofileId === id) {
                        return { ...energyprofile, configValue: newValue }
                    }
                    return energyprofile
                })
                setEnergyProfile(updatedEnergyProfile)
            } else {
                console.error(response.statusText)
            }
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

                    <Box display='flex' alignItems='center' justifyContent='center'>
                        Previously selected:&nbsp;<span id="previouslySelected"></span>
                    </Box>

                    <Box p='6' display='flex' alignItems='center' justifyContent='center'>
                        <Box as="button" p={[5, 5, 5, 5]} marginRight="10%" borderWidth='3px' w="25%" h="70px" id="button0" onClick={handleClick}> 0 </Box>
                        <Box as="button" p={[5, 5, 5, 5]} borderWidth='3px' w="25%" h="70px" id="button1" onClick={handleClick}> 1 </Box>
                        <Box as="button" p={[5, 5, 5, 5]} marginLeft="10%" borderWidth='3px' id="button2" w="25%" h="70px" onClick={handleClick}> 2 </Box>
                    </Box>

                    <Box as='span' ml='2' color='gray.500' fontSize='xs' display='flex' alignItems='center' justifyContent='center'>
                        Selecting the above options might help you manage your home&apos;s energy use
                    </Box>
                    
                    <Box alignItems="center" display='flex' justifyContent='center' id="newlySelected"></Box>
                    <Box alignItems="center" display='flex' justifyContent='center'>
                        <Button onClick={submit}>Confirm changes to energy profile</Button>
                    </Box>
                </Box>
            </Box>
        </Container>
        
    )
}