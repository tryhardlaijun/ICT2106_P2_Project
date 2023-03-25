import { Button, Td } from "@chakra-ui/react";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import ConfirmationScreen from "../../pages/rules/ConfirmationScreen";

function Buttons({props, deleteRule,editButton}) {
	const [isDeleted, setIsDeleted] = useState(false);
	const [showConfirmation, setShowConfirmation] = useState(false);
	console.log(props);
	return (
		<Td>
			<Button ml={2} colorScheme="blue">
				{editButton({props})}
			</Button>
			<Button
				ml={2}
				colorScheme="red"
				onClick={() => {
					setShowConfirmation(true);
				}}
			>
				Delete
			</Button>
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
