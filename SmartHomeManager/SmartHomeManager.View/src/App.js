import React from "react";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

import { NavBar } from "./components/NavBar";
import Home from "./pages/Home";
import Devices from "./pages/Devices";
import Profiles from "./pages/Profiles";
import Register from "./pages/account/Register";
import ForgetPassword from "./pages/account/ForgetPassword";
import Login from "./pages/account/Login";
import RegisterOK from "./pages/account/RegisterOK";
import MyAccount from "./pages/account/Setting";
import UserProfileEdit from "./pages/profile/EditProfile";
import ProfileLanding from "./pages/profile/ProfileLanding"

export function App() {
  return (
    <>
      <Router>
        <NavBar />
        <Routes>
          <Route path="/" element={<Home />} />
                  <Route path="/devices" element={<Devices />} />
                  <Route path="/profiles" element={<Profiles />} />
                  <Route path="/register" element={<Register />} />
                  <Route path="/forgetpw" element={<ForgetPassword />} />
                  <Route path="/login" element={<Login />} />
                  <Route path="/myaccount" element={<MyAccount />} />
                  <Route path="/account-created" element={<RegisterOK />} />
                  <Route path="/edit-profile" element={<UserProfileEdit />} />
                  <Route path="/profile-landing" element={<ProfileLanding />} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
