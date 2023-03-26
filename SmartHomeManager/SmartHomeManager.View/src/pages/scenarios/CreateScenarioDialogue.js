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


import { redirect, useLocation } from "react-router-dom";
import { useToast } from "@chakra-ui/react";
import { v4 as uuidv4 } from "uuid";
import axios from "axios";

const FormCard = ({scenarioInfo, updateForm}) => {
	const name = "Create New Scenario";
   //let scenarioName = scenarioInfo.scenarioName;
	return (
		<>
			<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
				{name}
			</Heading>
            <FormControl>
					<FormLabel>Enter scenario name:</FormLabel>
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

export default function CreateScenarioDialogue() {
    const location = useLocation();
	const [newFlag, setNewFlag] = useState(false)
    const [scenarioDetail, setScenarioDetail] = useState({
        ScenarioId: uuidv4(),
        ScenarioName: "",
        ProfileId: "22222222-2222-2222-2222-222222222222",
        isActive: false,
    });



    function updateDetails(value) {
		return setScenarioDetail((prev) => {
			return { ...prev, ...value };
		});
	}

    async function createScenario(e){
		const newScenario = {...scenarioDetail}
		const {data} = await axios.post('http://localhost:7140/api/Scenarios/CreateScenario', newScenario, {headers:{
			'Content-Type': 'application/json'
		}})
	}
	const toast = useToast();

   /* function renderOptions(scenarioDetail){
		if(scenarioDetail.configurationKey=="speed"){
			return(
				<>
				<option value={0}>1</option>
				<option value={1}>2</option>
				<option value={2}>3</option>
				<option value={3}>4</option>
				<option value={4}>5</option>
				</>
			);
		}else if(scenarioDetail.configurationKey=="oscillation"){
			return(
				<>
				<option value={0}>Turn On</option>
				<option value={1}>Turn Off</option>
				</>
			);
		}
		return null;
	}
    */

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
								createScenario()
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
						Add Scenario
					</Button>
				</form>
			</Box>
		</>
	);
    
}


