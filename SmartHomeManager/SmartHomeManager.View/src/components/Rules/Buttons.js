import { Button, Td } from "@chakra-ui/react";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";
function Buttons({props, deleteRule,editButton}) {
	const [isDeleted, setIsDeleted] = useState(false);
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
					deleteRule(props.ruleId).catch((error) => {
						console.error(error);
					});
				}}
			>
				Delete
			</Button>
		</Td>
	);
}

export default Buttons;
