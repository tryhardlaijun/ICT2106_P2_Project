import React from "react";
import {
	Text,
	Flex,
	FormControl,
	FormLabel,
	Input,
	Heading,
	Select,
} from "@chakra-ui/react";

function FormCard({ ruleInfo, updateForm, updateOption, stateInfo }) {
    let originalDevice = ruleInfo.deviceId;
	const name = stateInfo?"Update Schedule":"Add New Schedule";
    let startTime = ruleInfo.startTime ? toSgTime(ruleInfo.startTime): "--:--:--"
    let endTime = ruleInfo.endTime ? toSgTime(ruleInfo.endTime): "--:--:--"
    function toSgTime(sqlTime) {
        if(!(sqlTime.includes('Z'))){
            sqlTime = sqlTime+'Z'
        }
        let date = new Date(sqlTime);
        date.setSeconds(0)
        let newDate = date.toLocaleTimeString('en-SG', { hour12: false }).slice(0,5);
        let [hours] = newDate.split(':')
        if(hours == '24'){
            date.setHours(0)
            newDate ="00" + newDate.slice(2);
        }
        return newDate
      }

	function returnDeviceName(deviceID){
		if(deviceID == "33333333-3333-3333-3333-333333333333"){
			return "FAN Room 1"
		}
	}

    function toSqlTime(sgTime, flag) {
        let date = new Date();
        let [hours, minutes] = sgTime.split(':');
        date.setHours(hours, minutes, 0,0);
		if(flag){
			let startTimeDate = new Date(ruleInfo.startTime)
			if(date < startTimeDate){
				date.setDate(date.getDate() + 1)
			}
		}
        return date.toISOString();
      }
	return (
		<>
		<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
			{name}
		</Heading>
			<Input
				variant="unstyled"
				textAlign="center"
				fontWeight="normal"
				fontSize="3xl"
				pb="1%"
				placeholder="ENTER NAME"
				size="lg"
				value={ruleInfo.ruleName}
				onChange={(e) => {
					updateForm({ ruleName: e.target.value });
				}}
			/>
			<Text fontSize="2xl">
				{"Scenario Name: " +
					localStorage.getItem("currentScenarioName")}
			</Text>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Start Time</FormLabel>
					<Input
						placeholder="Select Start Time"
						size="md"
						type="time"
						value={startTime}
						onChange={(e) => {
							updateForm({ startTime: toSqlTime(e.target.value, 0) });
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
							updateForm({ endTime: toSqlTime(e.target.value, 1) });
						}}
					/>
				</FormControl>
			</Flex>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Device</FormLabel>
					<Select
                    value={originalDevice}
						placeholder="Select option"
						onChange={(e) => {
							updateForm({ deviceId: e.target.value });
						}}
					>
						<option value={originalDevice}>
							{returnDeviceName(ruleInfo.deviceId)}
						</option>
					</Select>
				</FormControl>			
			</Flex>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Action</FormLabel>
					<Select placeholder="Select option" value={stateInfo?stateInfo.configurationKey:null}
					onChange={(e)=>{
						updateForm({configurationKey: e.target.value})
					}}>
						<option value="speed">Speed</option>
						<option value="oscillation">Oscillation</option>
					</Select>
				</FormControl>
				<FormControl>
					<FormLabel>Value</FormLabel>
					<Select placeholder="Select option" value={stateInfo?stateInfo.configurationValue:null}
					onChange={(e)=>{
						updateForm({configurationValue: parseInt(e.target.value)})
					}}>	
						{updateOption(ruleInfo)}						
					</Select>
				</FormControl>
			</Flex>
		</>
	);
}

export default FormCard;
