import React, { useState } from "react";
import {
	Box,
	Button,
	Heading,
	Flex,
	FormControl,
	FormLabel,
	Input,
	Select,
} from "@chakra-ui/react";

import { useToast } from "@chakra-ui/react";

const FormCard = () => {
	const name = "Scencerio Name";
	return (
		<>
			<Heading w="100%" textAlign={"center"} fontWeight="normal" mb="2%">
				{name}
			</Heading>
			<Input variant="unstyled" placeholder="ENTER NAME" size="lg" />
			<Flex mt="2%">
				<FormControl mr="5%">
					<FormLabel>Triggering Action</FormLabel>
					<Select placeholder="Select option">
						<option value="option1">Wave Hands</option>
						<option value="option2">Clap</option>
						<option value="option3">Say Hello</option>
					</Select>
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

export default function ActionRule() {
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
					<FormCard />
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
