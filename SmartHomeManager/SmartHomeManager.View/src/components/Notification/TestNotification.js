import  React  from "react";
import {
    FormControl,
    FormLabel,
    Input,
    Button,
    useToast,
    Toast,
  } from "@chakra-ui/react";
  

export default function TestNotification(){

const toast = useToast();


const handleClick = () => {
    toast({
        title: "Button was clicked",
        description: "Message submitted",
        status: "success",
        duration: 15000,
        isClosable: false
    });
};

return(
    <>
      <form>
        <FormControl>
          <FormLabel htmlFor="message">Message</FormLabel>
          <Input type="text" id="message" placeholder="Notification Message"/>
        </FormControl>
        {/* <Button mt={4} type="submit">  */}
        <Button mt={4} onClick={handleClick}> 
          Submit
        </Button>
      </form>
    </>
  )
}