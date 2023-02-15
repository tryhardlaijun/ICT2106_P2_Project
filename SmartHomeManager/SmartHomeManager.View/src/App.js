import React from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

import { NavBar } from "./components/NavBar";
import Home from "./pages/Home";
import Devices from "./pages/Devices";
import Profiles from "./pages/Profiles";
import DeviceConfig from "./pages/DeviceConfig";
import Report from "pages/Analytics";

export function App() {
  return (
    <>
      <Router>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/devices" element={<Devices />} />
          <Route path="/profiles" element={<Profiles />} />
          <Route path="/config" element={<DeviceConfig />} />
          <Route path="/analytics" element={<Report />} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
