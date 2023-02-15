import React from "react";
import { Card, CardHeader, CardBody, CardFooter, Stack, Heading, Text, Button, Image} from '@chakra-ui/react'

export function CardComponent({ imgSrc }) {
    return (
        <Card
            direction={{ base: 'column', sm: 'row' }}
            size="sm"
            h = "200px"
            overflow='hidden'
            variant='outline'
        >
            <Image
                objectFit='cover'
                maxW={{ base: '100%', sm: '200px' }}
                src={imgSrc} 
                alt='Caffe Latte'
            />

            <Stack>
                <CardBody>
                    <Heading size='md'>Juleus</Heading>

                    <Text py='2'>
                        description of profile
                    </Text>
                </CardBody>

                <CardFooter>
                    <Button variant='solid' colorScheme='blue'>
                        Edit Profile
                    </Button>
                    <Button >
                        Edit Pin
                    </Button>
                </CardFooter>
            </Stack>
        </Card>
        )
}