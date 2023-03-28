import { Box, Container, Heading, Button, useToast, VStack, Text, calc } from "@chakra-ui/react";
import React, { useState, useEffect } from "react";
import {
    Slider,
    SliderTrack,
    SliderFilledTrack,
    SliderThumb,
    SliderMark,
    Select,
} from '@chakra-ui/react'

import { ArrowRightIcon } from '@chakra-ui/icons'

export default function EnergyProfile() {
    const toast = useToast();
    // Hardcoded accountId 3fa85f64-5717-4562-b3fc-2c963f66afa6
    const [accountId, setAccountId] = useState("11111111-1111-1111-1111-111111111111");
    const [energyProfile, setEnergyProfile] = useState(null);
    const [energyProfileSelected, setEnergyProfileSelected] = useState(-1);
    var configValue = -1;
    let energyProfileSelectedValue = null
    const [newValue, setNewValue] = useState('EnergyProfile')
    const [energyProfiles, setEnergyProfiles] = useState([])
    const [dropdownValue, setDropdownValue] = useState("option_ac")

    useEffect(() => {
        const getEnergyProfile = async () => {
            const response = await fetch(`https://localhost:7140/api/EnergyProfile/GetEnergyProfile?AccountId=${accountId}`);
            console.log(response)

            if (response.status == 200) {
                const data = await response.json();
                setEnergyProfile(data);

                document.getElementById("previouslySelected").innerText = data.configurationValue;


                //set currently selected button to grey
                if (data != null) {
                    setProfileValue(parseInt(data.configurationValue))
                    setEnergyProfileSelected(parseInt(data.configurationValue))
                    energyProfileSelectedValue = parseInt(data.configurationValue)
                    updateSlider(sliderSetting)
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
        // All hardcoded except Config Value

        configValue = parseInt(document.getElementById("previouslySelected").innerText);
        console.log("HELLO: ", configValue);

        // set text of newlySelected
        document.getElementById("newlySelected").innerText = "Newly selected: " + event.target.innerText;

        console.log("WHY", configValue);

        // "highlight" box when clicked
        if (event.target.innerText == "0") {
            setEnergyProfileSelected(0)
            setProfileValue(0)
            energyProfileSelectedValue = 0
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
            setEnergyProfileSelected(1)
            energyProfileSelectedValue = 1
            setProfileValue(1)
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
            setEnergyProfileSelected(2)
            setProfileValue(2)
            energyProfileSelectedValue = 2
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
        handleSlider(sliderValue)


    }

    function submit() {
        console.log("value selected: ", energyProfileSelected);

        if (energyProfileSelected != -1) {
            console.log()
            putData("11111111-1111-1111-1111-111111111111", energyProfileSelected)

            //setEnergyProfile(newEnergyProfile);
            configValue = energyProfileSelected
            document.getElementById("previouslySelected").innerText = "" + energyProfileSelected;
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
                    "Chose " + energyProfileSelected + " as new energy profile.",
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


    const [reccoValue, setReccoValue] = useState(26)
    const [profileValue, setProfileValue] = useState(0)
    const labelStyles = {
        fontSize: "md",
        color: "gray.500",
        marginTop: "4px",
    };


    const demoSliderSettings = {
        "option_ac": {
            "default": 26,
            "min": 16,
            "max": 32,
            "step": 1,
            "factor": 1
        },
        "option_fan": {
            "default": 3,
            "min": 1,
            "max": 5,
            "step": 1,
            "factor": -1
        },
        "option_heater": {
            "default": 32,
            "min": 32,
            "max": 40,
            "step": 1,
            "factor": -1
        }
    }

    const [sliderValue, setSliderValue] = useState()
    const [sliderMin, setSliderMin] = useState()
    const [sliderMax, setSliderMax] = useState()
    const [sliderStep, setSliderStep] = useState()
    const [sliderSetting, setSliderSetting] = useState("option_ac")
    let sliderSettingValue = null

    const updateSlider = (value) => {
        setSliderSetting(value)
        sliderSettingValue = value
        setDropdownValue(value)
        let sliderOption = demoSliderSettings[value]
        setSliderMin(sliderOption["min"])
        setSliderMax(sliderOption["max"])
        setSliderStep(sliderOption["step"])
        setSliderValue(sliderOption["default"])
        handleSlider(sliderOption["default"])
    }
    const handleSlider = (val) => {
        setSliderValue(val);
        let setting = sliderSettingValue ?? sliderSetting
        let option = demoSliderSettings[setting]
        let factor = energyProfileSelectedValue ?? energyProfileSelected
        let calculation = val * factor * .05 * option["factor"] + val
        calculation = Math.floor(calculation)
        setReccoValue(calculation)
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
                        <Box alignItems="center" display='flex' justifyContent='center' id="newlySelected"></Box>


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
            <Box display='flex' alignItems='center' justifyContent='center'>
                <VStack>
                    <Box display='flex' alignItems='center' justifyContent='center'>

                        <Heading as="h2" size="md">Choose Energy Profile</Heading>
                        </Box>
                    <Box mt="3%" display='flex' alignItems='center' justifyContent='center'>
                            <Select placeholder='Select option' value={dropdownValue} onChange={(e) => updateSlider(e.target.value) }>
                                <option value='option_ac'>Air Conditioner</option>
                                <option value='option_fan'>Fan</option>
                                <option value='option_heater'>Heater</option>
                        </Select>
                    </Box>     
                </VStack>
                </Box>
            <Box paddingTop="10%" display="flex" alignItems="center" justifyContent="center">
                <Slider value={sliderValue} min={sliderMin} max={sliderMax} step={sliderStep} onChange={(value) => handleSlider(value)}>
                    <SliderTrack bg='blue.100'>
                        <SliderFilledTrack />
                    </SliderTrack>
                    <SliderMark value={sliderMin} {...labelStyles}>{sliderMin}</SliderMark>
                    <SliderMark value={sliderMax} {...labelStyles}>{sliderMax}</SliderMark>
                    <SliderMark
                        value={sliderValue}
                        textAlign='center'
                        bg='blue.500'
                        color='white'
                        mt='-10'
                        ml='-6'
                        w='12'
                    >
                        {sliderValue}
                    </SliderMark>
                    <SliderThumb boxSize={6} />
                </Slider>
            </Box>
            <Box paddingTop="5%" display='flex' alignItems='center' justifyContent='center'>
                Your recommended setting will change from 
            </Box>
            <Box paddingTop="5%" display='flex' alignItems='center' justifyContent='center' fontSize="xl">
                {sliderValue} <ArrowRightIcon ml={"5%"} mr={"5%"} /> {reccoValue}
            </Box>
        </Container >

    )
}