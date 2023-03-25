import React, { useState } from "react";
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

import { useToast } from "@chakra-ui/react";
import { v4 as uuidv4 } from "uuid";
import axios from "axios";

const FormCard = ({ruleInfo, updateForm, updateOption}) => {
	// Should change to the scenario name
	const name = "Scencerio Name";
	let originalDevice = ruleInfo.deviceId
	return (
		<>
			<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
				{name}
			</Heading>
			<Input variant="unstyled" placeholder="ENTER NAME" size="lg" 
			value={ruleInfo.RuleName}
			onChange={(e) => {
				updateForm({ RuleName: e.target.value });
			}}/>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Triggering Action</FormLabel>
					<Select placeholder="Select option"
					onChange={(e)=>{
						updateForm({actionTrigger: e.target.value})
					}}>
						<option value="waveHands">Wave Hands</option>
						<option value="clap">Clap</option>
						<option value="sayHello">Say Hello</option>
					</Select>
				</FormControl>
			</Flex>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Device</FormLabel>
					<Select placeholder="Select option"
					onChange={(e)=>{
						updateForm({deviceId: e.target.value})
					}}>
						<option value={originalDevice}>{originalDevice}</option>
					</Select>
				</FormControl>				
			</Flex>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Action</FormLabel>
					<Select placeholder="Select option"
					onChange={(e)=>{
						updateForm({configurationKey: e.target.value})
					}}>
						<option value="speed">Speed</option>
						<option value="oscillation">Oscillation</option>
					</Select>
				</FormControl>
				<FormControl>
					<FormLabel>Value</FormLabel>
					<Select placeholder="Select option"
					onChange={(e)=>{
						updateForm({configurationValue: parseInt(e.target.value)})
					}}>	
						{updateOption(ruleInfo)}						
					</Select>
				</FormControl>
			</Flex>
		</>
	);
};

export default function ActionRule() {
	const [ruleDetail, setRuleDetail] = useState({
		ruleId: uuidv4(),
		scenarioId: "ac38af14-9a57-4df3-89f3-78f9ce9f4983",
		configurationKey: "",
		configurationValue: 0,
		actionTrigger: "",
		RuleName: "",
		startTime: null,
		endTime: null,
		deviceId: "33333333-3333-3333-3333-333333333333",
		apiKey: null,
		apiValue: null
	});

	function updateDetails(value) {
		return setRuleDetail((prev) => {
			return { ...prev, ...value };
		});
	}

	function renderOptions(ruleDetail){
		if(ruleDetail.configurationKey=="speed"){
			return(
				<>
				<option value={0}>1</option>
				<option value={1}>2</option>
				<option value={2}>3</option>
				<option value={3}>4</option>
				<option value={4}>5</option>
				</>
			);
		}else if(ruleDetail.configurationKey=="oscillation"){
			return(
				<>
				<option value={0}>Turn On</option>
				<option value={1}>Turn Off</option>
				</>
			);
		}
		return null;
	}

	async function createRule(e){
		const newRule = {...ruleDetail}
		const {data} = await axios.post('https://localhost:7140/api/Rules/CreateRule', newRule, {headers:{
			'Content-Type': 'application/json'
		}})
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
					<FormCard ruleInfo={ruleDetail} updateForm={updateDetails} updateOption={renderOptions}/>
					<Button
						mt="2%"
						w="7rem"
						colorScheme="blue"
						variant="solid"
						onClick={() => {
							// check the content of the rule before submit
							console.log(ruleDetail)
							// Need to submit a api request to create
							createRule().then(()=>{
								toast({
									title: "Rule created.",
									description:
										"Rule Successfully added to the DB",
									status: "success",
									duration: 3000,
									isClosable: true,
								});
							}).catch((e)=>{
								toast({
									title: "Rule created.",
									description:
										e.toString(),
									status: "error",
									duration: 3000,
									isClosable: true,
								});
							})						
						}}
					>
						Create
					</Button>
				</form>
			</Box>
		</>
	);
}
