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
import ModalButton from "components/Rules/ModalButton";
import MenuItems from "components/Rules/MenuItems";
import axios from "axios";
import UploadModalButton from "components/Rules/UploadModal";
import TroublershooterToTable from "components/Rules/TroublershooterToTable";

export default function Troubleshooter() {
	const [allTroubleshooters, setAllTroubleshooters] = useState([])
	const [filteredData, setFilteredData] = useState([]);
	const [deviceTypeFilter, setDeviceTypeFilter] = useState("");
	const [configurationKeyFilter, setConfigurationKeyFilter] = useState("");

	const toast = useToast();

	async function getAllTroubleshooters() {
		try {
			const { data: troubleshootersData } = await axios.get(`https://localhost:7140/api/Troubleshooter/GetAllTroubleshooter`)
			setAllTroubleshooters(troubleshootersData)
			setFilteredData(troubleshootersData); // initialize filteredData with allTroubleshooters data
		} catch (error) {
			toast({
				title: "An error occurred.",
				description: error.message,
				status: "error",
				duration: 5000,
				isClosable: true,
			});
		}
	}

	useEffect(() => {
		getAllTroubleshooters();
	}, []);

	// Filter data based on deviceTypeFilter and configurationKeyFilter
	function applyFilters() {
		const filtered = allTroubleshooters.filter(
			(info) =>
				info.deviceType.toLowerCase().includes(deviceTypeFilter.toLowerCase()) &&
				info.configurationKey.toLowerCase().includes(configurationKeyFilter.toLowerCase())
		);
		setFilteredData(filtered);
	}


	// Reset filters and display all data
	function clearFilters() {
		setDeviceTypeFilter("");
		setConfigurationKeyFilter("");
		setFilteredData(allTroubleshooters);
	}
	

	return (
		<Box padding="16">
			<Heading alignContent="center">Profile : Wen Jun</Heading>
			<Input
				placeholder="Device Type"
				value={deviceTypeFilter}
				onChange={(e) => setDeviceTypeFilter(e.target.value)}
				display="inline-block"
			/>
			<Input
				placeholder="Configuration Key"
				value={configurationKeyFilter}
				onChange={(e) => setConfigurationKeyFilter(e.target.value)}
				display="inline-block"
			/>
			<Button onClick={applyFilters}>Filter</Button>
			<Button onClick={clearFilters}>Clear Filters</Button>

			<TroublershooterToTable troublershootersData={filteredData} />
		</Box>
	);
}
