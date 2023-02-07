import {
    Button,
    Td
} from '@chakra-ui/react'
import React from "react";

function Buttons() {
    return (
        <Td>
            <Button ml={2} colorScheme='green' >
                Add
            </Button>
            <Button ml={2} colorScheme='teal' >
                More
            </Button>
            <Button ml={2} colorScheme='blue' >
                Edit
            </Button>
            <Button ml={2} colorScheme='red' >
                Delete
            </Button>

        </Td>
    );
}


export default Buttons;