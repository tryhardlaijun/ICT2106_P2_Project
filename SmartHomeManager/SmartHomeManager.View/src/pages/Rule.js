import React, { useState } from "react";
import {
	Box,
	ButtonGroup,
	Button,
	Heading,
	Flex,
	FormControl,
	GridItem,
	FormLabel,
	Input,
	Select,
} from "@chakra-ui/react";

import { useToast } from "@chakra-ui/react";

const Form1 = () => {
	const name = "Scencerio Name";
	return (
		<>
			<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
				{name}
			</Heading>
			<Input variant="unstyled" placeholder="ENTER NAME" size="lg" />
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Start Time</FormLabel>
					<Input
						placeholder="Select Start Time"
						size="md"
						type="time"
					/>
				</FormControl>
				<FormControl>
					<FormLabel>Start Time</FormLabel>
					<Input
						placeholder="Select Start Time"
						size="md"
						type="time"
					/>
				</FormControl>
			</Flex>
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Device</FormLabel>
					<Select placeholder="Select option">
						<option value="option1">Device 1</option>
						<option value="option2">Device 2</option>
						<option value="option3">Device 3</option>
					</Select>
				</FormControl>
				<FormControl>
					<FormLabel>Action</FormLabel>
					<Select placeholder="Select option">
						<option value="option1">Action 1</option>
						<option value="option2">Action 2</option>
						<option value="option3">Action 3</option>
					</Select>
				</FormControl>
			</Flex>
		</>
	);
};

export default function Rule() {
	const toast = useToast();
	return (
		<>
			<Box
				borderWidth="1px"
				rounded="lg"
				shadow="1px 1px 3px rgba(0,0,0,0.3)"
				maxWidth={800}
				p={6}
				m="10px auto"
				as="form"
			>
				<form>
					<Form1 />

					<Button
						mt="2%"
						w="7rem"
						colorScheme="blue"
						variant="solid"
						onClick={() => {
							toast({
								title: "Rule created.",
								description:
									"Rule Successfully added to the DB",
								status: "success",
								duration: 3000,
								isClosable: true,
							});
						}}
					>
						Submit
					</Button>
				</form>
			</Box>
		</>
	);
}
