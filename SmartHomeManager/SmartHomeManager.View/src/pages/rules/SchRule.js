import React, { useEffect, useState } from "react";
import { Box, Button } from "@chakra-ui/react";
import { useNavigate, useLocation } from "react-router-dom";
import { useToast } from "@chakra-ui/react";
import { v4 as uuidv4 } from "uuid";
import axios from "axios";
import FormCard from "components/Rules/FormCard";
import OverwriteRuleDialogue from "pages/rules/OverwriteRuleDialogue";

export default function SchRule() {
	const [showRuleOption, setShowRuleOption] = useState(false);
	const [clashedRule, setClashedRule] = useState("");
	const location = useLocation();
	const navigate = useNavigate();
	const toast = useToast();
	const [newFlag, setNewFlag] = useState(false);
	const [ruleDetail, setRuleDetail] = useState({
		ruleId: uuidv4(),
		scenarioId: localStorage.getItem("currentScenarioId"),
		configurationKey: "",
		configurationValue: "",
		actionTrigger: "",
		ruleName: "",
		startTime: "",
		endTime: "",
		deviceId: "33333333-3333-3333-3333-333333333333",
		apiKey: "",
		apiValue: "",
	});

	function updateDetails(value) {
		return setRuleDetail((prev) => {
			return { ...prev, ...value };
		});
	}

	async function createRule() {
		const newRule = { ...ruleDetail };
		const url = newFlag
			? "http://localhost:7140/api/Rules/CreateRule"
			: "http://localhost:7140/api/Rules/EditRule";
		const method = newFlag ? axios.post : axios.put;
		const { data } = await method(url, newRule, {
			headers: {
				"Content-Type": "application/json",
			},
		});
	}
	async function checkIfClash(ruleReq) {
		const response = await axios.post("http://localhost:7140/api/Rules/CheckIfClash", ruleReq, {
			headers: {
				"Content-Type": "application/json",
			},
		});
		return response;
	}

	async function OverwriteRules(ruleReq) {
		const response = await axios.post("http://localhost:7140/api/Rules/OverWrite", ruleReq, {
			headers: {
				"Content-Type": "application/json",
			},
		})

		return response;
	}

	function renderOptions(ruleDetail) {
		if (ruleDetail.configurationKey == "speed") {
			return (
				<>
					<option value={0}>1</option>
					<option value={1}>2</option>
					<option value={2}>3</option>
					<option value={3}>4</option>
					<option value={4}>5</option>
				</>
			);
		} else if (ruleDetail.configurationKey == "oscillation") {
			return (
				<>
					<option value={0}>Turn On</option>
					<option value={1}>Turn Off</option>
				</>
			);
		}
		return null;
	}

	/**
 * Makes a toast notification.
 * @param {string} title
 * @param {string} message
 * @param {import("@chakra-ui/react").UseToastOptions["status"]} status
 * @param {number} timing
 */
	function makeToast(title, message, status, timing) {
		toast({
			title: title,
			description: message,
			status: status,
			duration: timing,
			isClosable: true,
		});
	}
	async function handleCreateAndEditRule() {
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
				title: "Error Creating Rule.",
				description: "Something went wrong",
				status: "error",
				duration: 3000,
				isClosable: true,
			});
		}
	}
	const handleSubmit = async () => {
		let returnCode = ""
		let clashedTitle = ""
		console.log(ruleDetail);
		await checkIfClash(ruleDetail).then(() => {
		}).catch((err) => {
			console.error(err)
			returnCode = err.response.status
			clashedTitle = (err.response.data.ruleName)
		})
		if (returnCode == "400") {
			makeToast("Error Invalid Rule.", "Something went wrong.", "error", 3000);
		}
		else if (returnCode == "409") {
			setShowRuleOption(true);
			setClashedRule(clashedTitle)
		}
		else {
			handleCreateAndEditRule()
		}
	};
	const handleOverwrite = async () => {
		try {
			const success = await OverwriteRules(ruleDetail);
			handleCreateAndEditRule()
		} catch (error) {
			toast({
				title: "Error Overwrite Rule.",
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
						updateOption={renderOptions}
						stateInfo={location.state}
					/>
					<Button
						mt="2%"
						w="7rem"
						colorScheme="blue"
						variant="solid"
						onClick={handleSubmit}
					>
						{location.state ? "Update" : "Create"}
					</Button>
				</form>
			</Box>
			{showRuleOption && (
				<OverwriteRuleDialogue
					Close={() => {
						setShowRuleOption(false);
					}}
					OverwriteCallBack={() => {
						handleOverwrite()
					}}
					Title={clashedRule}
				/>
			)}
		</>
	);
}