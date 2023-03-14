import React, { useEffect, useState } from "react";
import { Box, Button } from "@chakra-ui/react";
import { useNavigate , useLocation } from "react-router-dom";
import { useToast } from "@chakra-ui/react";
import { v4 as uuidv4 } from "uuid";
import axios from "axios";
import FormCard from "components/Rules/FormCard";

export default function SchRule() {
	const location = useLocation();
	const navigate = useNavigate();
	const toast = useToast();
	const [newFlag, setNewFlag] = useState(false);
	const [ruleDetail, setRuleDetail] = useState({
		ruleId: uuidv4(),
		scenarioId: localStorage.getItem("currentScenarioId"),
		configurationKey: "",
		configurationValue: 1,
		actionTrigger: "",
		ruleName: "",
		startTime: "",
		endTime: "",
		deviceId: "33333333-3333-3333-3333-333333333333",
		apiKey: "",
		apiValue: "",
	});
	
	function updateDetails(value) {
		console.log(value);
		return setRuleDetail((prev) => {
			return { ...prev, ...value };
		});
	}

	async function createRule() {
		const newRule = { ...ruleDetail };
		const url = newFlag
			? "https://localhost:7140/api/Rules/CreateRule"
			: "https://localhost:7140/api/Rules/EditRule";
		const method = newFlag ? axios.post : axios.put;
		const { data } = await method(url, newRule, {
			headers: {
				"Content-Type": "application/json",
			},
		});
	}

	const handleSubmit = async () => {
		try {
			const success = await createRule();
			toast({
				title: "Rule created.",
				description: "Rule Successfully added to the DB",
				status: "success",
				duration: 3000,
				isClosable: true,
			});
			navigate("/Scenario");
		} catch (error) {
			toast({
				title: "Rule created.",
				description: "Something went wrong",
				status: "error",
				duration: 3000,
				isClosable: true,
			});
		}
	};

	useEffect(() => {
		if (location.state) {
			let ruleinfo = location.state;
			console.log(ruleinfo);
			setRuleDetail(ruleinfo);
		} else {
			setNewFlag(true);
		}
	}, [location.state]);

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
						onClick={handleSubmit}
					>
						Submit
					</Button>
				</form>
			</Box>
		</>
	);
}
