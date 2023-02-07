import { Button, Td } from "@chakra-ui/react";
import React from "react";
import { Link } from "react-router-dom";

function Buttons(props) {
	return (
		<Td>
			<Button ml={2} colorScheme="green">
				<Link to={`/`}>Add</Link>
			</Button>
			<Button ml={2} colorScheme="teal">
				<Link to={`/`}>More</Link>
			</Button>
			<Button ml={2} colorScheme="blue">
				<Link to={{
                    pathname:`/scenario/edit/${props.props.RuleID}`,
                    state:{props}}}>Edit</Link>
			</Button>
			<Button ml={2} colorScheme="red">
				<Link to={`/`}>Delete</Link>
			</Button>
		</Td>
	);
}

export default Buttons;
