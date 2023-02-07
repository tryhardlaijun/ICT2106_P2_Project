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
    Heading
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
                    <CarbonChart />
                </TabPanel>
                <TabPanel>
                    <UsageBar />
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