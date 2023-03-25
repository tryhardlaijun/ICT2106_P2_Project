import { MenuItem, MenuList } from "@chakra-ui/react";
import React from "react";

function MenuItems({scenarios, buttonUpdate}){
    
	const DisplayData = scenarios.map((scenario=>{
		return <MenuItem key={scenario.scenarioId} onClick={()=>buttonUpdate(scenario)}>{scenario.scenarioName}</MenuItem>
	}))
	return(
		<MenuList>
			{DisplayData}
		</MenuList>
	)
}

function RulesMenuItems({typeOfRules, buttonUpdate}){
	const DisplayData = typeOfRules.map((rule=>{
		return <MenuItem key={rule.id} onClick={()=>buttonUpdate(rule)}>{rule.name}</MenuItem>
	}))
	return(
		<MenuList>
			{DisplayData}
		</MenuList>
	)
}


// export default MenuItems;
export default{
	MenuItems,
	RulesMenuItems,
};