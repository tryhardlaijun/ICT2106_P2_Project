import React from "react";
import {
	Text,
	Flex,
	FormControl,
	FormLabel,
	Input,
	Select,
} from "@chakra-ui/react";

function FormCard({ ruleInfo, updateForm }) {
    let originalDevice = ruleInfo.deviceId;
    let startTime = ruleInfo.startTime ? toSgTime(ruleInfo.startTime): "--:--:--"
    let endTime = ruleInfo.endTime ? toSgTime(ruleInfo.endTime): "--:--:--"
    function toSgTime(sqlTime) {
        console.log(sqlTime);
        if(!(sqlTime.includes('Z'))){
            sqlTime = sqlTime+'Z'
        }
        let date = new Date(sqlTime);
        date.setSeconds(0)
        let newDate = date.toLocaleTimeString('en-SG', { hour12: false }).slice(0,5);
        let [hours] = newDate.split(':')
        console.log(hours);
        if(hours == '24'){
            date.setHours(0)
            newDate ="00" + newDate.slice(2);
        }
        console.log(newDate);
        return newDate
      }

    function toSqlTime(sgTime) {
        console.log(sgTime);
        let date = new Date();
        let [hours, minutes] = sgTime.split(':');
        date.setHours(hours, minutes, 0,0);
        console.log(date.toISOString())
        return date.toISOString();
      }
	return (
		<>
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
                            console.log(e.target.value);
							updateForm({ startTime: toSqlTime(e.target.value) });
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
							updateForm({ endTime: toSqlTime(e.target.value) });
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
							{ruleInfo.deviceId}
						</option>
					</Select>
				</FormControl>
				<FormControl>
					<FormLabel>Action</FormLabel>
					<Select
                    value={ruleInfo.configurationValue}
						placeholder="Select option"
						onChange={(e) => {
							const selectedOption = e.target.selectedOptions[0];
							const selectedKey = selectedOption.innerText;
							const selectedValue = selectedOption.value;
							updateForm({
								configurationKey: selectedKey,
								configurationValue: selectedValue,
							});
                            console.log(selectedKey);
						}}
					>
						<option value={0}>Dim</option>
						<option value={1}>Turn On</option>
					</Select>
				</FormControl>
			</Flex>
		</>
	);
}

export default FormCard;
