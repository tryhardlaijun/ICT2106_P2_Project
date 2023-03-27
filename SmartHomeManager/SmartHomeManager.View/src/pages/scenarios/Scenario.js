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
import { AddIcon,DeleteIcon,EditIcon,ChevronDownIcon } from '@chakra-ui/icons'
import { Badge,Divider } from '@chakra-ui/react'
import { v4 as uuidv4 } from "uuid";

export default function Scenarios() {
	const [ allRules, setAllRules] = useState([])
	const [ allScenario, setAllScenario] = useState([])
	const [ allTypes, setTypes] = useState([{id:"1",name:"Schedule"},{id:"2",name:"Event"},{id:"3",name:"API"}])
	const [ currentScenario, setCurrentScenario ] = useState()
	const [buttonName, setButtonName] = useState("")
	const [typesOfRuleButton, setTypesOfRuleButton] = useState("Schedule")
	let [searchParams, setSearchParams] = useSearchParams();
	const [showRuleOption, setShowRuleOption] = useState(false);
	const [inputValue, setInputValue] = useState("");

	const toast = useToast();
	const deviceTypeFilter = "Fan";
	const configurationKeyFilter = "Unable to oscillate";
	const [scenarioDetail, setScenarioDetail] = useState({
		ScenarioId: uuidv4(),
		ScenarioName: "",
		ProfileId: "22222222-2222-2222-2222-222222222222",
		isActive: false,
	});

	/**
	 * @param {string} id
	 */
	async function getAllRules(id){
		const { data: ruleData } = await axios.get(`https://localhost:7140/api/Rules/schedulesByScenarioId/${id}`)
		setAllRules(ruleData)
	}

	/**
	 * @param {string} currentScenario
	 */
	async function getRulesBasedOnTypes(currentScenario, type){
		switch(type){
			case "Schedule":
				const { data: scheduleData } = await axios.get(`https://localhost:7140/api/Rules/schedulesByScenarioId/${currentScenario.scenarioId}`)
				setAllRules(scheduleData)
				break;
			case "Event":
				const { data: eventData } = await axios.get(`https://localhost:7140/api/Rules/eventsByScenarioId/${currentScenario.scenarioId}`)
				setAllRules(eventData)
				break;
			case "API":
				const { data: apiData } = await axios.get(`https://localhost:7140/api/Rules/apisByScenarioId/${currentScenario.scenarioId}`)
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
	
	function handleVoiceInput(value) {
		return setScenarioDetail((prev) => {
			return { ...prev, ...value };
		});
		
	}
	// function updateDetails(value) {
	// 	return setRuleDetail((prev) => {
	// 		return { ...prev, ...value };
	// 	});
	// }
	
	async function handleVoiceSubmit() {
		const newScenario = {...scenarioDetail}
		console.log(newScenario.ScenarioName)
		try {
			await axios.post('https://localhost:7140/api/Scenarios/VoiceInput',
				newScenario.ScenarioName,
				{ headers:{
					'Content-Type': 'application/json' }})
		} catch (error) {
			console.error(error);
			makeToast('Error', 'Failed to process voice input. Please try again.',error,5000);
		}
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

	/**
	 * Deletes the scenario with the scenarioID
	 * @param {string} scenarioID
	 */
    async function deleteScenario(scenarioID){
		try {
			await axios.delete(`https://localhost:7140/api/Scenarios/${scenarioID}`, {
				headers: {
					"Content-Type": "application/json",
				},
				data: scenarioID,
			});
			setAllScenario(allScenario.filter(scenario => scenario.ScenarioId !== scenarioID));
			setCurrentScenario(currentScenario);
		} catch (error) {
			console.error(error);
		}
	}

	useEffect(() => {
		const fetchData = async () => {
		  try {
			const { data: scenarioData } = await axios.get(`https://localhost:7140/api/Scenarios/GetAllScenarios`);
			  setAllScenario(scenarioData);
			  console.log(scenarioData.length)
			if (scenarioData.length > 0) {
				const currentScenario = scenarioData[scenarioData.length - 1];
				console.log(scenarioData[scenarioData.length - 1])
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
			<Box width="100%" display="flex" justifyContent="flex-start">	
			<Box width="100%" display="flex" justifyContent="flex-start">			
					<Heading size={"2xl"}>Profile : Wen Jun</Heading>
					
				</Box>		
				<Box width="100%" display="flex" justifyContent="flex-end">			
					<Button ml={2} paddingRight={5}>
						{currentScenario?(
							<Link to={`https://localhost:7140/api/Rules/DownloadRules?ScenarioId=${currentScenario.scenarioId}`}>Export</Link>
						): "Export Rules"}
					</Button>
					<UploadModalButton title={"test"} text={"Import"} action={getAllRules} />
						<ModalButton
						 	color="red"
							title="Simulate Clash"
							text="This rule will clash with another rule to turn on device at 1500."
							action="override"
						/>
				</Box>						
			</Box>
			<Badge>Profile_Id: {scenarioDetail.ProfileId}</Badge>
			
			
			
			
		
			{/*<Input placeholder="Voice Control" display="inline-block" />*/}
			<Box h="10px"></Box>
			<Divider orientation='horizontal' />

			<Box width="50%" display="flex" justifyContent="flex-start">
				{/* This will be the list of scenarios */}
				<Box h="60px">
					<Menu isLazy>
						<MenuButton
							rightIcon={<ChevronDownIcon/>}
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
				<Box h="100px"></Box>
				
				<Box h="60px">
				
				<Button ml={2}  margin={2} colorScheme="blue">					
					<Link to="/scenario/edit/edit-dialogue-scenario">Edit Scenario<EditIcon paddingLeft={1}></EditIcon>	</Link>	
				</Button>
				</Box>			
				<Box h="60px">
				<Button ml={2}  margin={2} colorScheme="red" onClick={() => {
					deleteScenario(localStorage.getItem("currentScenarioId")).catch((error) => {
						console.error(error);
					});
				}}>
					
				Delete Scenario
				<DeleteIcon paddingLeft={1}></DeleteIcon>
				</Button>
				</Box>
				{/* </Box> */}
			</Box>
			<Box margin={2}>
			{/* <Box h="60px" > */}
					<Menu isLazy>
						<MenuButton
							// margin = "2"
							// marginLeft={2}
							rightIcon={<ChevronDownIcon/>}
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
					
			<JsonToTable ruleData = {allRules} deleteRule = {deleteRule} editButton={renderUpdateButton}/>
			<Box h="60px">
				<Button ml={2} margin={2} colorScheme="green" justifyContent='flex-start'>
				
					<Link to="/scenario/create/create-dialogue-scenario">Add Scenario<AddIcon paddingLeft={1}></AddIcon></Link>
				</Button>	
				<Button ml={2} colorScheme="green"
						onClick={() => {
							setShowRuleOption(true);
						}}>		
						Add Rule	
						<AddIcon paddingLeft={1}></AddIcon>			
					</Button>					
				</Box>		
				
			</Box>
			<Box h="20px"></Box>
			<Divider orientation='horizontal' />

			<Box h="20px"></Box>
			<Box display="flex">
				<Input placeholder="Voice Control" display="inline-block" onChange={(e)=>{handleVoiceInput({ScenarioName: e.target.value})}} value={scenarioDetail.ScenarioName} />
				<Button onClick={handleVoiceSubmit}>
					Submit
				</Button>
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
