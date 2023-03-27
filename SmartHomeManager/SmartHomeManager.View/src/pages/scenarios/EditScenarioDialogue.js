import React, { useEffect, useState } from "react";
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


import { redirect, useLocation,useNavigate } from "react-router-dom";
import { useToast } from "@chakra-ui/react";
import { v4 as uuidv4 } from "uuid";
import axios from "axios";

//getCurrentScenario({scenarios})
const FormCard = ({scenarioInfo, updateForm}) => {
	const name = "Edit Scenario";
	return (
		<>
			<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
				{name}
			</Heading>
            <FormControl>
					<FormLabel>Edit scenario name:</FormLabel>
					<Input
						placeholder="Scenario Name"
						type="string"
						value={scenarioInfo.ScenarioName}
						onChange={(e) => {
							updateForm({ ScenarioName: e.target.value });
						}}
					/>
				</FormControl>		
		</>
	);
};


export default function EditScenarioDialogue() {
	const location = useLocation();
	const navigate = useNavigate();
    const [scenarioDetail, setScenarioDetail] = useState({
        ScenarioId:  localStorage.getItem("currentScenarioId"),
        ScenarioName: "",
        ProfileId: "22222222-2222-2222-2222-222222222222",
        isActive: false
    });


    function updateDetails(value) {
		return setScenarioDetail((prev) => {
			return { ...prev, ...value };
		});
	}

    async function EditScenario(e){
		const EditScenario = {...scenarioDetail}
		const {data} = await axios.put('https://localhost:7140/api/Scenarios/EditScenario', EditScenario, {headers:{
			'Content-Type': 'application/json'
		}})
		navigate("/Scenario");
	}
	const toast = useToast();

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
                <FormCard
						scenarioInfo={scenarioDetail}
						updateForm={updateDetails}
					/>
					<Button
						mt="2%"
						w="7rem"
						colorScheme="blue"
						variant="solid"
						onClick={() => {
							// check the content of the rule before submit
							console.log(scenarioDetail)
							// Need to submit a api request to create
							EditScenario()
								.then(()=>{
									toast({
										title: "Scenario created.",
										description:
											"Scenario Successfully added to the DB",
										status: "success",
										duration: 3000,
										isClosable: true,
									});
								})
								.catch((e)=>{
									toast({
										title: "Scenario created.",
										description:
											e.toString(),
										status: "error",
										duration: 3000,
										isClosable: true,
									});
								})
							
											
						}}
					>
						Edit Scenario
					</Button>
				</form>
			</Box>
		</>
	);
    
}


