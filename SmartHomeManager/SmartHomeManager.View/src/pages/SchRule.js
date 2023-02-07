import React, { useEffect, useState } from "react";
import {Box, Button, Heading, Flex, FormControl, FormLabel, Input, Select} from "@chakra-ui/react";
import { useLocation } from "react-router-dom";
import { useToast } from "@chakra-ui/react";

function FormCard({ruleInfo, updateForm}) {
	const name = "Scencerio Name";
	console.log(ruleInfo);
	console.log(ruleInfo.RuleName)
	return (
		<>
			<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
				{ruleInfo.ScenarioName}
			</Heading>
			<Input variant="unstyled" placeholder="ENTER NAME" size="lg" value={ruleInfo.RuleName} onChange={(e)=>{updateForm({RuleName: e.target.value})}}/>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Start Time</FormLabel>
					<Input
						placeholder="Select Start Time"
						size="md"
						type="time"
						value={ruleInfo.StartTime}
						onChange={(e)=>{updateForm({StartTime: e.target.value})}}
					/>
				</FormControl>
				<FormControl>
					<FormLabel>End Time</FormLabel>
					<Input
						placeholder="Select Start Time"
						size="md"
						type="time"
						value={ruleInfo.EndTime}
						onChange={(e)=>{updateForm({EndTime: e.target.value})}}
					/>
				</FormControl>
			</Flex>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Device</FormLabel>
					<Select placeholder="Select option">
						<option value="option1">Wen Jun&apos;s Light</option>
						<option value="option2">Wen Jun&apos;s Fan</option>
						<option value="option3">Wen Jun&apos;s Aircon</option>
					</Select>
				</FormControl>
				<FormControl>
					<FormLabel>Action</FormLabel>
					<Select placeholder="Select option">
						<option value="option1">Dim</option>
						<option value="option2">Turn On</option>
					</Select>
				</FormControl>
			</Flex>
		</>
	);
};

export default function SchRule() {
	const location = useLocation();
	const [ruleDetail, setRuleDetail] = useState({});
	useEffect(() => {
		if (location.state != null) {
			let ruleinfo = location.state;
			console.log(location.state)
			setRuleDetail(ruleinfo)
		}
	}, [location.state]);
	const toast = useToast();
	function updateDetails(value) {
		return setRuleDetail((prev) => {
		  return { ...prev, ...value };
		});
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
					<FormCard ruleInfo = {ruleDetail} updateForm={updateDetails}/>
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
						}}
					>
						Submit
					</Button>
				</form>
			</Box>
		</>
	);
}
