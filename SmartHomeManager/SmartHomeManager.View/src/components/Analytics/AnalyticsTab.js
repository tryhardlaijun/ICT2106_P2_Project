import React from 'react'
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
    Heading,
    Button,
} from '@chakra-ui/react';
import CarbonChart from './CarbonChart';
import UsageBar from './UsageBar';
import Report from './Report';


function AnalyticsTab() {
    return (
        <Tabs isFitted variant='enclosed'>
            <TabList mb='1em'>
                <Tab>Carbon Footprint</Tab>
                <Tab>Energy Usage</Tab>
                <Tab>Energy Efficiency</Tab>
                <Tab>Report</Tab>
            </TabList>
            <TabPanels>
                <TabPanel>
                    <CarbonChart />
                </TabPanel>
                <TabPanel>
                    <UsageBar />
                </TabPanel>
                <TabPanel>
                    <p>Efficiency</p>
                </TabPanel>
                <TabPanel>
<<<<<<< HEAD
                <Container>
                        <Report />
                    </Container>
=======
                    <Report />
>>>>>>> dd29b558b0132bd09a8202779f6d4ace12fe602b
                </TabPanel>
            </TabPanels>
        </Tabs>
    )
}

export default AnalyticsTab