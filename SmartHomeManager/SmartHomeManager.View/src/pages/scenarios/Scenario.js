import {
	Heading,
	Box,
	Menu,
	MenuButton,
	Button,
	Input,
	useToast
} from "@chakra-ui/react";
import { Link } from "react-router-dom";
import React, { useEffect, useState } from "react";
import JsonToTable from "components/Rules/JsonToTable";
import ModalButton from "components/Rules/ModalButton";
import MenuItems from "components/Rules/MenuItems";
import axios from "axios";
import UploadModalButton from "components/Rules/UploadModal";

export default function Scenarios() {
	const [ allRules, setAllRules] = useState([])
	const [ allScenario, setAllScenario] = useState([])
	const [ currentScenario, setCurrentScenario ] = useState()
	const [buttonName, setButtonName] = useState("")
	const toast = useToast();

	/**
	 * @param {string} id
	 */
	async function getAllRules(id){
		const { data: ruleData } = await axios.get(`https://localhost:7140/api/Rules/rulesByScenarioId/${id}`)
		setAllRules(ruleData)
	}

	/**
	 * @param {string} scenarioName
	 * @param {string} scenarioId
	 */
	function updateLocalStore(scenarioName ,scenarioId){
		localStorage.setItem('currentScenarioName', scenarioName);
		localStorage.setItem('currentScenarioId', scenarioId);
	}

	/**
	 * Makes a toast notification.
	 * @param {string} title
	 * @param {string} message
	 * @param {import("@chakra-ui/react").UseToastOptions["status"]} status
	 * @param {number} timing
	 */
	function makeToast(title, message, status, timing){
		toast({
			title: title,
			description: message,
			status: status,
			duration: timing,
			isClosable: true,
		  });
	}
	/**
	 * Updates the current scenario to what the user has selected
	 * Uses Axios to get the rules for the selected scenario
	 * @param {object} scenario
	 */
	function scenarioSelect(scenario){
		setCurrentScenario(scenario)
		setButtonName(scenario.scenarioName)
		getAllRules(scenario.scenarioId)
		.catch((error)=>{
			console.error(error)
			makeToast('Error', 'Failed to fetch rules for the scenario. Please try again later.', 'error', 5000);
		})
		updateLocalStore(scenario.scenarioName, scenario.scenarioId)
	}

	/**
	 * Deletes the rule with the ruleID
	 * @param {string} ruleID
	 */
	async function deleteRule(ruleID) {
		try {
			await axios.delete(`https://localhost:7140/api/Rules/${ruleID}`, {
				headers: {
					"Content-Type": "application/json",
				},
				data: ruleID,
			});
			setAllRules(allRules.filter(rule => rule.ruleId !== ruleID));
		} catch (error) {
			console.error(error);
		}
	}

	useEffect(() => {
		const fetchData = async () => {
		  try {
			const { data: scenarioData } = await axios.get(`https://localhost:7140/api/Scenarios/GetAllScenarios`);
			setAllScenario(scenarioData);
			if (scenarioData.length > 0) {
			  const currentScenario = scenarioData[0];
			  setCurrentScenario(currentScenario);
			  setButtonName(currentScenario.scenarioName);
			  getAllRules(currentScenario.scenarioId)
			  updateLocalStore(currentScenario.scenarioName, currentScenario.scenarioId)
			}
		  } catch (error) {
			console.error(error);
			  makeToast('Error', 'Failed to fetch scenarios data. Please try again later.', 'error', 5000);
		  }
		};
		fetchData();
	  }, []);

	return (
		<Box padding="16">
			<Heading alignContent="center">Profile : Wen Jun</Heading>
			<Input placeholder="Voice Control" display="inline-block" />

			<Box h="60px">
				<Menu isLazy>
					<MenuButton
						margin = "2"
						as={Button}
						variant="solid"
						backgroundColor="gray.300"
						color="black"
					>
						{buttonName}
					</MenuButton>
					{/* MenuItems are not rendered unless Menu is open */}
					<MenuItems scenarios={allScenario} buttonUpdate={scenarioSelect}/>
				</Menu>
			</Box>
			<JsonToTable ruleData = {allRules} deleteRule = {deleteRule}/>
			<Box padding="3" display="flex">
				<Box width="50%" display="flex" justifyContent="flex-start">
					<Button ml={2} colorScheme="whatsapp">
						Add Scenario
					</Button>
					<Button ml={2} colorScheme="whatsapp">
						<Link to="/scenario/create/time-rule">Add Rule</Link>
					</Button>
				</Box>
				<Box width="50%" display="flex" justifyContent="flex-start">
					<Button ml={2} colorScheme="whatsapp">
						{currentScenario?(
							<Link to={`https://localhost:7140/api/Rules/DownloadRules?ScenarioId=${currentScenario.scenarioId}`}>Export Rules</Link>
						): "Export Rules"}
					</Button>
					<UploadModalButton title={"test"} text={"Import Rules"} action={getAllRules}/>
					<ModalButton
						title="Simulate Clash"
						text="This rule will clash with another rule to turn on device at 1500."
						action="override"
					/>
					<ModalButton
						title="Simulate Troubleshooting"
						text="You fan seems to be unable to oscilate, please check if something is blocking it."
						action="Try again"
					/>
				</Box>
			</Box>
		</Box>
	);
}
