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
} from '@chakra-ui/react';
import CarbonChart from './CarbonChart';
import UsageBar from './UsageBar';


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
                    <Container maxWidth={1800} mt={2} centerContent>
                        <CarbonChart />
                    </Container>
                </TabPanel>
                <TabPanel>
                    <Container maxWidth={1800} mt={2} centerContent>
                        <UsageBar />
                    </Container>
                </TabPanel>
                <TabPanel>
                    <p>Energy Efficiency</p>
                </TabPanel>
                <TabPanel>
                    <p>Report</p>
                </TabPanel>
            </TabPanels>
        </Tabs>
    )
}

export default AnalyticsTab