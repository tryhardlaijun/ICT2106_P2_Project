import { Button, Td } from "@chakra-ui/react";
import React from "react";
import { Link } from "react-router-dom";
import axios from "axios";
function Buttons(props) {
	console.log(props);
	async function deleteRule(ruleID){
		const {data} = await axios.delete(`http://localhost:5186/api/Rules/${ruleID}`,{headers:{
			'Content-Type': 'application/json'
		}, data:ruleID})
		console.log(data);
	}
	return (
		<Td>

			<Button ml={2} colorScheme="blue">
				<Link to={`/scenario/edit/${props.props.ruleId}`} state={props.props}>Edit</Link>
			</Button>
			<Button ml={2} colorScheme="red" onClick={()=>{deleteRule(props.props.ruleId).catch((error)=>{
				console.log(error);
			})}}>
				Delete
			</Button>
		</Td>
	);
}

export default Buttons;
