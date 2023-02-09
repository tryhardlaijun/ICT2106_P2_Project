import { React } from 'react';
import {
    Flex,
    Box,
    Stack,
    Button,
    Heading,
    useColorModeValue,
} from '@chakra-ui/react';

export default function RegisterOK() {

    //Function to verify email
    const goHome = () => {
        window.location.href = "/";
    }
    const login = () => {
        window.location.href = "/login";
    }

    return (
        <Flex
            minH={'100vh'}
            align={'center'}
            justify={'center'}
            bg={useColorModeValue('gray.50', 'gray.800')}>
            <Stack spacing={8} mx={'auto'} maxW={'lg'} py={12} px={6}>

                <Box
                    rounded={'lg'}
                    bg={useColorModeValue('white', 'gray.700')}
                    boxShadow={'lg'}
                    p={8}>
                        <Stack spacing={4}>
                            <Stack align={'center'}>
                                <Heading textAlign={'center'} fontSize={'4xl'}>Your account has been created.</Heading>
                                <Heading textAlign={'center'} fontSize={'1xl'}>Check your email for verification!</Heading>
                            </Stack>
                            <Button
                                onClick={() => login()}
                                bg={'blue.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'blue.500',
                                }}>
                                Sign in
                            </Button>
                            <Button
                                onClick={() => goHome()}
                                bg={'blue.400'}
                                color={'white'}
                                _hover={{
                                    bg: 'blue.500',
                                }}>
                                Back to Home
                            </Button>
                        </Stack>
                </Box>
            </Stack>
        </Flex>
    );
}
