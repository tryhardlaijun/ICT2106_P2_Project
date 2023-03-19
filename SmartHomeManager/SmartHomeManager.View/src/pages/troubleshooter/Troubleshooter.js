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
import { useLocation } from "react-router-dom";


export default function Troubleshooter() {
	const [allTroubleshooters, setAllTroubleshooters] = useState([])
	const location = useLocation();
	const params = new URLSearchParams(location.search);
	const [deviceTypeFilter, setDeviceTypeFilter] = useState(params.get("deviceType") || "");
	const [configurationKeyFilter, setConfigurationKeyFilter] = useState(params.get("configMsg") || "");
	const [filteredData, setFilteredData] = useState([]);

	const toast = useToast();
	let timeoutId; // keep track of timeout id

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

	const debouncedApplyFilters = () => {
		clearTimeout(timeoutId);
		timeoutId = setTimeout(() => {
			applyFilters();
		}, 500); // delay execution of applyFilters for 500ms after user stops typing
	}

	const applyFilters = () => {
		let filtered = allTroubleshooters;
		if (deviceTypeFilter && configurationKeyFilter) {
			filtered = filtered.filter(
				(item) =>
					item.deviceType.toLowerCase().includes(deviceTypeFilter.toLowerCase()) &&
					item.configurationKey.toLowerCase().includes(configurationKeyFilter.toLowerCase())
			);
		} else if (deviceTypeFilter) {
			filtered = filtered.filter(
				(item) =>
					item.deviceType.toLowerCase().includes(deviceTypeFilter.toLowerCase())
			);
		} else if (configurationKeyFilter) {
			filtered = filtered.filter(
				(item) =>
					item.configurationKey.toLowerCase().includes(configurationKeyFilter.toLowerCase())
			);
		}

		setFilteredData(filtered);

		if (filtered.length === 0) {
			toast({
				title: "No results found",
				status: "warning",
				duration: 5000,
				isClosable: true,
			});
		} else {
			toast({
				title: "Filters applied",
				status: "success",
				duration: 5000,
				isClosable: true,
			});
		}
	};



	const clearFilters = () => {
		setDeviceTypeFilter("");
		setConfigurationKeyFilter("");
	};

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
