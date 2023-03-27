import { Button, Td } from "@chakra-ui/react";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import ModalButton from "components/Rules/ModalButton";
import { AddIcon,DeleteIcon,EditIcon,WarningTwoIcon } from '@chakra-ui/icons'
import axios from "axios";

import ConfirmationScreen from "../../pages/rules/ConfirmationScreen";

function Buttons({props, deleteRule,editButton, deviceName}) {
	const [isDeleted, setIsDeleted] = useState(false);
	const [showConfirmation, setShowConfirmation] = useState(false);
	console.log(props);
	return (
		<Td>
			<Button ml={2} colorScheme="blue">
				{editButton({props})} <EditIcon marginLeft={1}></EditIcon>
			</Button>
			<Button
				ml={2}
				colorScheme={"red"}
				onClick={() => {
					setShowConfirmation(true);
				}}
			>
				Delete
				<DeleteIcon marginLeft={1} ></DeleteIcon>
			</Button>
			<ModalButton
						title="Troubleshoot"
						text= {"Device " + deviceName + "'s "+ props.configurationKey.toLowerCase() +" is not operating!"}
						action="Try again"
						deviceType= {deviceName}
						configMsg= {props.configurationKey.toLowerCase()}
						redirectTo={{
							pathname: "/troubleshooters",
							// state: { deviceName, deviceName},
						}}						
					/>
			{showConfirmation && (
				<ConfirmationScreen
					onConfirm={() => {
						deleteRule(props.ruleId).catch((error) => {
							console.error(error);
							});
						setShowConfirmation(false);
					}}
					Close={()=>{
						setShowConfirmation(false);
					}}
				/>
			)}
		</Td>
	);
}

export default Buttons;
