import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import EpisodeList from "./EpisodeList";
//import EpisodeForm from "./EpisodeForm";
import Login from "./Login";
import Register from "./Register";
import UserProfilePage from "./UserProfilePage";
import Home from "./Home";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={isLoggedIn ? <EpisodeList /> : <Navigate to="/login" />}
        />
        {/* <Route
          path="add"
          element={isLoggedIn ? <EpisodeForm /> : <Navigate to="/login" />}
        /> */}
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="userProfile" element={<UserProfilePage />} />
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
      </Route>
    </Routes>
  );
}
//To do: if logged in > home, Profile list, log out
// To do: if logged out > home, log in
