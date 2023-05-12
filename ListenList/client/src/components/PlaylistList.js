import React, { useEffect, useState } from "react";
import UserProfilePage from "./UserProfilePage";
// import { getAllPlaylist } from "../modules/playlistManager";
import { Link, useParams } from "react-router-dom";
import {
  getbyPlaylistId,
  getPlaylistByUserId,
} from "../modules/playlistManager";
import { getEpisodeByPlaylistId } from "../modules/episodeManager";

export default function PlaylistList({ playlistParam, userProfileParam }) {
  const [playlist, setPlaylist] = useState([]);

  useEffect(() => {
    if (userProfileParam) {
      getPlaylistByUserId(userProfileParam?.id).then(setPlaylist);
    }
  }, [userProfileParam]);

  if (!playlistParam) {
    return "";
  }
  return (
    <section>
      {playlistParam.map((item) => (
        <div key={item.id}>
          <Link to={`/playlist/${item.id}`}>
            <div className="image-card">
              <img src={item.image} alt={item.name} />
              <h5>{item.name}</h5>
            </div>
          </Link>
        </div>
      ))}
    </section>
  );
}
