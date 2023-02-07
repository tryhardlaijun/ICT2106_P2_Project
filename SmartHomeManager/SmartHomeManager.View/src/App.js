import React from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

import { NavBar } from "./components/NavBar";
import Home from "./pages/Home";
import Devices from "./pages/Devices";
import Profiles from "./pages/Profiles";
import Director from "./pages/Director";
import Backup from "./pages/Backup";
import Intruder from "./pages/Intruder";
import Configuration from "./pages/Configuration";

export function App() {
  return (
    <>
      <Router>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/devices" element={<Devices />} />
          <Route path="/profiles" element={<Profiles />} />
          <Route path="/director" element={<Director />} />
          <Route path="/backup" element={<Backup />} />
          <Route path="/intruder" element={<Intruder />} />
          <Route path="/configuration" element={<Configuration />} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
