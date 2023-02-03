import React, { useState } from "react";
import { sha256 } from 'react-native-sha256';

import {
    Button, FormControl,
    FormLabel,
    FormErrorMessage,
    FormHelperText,
    Input, NumberInput,
    NumberInputField,
    NumberInputStepper,
    Slider,
    SliderTrack,
    SliderFilledTrack,
    SliderThumb,
} from "@chakra-ui/react";
export default function Register() {
    /*const [newState, updateNewState] = useState(0)
    const [newState2, updateNewState2] = useState("")
    useEffect(() => {
        console.log("Hello")
    });

    useEffect(() => {
        console.log("asd")
    }), [newState2];


            <h1>{newState}</h1>
            {
                newState >= 5 ? <h1>More than 5</h1> : <h1>Less than 5</h1>
            }
            {
                newState2 == "ASD" ? <h1>{newState2}</h1> : <h1>nothing</h1>
            }
            <Button colorScheme='blue' onClick={() => updateNewState(newState + 1)}>Button</Button>
            <Button colorScheme='blue' onClick={() => updateNewState2("ASD")}>Button</Button>
            
    */

    //Input declaration
    const [emailInput, updateEmailInput] = useState("")
    const [usernameInput, updateUsernameInput] = useState("")
    const [passwordInput, updatePasswordInput] = useState("")
    const [confirmPasswordInput, updateConfirmPasswordInput] = useState("")
    const [timezoneInput, updateTimezoneInput] = useState('0')
    const [addressInput, updateAddressInput] = useState("")
    const [hashPassword, setHashPassword] = useState("");

    //Boolean declaration for validation
    const [emailValid, updateEmailValid] = useState(true)
    const [passwordMessage, updatePasswordMessage] = useState("")
    const [passwordValid, updatePasswordValid] = useState(true)
    const [confirmPasswordMessage, updateConfirmPasswordMessage] = useState("")
    const [confirmPasswordValid, updateConfirmPasswordValid] = useState(true)

    //Use for number slider
    const handleChange = (timezoneInput) => updateTimezoneInput(timezoneInput)
    const format = (val) => "GMT " + val
    const parse = (val) => val.replace(/^\GMT /, "")

    //Function to verify email & password
    const checkEmailInput = (emailInput) => {
        if (emailInput.length == 0) {
            updateEmailValid(true)
        } else {
            var mailFormat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            updateEmailValid(mailFormat.test(emailInput))
        }
    }

    const verifyPasswordInput = () => {
        if (passwordInput.length == 0) {
            updatePasswordValid(true)
        }
        if (confirmPasswordInput.length == 0) {
            updateConfirmPasswordValid(true)
        }
            //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
            var passwordFormat = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
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

        //Check if password match
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

  

    //Submit form
    const submitRegisterForm = () => {
        if (passwordInput != confirmPasswordInput) {
            
            console.log("not match password");
        }
        convertSHA()
        console.log({ emailInput })
        console.log({ usernameInput })
        console.log({ timezoneInput })
        console.log({ passwordInput })
        console.log({ confirmPasswordInput })
        console.log({ hashPassword })
        console.log({ addressInput })
        /*
        fetch('/localhost:7140/api/accounts/', {
            method: 'POST',
            body: JSON.stringify({
                emailInput, usernameInput, addressInput, timezoneInput, passwordInput
            }),
        headers: {
                'Content-type': 'application/json; charset=UTF-8',
            },
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
            })
            .catch((err) => {
                console.log(err.message);
            });
            */
    }



    return (
        <>
            <FormControl isInvalid={!emailValid}>
                <FormLabel>Email address</FormLabel>
                <Input type="email" onChange={(e) => { updateEmailInput(e.target.value); checkEmailInput(e.target.value) }} />
                {
                    emailValid && emailInput>0 ? <FormHelperText>Email is valid!</FormHelperText> : <FormErrorMessage>Email is invalid!</FormErrorMessage> 
                }
            </FormControl>

            <FormControl>
                <FormLabel>Username</FormLabel>
                <Input type="string" onChange={(e) => updateUsernameInput(e.target.value)} />
            </FormControl>

            <FormControl isInvalid={!passwordValid}>
                <FormLabel>Password</FormLabel>
                <Input type="password" minlength="8" value={passwordInput} onChange={(e) => updatePasswordInput(e.target.value)} onBlur={(e) => verifyPasswordInput()} />
                {
                    passwordValid ? <FormHelperText>{passwordMessage}</FormHelperText> : <FormErrorMessage>{passwordMessage}</FormErrorMessage>
                }
            </FormControl>

            <FormControl isInvalid={!confirmPasswordValid}>
                <FormLabel>Confirm Password</FormLabel>
                <Input type="password" minlength="8" value={confirmPasswordInput} onChange={(e) => updateConfirmPasswordInput(e.target.value)} onBlur={(e) => verifyPasswordInput()} />
                {
                    confirmPasswordValid ? <FormHelperText>{confirmPasswordMessage}</FormHelperText> : <FormErrorMessage>{confirmPasswordMessage}</FormErrorMessage>
                }
            </FormControl>

            <FormControl>
                <FormLabel>Address</FormLabel>
                <Input type="string" onChange={(e) => updateAddressInput(e.target.value)} />
            </FormControl>

            <FormControl>
                <FormLabel>Timezone</FormLabel>
                <NumberInput maxW='150px' mr='2rem' isReadOnly
                    min={-12} max={12}
                    onChange={(valueString) => updateTimezoneInput(parse(valueString))}
                    value={format(timezoneInput)}>
                    <NumberInputField />
                    <NumberInputStepper>
                    </NumberInputStepper>
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

                <Button
                    onClick={() => submitRegisterForm()}
                    mt={4}
                    colorScheme='blue'
                    type='submit'
                >
                    Submit
            </Button>

        </>
    )
}
