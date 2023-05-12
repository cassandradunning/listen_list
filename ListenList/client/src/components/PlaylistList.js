import React, { useEffect, useState } from "react";
import { Row, Col } from "reactstrap";
import { Link } from "react-router-dom";
import { getPlaylistByUserId } from "../modules/playlistManager";

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
