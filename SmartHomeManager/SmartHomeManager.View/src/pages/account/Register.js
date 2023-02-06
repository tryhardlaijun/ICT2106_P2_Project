import { React, useState } from 'react';
import {
    Flex,
    Box,
    FormControl,
    FormLabel,
    Input,
    InputGroup,
    InputRightElement,
    Stack,
    Button,
    Heading,
    Text,
    useColorModeValue,
    Link,
    FormErrorMessage,
    FormHelperText,
    NumberInput,
    NumberInputField,
    NumberInputStepper,
    Slider,
    SliderTrack,
    SliderFilledTrack,
    SliderThumb
} from '@chakra-ui/react';
import { ViewIcon, ViewOffIcon } from '@chakra-ui/icons';
import bcrypt from 'bcryptjs'

export default function Register() {
    //Input declaration
    const [emailInput, updateEmailInput] = useState("")
    const [usernameInput, updateUsernameInput] = useState("")
    const [passwordInput, updatePasswordInput] = useState("")
    const [confirmPasswordInput, updateConfirmPasswordInput] = useState("")
    const [timezoneInput, updateTimezoneInput] = useState('0')
    const [addressInput, updateAddressInput] = useState("")

    //Boolean declaration for validation
    const [emailValid, updateEmailValid] = useState(true)
    const [passwordMessage, updatePasswordMessage] = useState("")
    const [passwordValid, updatePasswordValid] = useState(true)
    const [confirmPasswordMessage, updateConfirmPasswordMessage] = useState("")
    const [confirmPasswordValid, updateConfirmPasswordValid] = useState(true)

    //Show password
    const [showPassword, setShowPassword] = useState(false);
    const [showCfmPassword, setShowCfmPassword] = useState(false);

    //Use for number slider timezone
    const handleChange = (timezoneInput) => updateTimezoneInput(timezoneInput)
    const format = (val) => "GMT " + val
    const parse = (val) => val.replace(/^\GMT /, "")

    //Function to verify email
    const checkEmailInput = (emailInput) => {
        if (emailInput.length == 0) {
            updateEmailValid(true)
        } else {
            var mailFormat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            updateEmailValid(mailFormat.test(emailInput))
        }
    }

    //Function to verify password
    const verifyPasswordInput = () => {
        
        if (passwordInput.length == 0) {
            updatePasswordValid(true)
        }
        if (confirmPasswordInput.length == 0) {
            updateConfirmPasswordValid(true)
        }
        //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
        var passwordFormat = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        if (passwordInput.length!=0){
            if (passwordInput.length < 8) {
                updatePasswordMessage("Password should have a minimum of 8 characters")
                updatePasswordValid(false)
            } else if (passwordFormat.test(passwordInput)) {
                updatePasswordMessage("Password is looking good")
                updatePasswordValid(true)
            } else {
                updatePasswordMessage("Password should have a minimum of 8 characters, at least 1 uppercase letter, 1 lowercase letter, 1 number and 1 special character")
                updatePasswordValid(false)
            }
        }

        //Check if password match
        if (confirmPasswordInput.length!=0){
            if (confirmPasswordInput.length < 8) {
                updateConfirmPasswordMessage("Password should have a minimum of 8 characters")
                updateConfirmPasswordValid(false)
            } else if (passwordInput == confirmPasswordInput && passwordFormat.test(confirmPasswordInput)) {
                updateConfirmPasswordMessage("Password matched")
                updateConfirmPasswordValid(true)
            } else {
                updateConfirmPasswordMessage("Password does not match or meet the required condition")
                updateConfirmPasswordValid(false)
            }
        }
    }

    //Submit form
    const submitRegisterForm = () => {
        if (passwordInput != confirmPasswordInput) {
            updateConfirmPasswordMessage("Password does not match or meet the required condition")
            updateConfirmPasswordValid(false)
        }else{
        const salt = bcrypt.genSaltSync(10)
        const hashPassword = bcrypt.hash(passwordInput, salt)
        /*
        bcrypt.compare(passwordInput, hashKey, function (err, result) {
            console.log({ passwordInput }, result)
        });
        console.log({ emailInput })
        console.log({ usernameInput })
        console.log({ timezoneInput })
        console.log({ passwordInput })
        console.log({ confirmPasswordInput })
        console.log({ addressInput })
        */
        
        const obj = {
            "email": emailInput, "username": usernameInput, "address": addressInput, "timezone": timezoneInput, "password": passwordInput
        }
        const message = JSON.stringify(obj)
        //emailInput, usernameInput, addressInput, timezoneInput, passwordInput, hashKey
        fetch('https://localhost:7140/api/Accounts', {
            method: 'POST',
            body: message,
        headers: {
                'Content-type': 'application/problem+json; charset=utf-8',
            },
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
            })
            .catch((err) => {
                console.log(err.message);
            });
        }
            
    }


    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6} minW={'lg'}>
                <Stack align={'center'}>
                    <Heading fontSize={'4xl'} textAlign={'center'}>
                        Sign up
                    </Heading>
                    <Text fontSize={'lg'} color={'gray.600'}>
                        to enjoy all of our cool features ✌️
                    </Text>
                </Stack>
                <Box
                    rounded={'lg'}
                    bg={useColorModeValue('white', 'gray.700')}
                    boxShadow={'lg'}
                    p={8}>
                    <Stack spacing={4}>
                        <FormControl id="email" isInvalid={!emailValid} isRequired>
                            <FormLabel>Email address</FormLabel>
                            <Input type="email" onChange={(e) => { updateEmailInput(e.target.value); checkEmailInput(e.target.value) }} />
                            {
                                (emailValid && emailInput.length > 0) ? (<FormHelperText>Email is valid!</FormHelperText>) : (<FormErrorMessage>Email is invalid!</FormErrorMessage>)
                            }
                        </FormControl>
                        <FormControl id="username" isRequired>
                            <FormLabel>Username</FormLabel>
                            <Input type="text" onChange={(e) => updateUsernameInput(e.target.value)} />
                        </FormControl>
                        <FormControl id="password" isRequired isInvalid={!passwordValid}>
                            <FormLabel>Password</FormLabel>
                            <InputGroup>
                                <Input type={showPassword ? 'text' : 'password'} minLength="8" value={passwordInput} onChange={(e) => updatePasswordInput(e.target.value)} onBlur={(e) => verifyPasswordInput()} />
                                <InputRightElement h={'full'}>
                                    <Button
                                        variant={'ghost'}
                                        onClick={() =>
                                            setShowPassword((showPassword) => !showPassword)
                                        }>
                                        {showPassword ? <ViewIcon /> : <ViewOffIcon />}
                                    </Button>
                                </InputRightElement>
                            </InputGroup>
                            {
                                    passwordValid ? (<FormHelperText>{passwordMessage}</FormHelperText>) : (<FormErrorMessage>{passwordMessage}</FormErrorMessage>)
                                }
                        </FormControl>
                        <FormControl id="cfmpassword" isRequired isInvalid={!confirmPasswordValid}>
                            <FormLabel>Confirm password</FormLabel>
                            <InputGroup>
                                <Input type={showCfmPassword ? 'text' : 'password'} minLength="8" value={confirmPasswordInput} onChange={(e) => updateConfirmPasswordInput(e.target.value)} onBlur={(e) => verifyPasswordInput()} />
                                
                                <InputRightElement h={'full'}>
                                    <Button
                                        variant={'ghost'}
                                        onClick={() =>
                                            setShowCfmPassword((showCfmPassword) => !showCfmPassword)
                                        }>
                                        {showCfmPassword ? <ViewIcon /> : <ViewOffIcon />}
                                    </Button>
                                </InputRightElement>
                            </InputGroup>
                            {
                                    confirmPasswordValid ? (<FormHelperText>{confirmPasswordMessage}</FormHelperText>) : (<FormErrorMessage>{confirmPasswordMessage}</FormErrorMessage>)
                                }
                        </FormControl>
                        <FormControl id="address" isRequired>
                            <FormLabel>Address</FormLabel>
                            <Input type="text" onChange={(e) => updateAddressInput(e.target.value)} />
                        </FormControl>

                        <FormControl id="timezone" isRequired>
                            <FormLabel>Timezone</FormLabel>
                            <NumberInput maxW='150px' mr='2rem' isReadOnly
                                min={-12} max={12}
                                onChange={(valueString) => updateTimezoneInput(parse(valueString))}
                                value={format(timezoneInput)}>
                                <NumberInputField />
                            </NumberInput>
                            <Slider
                                flex='1'
                                focusThumbOnChange={false}
                                value={timezoneInput}
                                onChange={handleChange}
                                min={-12} default={0} max={12}
                            >
                                <SliderTrack>
                                    <SliderFilledTrack />
                                </SliderTrack>
                                <SliderThumb fontSize='sm' boxSize='32px'>{timezoneInput}</SliderThumb>

                            </Slider>
                        </FormControl>

                        <Stack spacing={10} pt={2}>
                            <Button
                                onClick={() => submitRegisterForm()}
                                type='submit'
                                loadingText="Submitting"
                                size="lg"
                                bg={'blue.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'blue.500',
                                }}>
                                Sign up
                            </Button>
                        </Stack>
                        <Stack pt={6}>
                            <Text align={'center'}>
                                Already a user? <Link color={'blue.400'} href="./login">Login</Link>
                            </Text>
                        </Stack>
                    </Stack>
                </Box>
            </Stack>
        </Flex>
    );
}