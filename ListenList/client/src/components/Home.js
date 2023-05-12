// To do: list all users (map through and include key, username, image, about )
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Container, Row } from "reactstrap";
import { getAllPlaylist } from "../modules/playlistManager";
import PlaylistList from "./PlaylistList";

export default function Home() {
  const [playlists, setPlaylist] = useState([]);

  useEffect(() => {
    getAllPlaylist().then(setPlaylist);
  }, []);

  return (
    <Container>
      <Row>
        <ul>
          <PlaylistList playlistParam={playlists} />
        </ul>
      </Row>
    </Container>
  );
}
