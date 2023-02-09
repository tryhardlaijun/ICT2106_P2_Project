import {
  Container,
  Stack,
  Text,
  FormControl,
  FormLabel,
  Input,
  Select,
  Button,
  useDisclosure,
  Modal,
  ModalOverlay,
  ModalBody,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalFooter,
} from "@chakra-ui/react";
import React, { useEffect, useState } from "react";

export default function RegisterDevice() {
  const [deviceTypeNames, setDeviceTypeNames] = useState([]);

  const [newDeviceTypeName, setNewDeviceTypeName] = useState("");

  const [deviceName, setDeviceName] = useState("");
  const [deviceBrand, setDeviceBrand] = useState("Xiaomi");
  const [deviceModel, setDeviceModel] = useState("Mi Smart Standing Fan 2 Lite, White");
  const [deviceTypeName, setDeviceTypeName] = useState("");

  const { isOpen, onOpen, onClose } = useDisclosure();

  function fetchDeviceTypes() {
    fetch("https://localhost:7140/api/RegisterDevice/GetAllDeviceTypes/")
      .then((response) => response.json())
      .then((data) => setDeviceTypeNames(data));
  }

  useEffect(() => {
    fetchDeviceTypes();
  }, []);

  function handleAddNewDeviceType(e) {
    e.preventDefault();

    fetch("https://localhost:7140/api/RegisterDevice/AddDeviceType/", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ deviceTypeName: newDeviceTypeName }),
    }).then((data) => {
      setDeviceTypeNames([]);
      fetchDeviceTypes();
      onClose();
    });
  }

  function handleRegisterDevice(e) {
    e.preventDefault();

    fetch("https://localhost:7140/api/RegisterDevice/RegisterDevice/", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        deviceName: deviceName,
        deviceBrand: deviceBrand,
        deviceModel: deviceModel,
        deviceTypeName: deviceTypeName,
        accountId: "06419047-3d8e-47fa-a239-80b7f78c4a2e",
      }),
    }).then((data) => {
      setDeviceTypeNames([]);
      fetchDeviceTypes();
      onClose();
    });
  }

  return (
    <Container mt={5} mb={5} p={5} maxW="3xl" minH="50vh" border="1px" borderColor="gray.100" rounded="lg" boxShadow="lg" centerContent>
      <Text fontWeight="bold" fontSize="xl">
        Register Device
      </Text>

      <Stack spacing={5} minW="full">
        <FormControl isRequired>
          <FormLabel>Device Name</FormLabel>
          <Input onChange={(e) => setDeviceName(e.target.value)} type="text" placeholder="John's Smart Fan" />
        </FormControl>
        <FormControl isRequired>
          <FormLabel>Device Brand</FormLabel>
          <Input isDisabled onChange={(e) => setDeviceBrand(e.target.value)} type="text" placeholder="Xiaomi" value="Xiaomi" />
        </FormControl>
        <FormControl isRequired>
          <FormLabel>Device Model</FormLabel>
          <Input
            isDisabled
            onChange={(e) => setDeviceModel(e.target.value)}
            type="text"
            placeholder="Mi Smart Standing Fan 2 Lite, White"
            value="Mi Smart Standing Fan 2 Lite, White"
          />
        </FormControl>
        <FormControl isRequired>
          <FormLabel>Device Type</FormLabel>
          <Select onChange={(e) => setDeviceTypeName(e.target.value)} placeholder="Please a select a device type">
            {deviceTypeNames
              ? deviceTypeNames.map((item, i) => {
                  return (
                    <option key={i} value={item}>
                      {item}
                    </option>
                  );
                })
              : null}
          </Select>
        </FormControl>
        <Stack direction="row" spacing={5} justifyContent="flex-end">
          <Button colorScheme="blue" onClick={onOpen}>
            Add Device Type
          </Button>
          <Button colorScheme="green" onClick={handleRegisterDevice}>
            Register Device
          </Button>
        </Stack>
      </Stack>

      <Modal blockScrollOnMount={false} isOpen={isOpen} onClose={onClose}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Add Device Type</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <FormControl isRequired>
              <FormLabel>Device Type</FormLabel>
              <Input onChange={(e) => setNewDeviceTypeName(e.target.value)} type="text" placeholder="Smart Fan" />
            </FormControl>
          </ModalBody>
          <ModalFooter>
            <Button variant="ghost" mr={3} onClick={onClose}>
              Close
            </Button>
            <Button colorScheme="green" onClick={handleAddNewDeviceType}>
              Add Device Type
            </Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </Container>
  );
}
