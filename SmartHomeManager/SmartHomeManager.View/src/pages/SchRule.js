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
import { useLocation } from "react-router-dom";
import { useToast } from "@chakra-ui/react";
import { v4 as uuidv4 } from "uuid";
import axios from "axios";

function FormCard({ ruleInfo, updateForm }) {
	const name = "Scencerio Name";
	let startTime = ruleInfo.startTime==="" ? "00:00:00":new Date(ruleInfo.startTime).toLocaleTimeString("en-SG",{hour12: false});
	let endTime = ruleInfo.endTime==="" ? "00:00:00":new Date(ruleInfo.endTime).toLocaleTimeString("en-SG",{hour12: false});
	let originalDevice = ruleInfo.deviceId
	function makeDate(time){
		let validDate = new Date();
		let timeArray = time.split(":")
		validDate.setHours(timeArray[0], timeArray[1])
		return validDate.toISOString()
	}
	return (
		<>
			<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
				{ruleInfo.scenarioId}
			</Heading>
			<Input
				variant="unstyled"
				placeholder="ENTER NAME"
				size="lg"
				value={ruleInfo.RuleName}
				onChange={(e) => {
					updateForm({ RuleName: e.target.value });
				}}
			/>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Start Time</FormLabel>
					<Input
						placeholder="Select Start Time"
						size="md"
						type="time"
						value={startTime}
						onChange={(e) => {
							updateForm({ startTime: makeDate(e.target.value)});
						}}
					/>
				</FormControl>
				<FormControl>
					<FormLabel>End Time</FormLabel>
					<Input
						placeholder="Select Start Time"
						size="md"
						type="time"
						value={endTime}
						onChange={(e) => {
							updateForm({ endTime:  makeDate(e.target.value)});
						}}
					/>
				</FormControl>
			</Flex>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Device</FormLabel>
					<Select placeholder="Select option"
					onChange={(e)=>{
						updateForm({ deviceId: e.target.value });
					}}>
						<option value={originalDevice}>{ruleInfo.deviceId}</option>
					</Select>
				</FormControl>
				<FormControl>
					<FormLabel>Action</FormLabel>
					<Select placeholder="Select option"
					onChange={(e)=>{
						updateForm({configurationValue: e.target.value})
					}}>
						<option value={0}>Dim</option>
						<option value={1}>Turn On</option>
					</Select>
				</FormControl>
			</Flex>
		</>
	);
}

export default function SchRule() {
	const location = useLocation();
	const [newFlag, setNewFlag] = useState(false)
	const [ruleDetail, setRuleDetail] = useState({
		ruleId: uuidv4(),
		scenarioId: "AC38AF14-9A57-4DF3-89F3-78F9CE9F4983",
		configurationKey: uuidv4(),
		configurationValue: 0,
		actionTrigger: "string",
		RuleName: "string1",
		startTime: "",
		endTime: "",
		deviceId: "33333333-3333-3333-3333-333333333333",
		apiKey: "",
		apiValue: "",
	});
	useEffect(() => {
		if (location.state != null) {
			let ruleinfo = location.state;
			setRuleDetail(ruleinfo);
		} else{
			setNewFlag(true)
		}
	}, [location.state]);
	const toast = useToast();
	function updateDetails(value) {
		return setRuleDetail((prev) => {
			return { ...prev, ...value };
		});
	}
	async function createRule(e){
		const newRule = {...ruleDetail}
		const {data} = await axios.post('http://localhost:5186/api/Rules/CreateRule', newRule, {headers:{
			'Content-Type': 'application/json'
		}})
	}
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
						ruleInfo={ruleDetail}
						updateForm={updateDetails}
					/>
					<Button
						mt="2%"
						w="7rem"
						colorScheme="blue"
						variant="solid"
						onClick={() => {
							toast({
								title: "Rule created.",
								description:
									"Rule Successfully added to the DB",
								status: "success",
								duration: 3000,
								isClosable: true,
							});
							if(newFlag){
								createRule()
							}
						}}
					>
						Submit
					</Button>
				</form>
			</Box>
		</>
	);
}
