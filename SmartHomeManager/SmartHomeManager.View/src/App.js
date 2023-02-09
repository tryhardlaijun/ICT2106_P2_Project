import React from 'react'
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom'

import { NavBar } from './components/NavBar'
import Home from './pages/Home'
import Devices from './pages/Devices'
import Profiles from './pages/Profiles'
import Rooms from './pages/Rooms'
import { Container } from '@chakra-ui/react'

export function App() {
	return (
		<>
			<Router>
				<NavBar />
				<Container maxW={'6xl'} py={4}>
					<Routes>
						<Route path="/" element={<Home />} />
						<Route path="/devices" element={<Devices />} />
						<Route path="/rooms" element={<Rooms />} />
						<Route path="/profiles" element={<Profiles />} />
					</Routes>
				</Container>
			</Router>
		</>
	)
}

export default App;
