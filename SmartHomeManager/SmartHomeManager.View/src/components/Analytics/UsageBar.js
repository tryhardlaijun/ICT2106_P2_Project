import React from 'react'
import Chart from "chart.js/auto";
import { Bar } from "react-chartjs-2";
import {
  Tabs,
  TabList,
  TabPanels,
  Tab,
  TabPanel,
  Box,
  Center,
  Container,
  Text,
  Heading
} from '@chakra-ui/react'; 

const labels = ["January", "February", "March", "April", "May", "June"];
    const data = {
      labels: labels,
      datasets: [
        {
          label: "My First dataset",
          backgroundColor: "rgb(255, 99, 132)",
          borderColor: "rgb(255, 99, 132)",
          data: [27, 10, 5, 2, 20, 30, 45],
        },
      ],
    };
function UsageBar() {
    

    return (
      <Container maxWidth={1600} mt={2} centerContent>
        <Heading pb={5}>Forecast Household Energy Usage</Heading>
        <Text pb={10}>This is your predicted total household energy usage for the following months. </Text>
          <Bar data={data} />
      </Container>
    )
  }

  
  export default UsageBar