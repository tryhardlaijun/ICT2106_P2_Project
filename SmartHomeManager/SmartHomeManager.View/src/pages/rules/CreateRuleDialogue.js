import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';

import {
	Box,
	Button,
	Heading,
	Flex,
	FormControl,
	FormLabel,
	Input,
	Select,
} from "@chakra-ui/react";

const FormCard = ({option, updateSelect}) => {
	const name = "Choose one to create";
	return (
		<>
			<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
				{name}
			</Heading>
			<Input variant="unstyled" placeholder="ENTER NAME" size="lg" />
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Triggering Action</FormLabel>
					<Select placeholder="Select option"
                        onChange={(event) => updateSelect(event.target.value)}
                    > 
                    {option.map( opt => (
                        <option key={opt.key} value={opt.value}>
                            {opt.name}
                        </option>
                    ))}
					</Select>
				</FormControl>
                
			</Flex>
		</>
	);
};

const SubmitButton = ({handleClicks}) => {
	return (
		<Button
            mt="2%"
            w="7rem"
            colorScheme="blue"
            variant="solid"
            onClick={() => {							
                handleClicks()
            }}
        >
            Create
        </Button>
	);
};


export default function CreateRuleDialogue() {
    const [selectedOption, setSelectedOption] = useState("");
    const navigate = useNavigate();

    const [options, setOption] = useState([
        {key:"1",value:"options1",name:"Schedule",url:"/scenario/create/schedule-rule"},
        {key:"2",value:"options2",name:"Event",url:"/scenario/create/action-rule"},
        {key:"3",value:"options3",name:"API",url:"/scenario/create/api-rule"},
        {key:"4",value:"options4",name:"Test",url:"/scenario/create/api-rule"}])

    const handleButtonClick = () => {
        console.log("Test")
        const option = options.find((opt) => opt.value === selectedOption);
        console.log(option)
        if (option) {
            navigate(option.url);
        }
    };
	return (
		<>
			<Box
				borderWidth="1px"
				rounded="lg"
				shadow="1px 1px 3px rgba(0,0,0,0.3)"
				maxWidth={800}
				p={6}
				m="10px auto"
				as="form"
			>
				<form>
					<FormCard updateSelect={setSelectedOption} option={options} />
                    <SubmitButton handleClicks={handleButtonClick}/>				
				</form>
			</Box>
		</>
	);
}
