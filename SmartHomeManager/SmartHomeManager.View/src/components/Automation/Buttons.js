import { Button, Td } from "@chakra-ui/react";
import React from "react";
import { Link } from "react-router-dom";
import ModalButton from "./ModalButton"
function Buttons(props) {
	console.log(props);
	return (
		<Td>

			<Button ml={2} colorScheme="blue">
				<Link to={`/scenario/edit/${props.props.RuleID}`} state={props.props}>Edit</Link>
			</Button>
			<Button ml={2} colorScheme="red">
				<Link to={`/`}>Delete</Link>
			</Button>
		</Td>
	);
}

export default Buttons;
