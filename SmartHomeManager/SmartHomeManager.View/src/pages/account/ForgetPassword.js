import { React, useState } from 'react';
import {
  Button,
  FormControl,
  FormErrorMessage,
  Flex,
  Heading,
  Input,
  Stack,
  Text,
  useColorModeValue,
} from '@chakra-ui/react';


export default function ForgotPassword() {

//Email input + validation
const [emailInput, updateEmailInput] = useState("")
const [emailValid, updateEmailValid] = useState(true)

//Function to verify email
const checkEmailInput = (emailInput) => {
  if (emailInput.length == 0) {
    updateEmailValid(true)
  } else {
    var mailFormat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    updateEmailValid(mailFormat.test(emailInput))
  }
}

  return (
    <Flex
      minH={'100vh'}
      align={'center'}
      justify={'center'}
      bg={useColorModeValue('gray.50', 'gray.800')}>
      <Stack
        spacing={4}
        w={'full'}
        maxW={'md'}
        bg={useColorModeValue('white', 'gray.700')}
        rounded={'xl'}
        boxShadow={'lg'}
        p={6}
        my={12}>
        <Heading lineHeight={1.1} fontSize={{ base: '2xl', md: '3xl' }}>
          Forgot your password?
        </Heading>
        <Text
          fontSize={{ base: 'sm', sm: 'md' }}
          color={useColorModeValue('gray.800', 'gray.400')}>
          You&apos;ll get an email with a reset link
        </Text>
        <FormControl id="email" isInvalid={!emailValid}>
          <Input
            placeholder="your-email@example.com"
            _placeholder={{ color: 'gray.500' }}
            type="email"
            onChange={(e) => { updateEmailInput(e.target.value); checkEmailInput(e.target.value) }} />
          {
            (emailValid && emailInput.length > 0) ? "" : (<FormErrorMessage>Email is invalid!</FormErrorMessage>)
          }
        </FormControl>
        <Stack spacing={6}>
          <Button
            bg={'blue.400'}
            color={'white'}
            _hover={{
              bg: 'blue.500',
            }}>
            Request Reset
          </Button>
        </Stack>
      </Stack>
    </Flex>
  );
}
