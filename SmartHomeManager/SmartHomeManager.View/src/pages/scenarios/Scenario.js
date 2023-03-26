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
import { useSearchParams } from "react-router-dom";
import JsonToTable from "components/Rules/JsonToTable";
import ModalButton from "components/Rules/ModalButton";
import MenuItems from "components/Rules/MenuItems";
import axios from "axios";
import UploadModalButton from "components/Rules/UploadModal";
import CreateRuleDialogue from "pages/rules/CreateRuleDialogue";

export default function Scenarios() {
	const [ allRules, setAllRules] = useState([])
	const [ allScenario, setAllScenario] = useState([])
	const [ allTypes, setTypes] = useState([{id:"1",name:"Schedule"},{id:"2",name:"Event"},{id:"3",name:"API"}])
	const [ currentScenario, setCurrentScenario ] = useState()
	const [buttonName, setButtonName] = useState("")
	const [typesOfRuleButton, setTypesOfRuleButton] = useState("Schedule")
	let [searchParams, setSearchParams] = useSearchParams();
	const [showRuleOption, setShowRuleOption] = useState(false);
	
	const toast = useToast();
	const deviceTypeFilter = "Fan";
	const configurationKeyFilter = "Unable to oscillate";

	/**
	 * @param {string} id
	 */
	async function getAllRules(id){
		const { data: ruleData } = await axios.get(`http://localhost:7140/api/Rules/schedulesByScenarioId/${id}`)
		setAllRules(ruleData)
	}

	/**
	 * @param {string} currentScenario
	 */
	async function getRulesBasedOnTypes(currentScenario, type){
		switch(type){
			case "Schedule":
				const { data: scheduleData } = await axios.get(`http://localhost:7140/api/Rules/schedulesByScenarioId/${currentScenario.scenarioId}`)
				setAllRules(scheduleData)
				break;
			case "Event":
				const { data: eventData } = await axios.get(`http://localhost:7140/api/Rules/eventsByScenarioId/${currentScenario.scenarioId}`)
				setAllRules(eventData)
				break;
			case "API":
				const { data: apiData } = await axios.get(`http://localhost:7140/api/Rules/apisByScenarioId/${currentScenario.scenarioId}`)
				setAllRules(apiData)
				break;
		}
	}

	function renderUpdateButton({props}){
		if(typesOfRuleButton == "Schedule"){
			return(
				<>
				<Link
					to={`/schedule/edit/${props.ruleId}`}
					state={props}
				>
					Edit
				</Link>
				</>
			);
		}else if(typesOfRuleButton == "Event"){
			return(
				<>
				<Link
					to={`/event/edit/${props.ruleId}`}
					state={props}
				>
					Edit
				</Link>
				</>
			);
		}else if(typesOfRuleButton == "API"){
			return(
				<>
				<Link
					to={`/api/edit/${props.ruleId}`}
					state={props}
				>
					Edit
				</Link>
				</>
			);
		}
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

	function ruleTypeSelect(type){
		setTypesOfRuleButton(type.name)
		getRulesBasedOnTypes(currentScenario , type.name)
	}

	/**
	 * Deletes the rule with the ruleID
	 * @param {string} ruleID
	 */
	async function deleteRule(ruleID) {
		try {
			await axios.delete(`http://localhost:7140/api/Rules/${ruleID}`, {
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

	/**
	 * Deletes the scenario with the scenarioID
	 * @param {string} scenarioID
	 */
    async function deleteScenario(scenarioID){
		try {
			await axios.delete(`http://localhost:7140/api/Scenarios/${scenarioID}`, {
				headers: {
					"Content-Type": "application/json",
				},
				data: scenarioID,
			});
			setAllScenario(allScenario.filter(scenario => scenario.ScenarioId !== scenarioID));
			window.location.reload();
			setCurrentScenario(currentScenario);
		} catch (error) {
			console.error(error);
		}
	}

	useEffect(() => {
		const fetchData = async () => {
		  try {
			const { data: scenarioData } = await axios.get(`http://localhost:7140/api/Scenarios/GetAllScenarios`);
			setAllScenario(scenarioData);
			if (scenarioData.length > 0) {
			  const currentScenario = scenarioData[scenarioData.length-1];
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

			<Box width="50%" display="flex" justifyContent="flex-start">
				{/* This will be the list of scenarios */}
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
						<MenuItems.MenuItems scenarios={allScenario} buttonUpdate={scenarioSelect}/>
					</Menu>
				</Box>
				{/* This will be the 3 types of rules*/}
				<Box h="60px">
					<Menu isLazy>
						<MenuButton
							margin = "2"
							as={Button}
							variant="solid"
							backgroundColor="gray.300"
							color="black"
						>
							{typesOfRuleButton}
						</MenuButton>
						{/* MenuItems are not rendered unless Menu is open */}
						<MenuItems.RulesMenuItems typeOfRules={allTypes} buttonUpdate={ruleTypeSelect}/>
					</Menu>
				</Box>
			</Box>
			<JsonToTable ruleData = {allRules} deleteRule = {deleteRule} editButton={renderUpdateButton}/>
			<Box padding="3" display="flex">
				<Box width="50%" display="flex" justifyContent="flex-start">
					<Button ml={2} colorScheme="whatsapp"
						onClick={() => {
							setShowRuleOption(true);
						}}>		
						Add Rule				
					</Button>
					<Button ml={2} colorScheme="whatsapp">
					<Link to="/scenario/create/create-dialogue-scenario">Add Scenario</Link>
					</Button>
					<Button ml={2} colorScheme="blue">
					<Link to="/scenario/edit/edit-dialogue-scenario">Edit Scenario</Link>
					</Button>
					<Button ml={2} colorScheme="red" onClick={() => {
					deleteScenario(localStorage.getItem("currentScenarioId")).catch((error) => {
						console.error(error);
					});
				}}>
				Delete Scenario
			</Button>
				</Box>
				
				<Box width="50%" display="flex" justifyContent="flex-start">
					<Button ml={2} colorScheme="whatsapp">
						{currentScenario?(
							<Link to={`http://localhost:7140/api/Rules/DownloadRules?ScenarioId=${currentScenario.scenarioId}`}>Export Rules</Link>
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
						text="Device fan seems to be unable to oscillate"
						action="Try again"
						redirectTo={{
							pathname: "/troubleshooters",
							state: { deviceTypeFilter, configurationKeyFilter },
						}}
						deviceType="Fan"
						configMsg="Unable to oscillate"
					/>
				</Box>
			</Box>
			{showRuleOption && (
				<CreateRuleDialogue				
					Close={()=>{
						setShowRuleOption(false);
					}}
					/>
				)}
		</Box>
		
	);
}
