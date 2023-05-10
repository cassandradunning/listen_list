import React, { useEffect, useState } from "react";
import UserProfilePage from "./UserProfilePage";
import { getAllPlaylist } from "../modules/playlistManager";

export default function PlaylistList({ playlistParam }) {
  //const [playlist, setPlaylist] = useState([]);

  // useEffect(() => {
  //   getAllPlaylist().then(setPlaylist);
  // }, []);

  return (
    <section>
      {playlistParam.map((playlist) => (
        <div>{playlist.name}</div>
      ))}
    </section>
  );
}
