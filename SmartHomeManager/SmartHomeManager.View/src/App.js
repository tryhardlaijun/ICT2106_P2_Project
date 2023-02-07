import React from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

import { NavBar } from "./components/NavBar";
import Home from "./pages/Home";
import Devices from "./pages/Devices";
import Profiles from "./pages/Profiles";
import Scenario from "./pages/Scenario";
import SchRule from "pages/SchRule";
import ActionRule from "pages/ActionRule";

export function App() {
	return (
		<>
			<Router>
				<NavBar />
				<Routes>
					<Route path="/" element={<Home />} />
					<Route path="/devices" element={<Devices />} />
					<Route path="/profiles" element={<Profiles />} />
					<Route path="/scenario" element={<Scenario />} />
					<Route
						path="/scenario/create/action-rule"
						element={<ActionRule />}
					/>
					<Route
						path="/scenario/create/time-rule"
						element={<SchRule />}
					/>
					<Route
						path="/scenario/edit/:id"
						element={<SchRule />}
					/>
				</Routes>
			</Router>
		</>
	);
}

export default App;
