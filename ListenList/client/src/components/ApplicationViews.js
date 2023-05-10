import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import EpisodeList from "./EpisodeList";
import Login from "./Login";
import Register from "./Register";
import UserProfilePage from "./UserProfilePage";
import Home from "./Home";
import PlaylistAddForm from "./PlaylistAddForm";
import EpisodeDelete from "./EpisodeDelete";
import EpisodeAddForm from "./EpisodeAddForm";
import EpisodeEditForm from "./EpisodeEditForm";

export default function ApplicationViews({ isLoggedIn, userProfile }) {
  return (
    <Routes>
      <Route path="home" element={<Home />} />
      <Route path="/">
        <Route
          index
          element={isLoggedIn ? <EpisodeList /> : <Navigate to="/login" />}
        />
        <Route
          path="add"
          element={isLoggedIn ? <EpisodeAddForm /> : <Navigate to="/login" />}
        />
        <Route
          path="edit"
          element={isLoggedIn ? <EpisodeEditForm /> : <Navigate to="/login" />}
        />
        <Route
          path="delete"
          element={isLoggedIn ? <EpisodeDelete /> : <Navigate to="/login" />}
        />
        <Route
          path="playlistAddForm"
          element={isLoggedIn ? <PlaylistAddForm /> : <Navigate to="/login" />}
        />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route
          path="userProfile"
          element={<UserProfilePage userProfile={userProfile} />}
        />
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
      </Route>
    </Routes>
  );
}
//To do: if logged in > home, Profile list, log out
// To do: if logged out > home, log in

//userProfile child for userProfilePage
