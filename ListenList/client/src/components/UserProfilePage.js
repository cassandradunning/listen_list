import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Container, Col, Row } from "reactstrap";
import { getPlaylistByUserId } from "../modules/playlistManager";
import PlaylistList from "./PlaylistList";

const UserProfilePage = ({ userProfile }) => {
  const [playlists, setPlaylists] = useState([]);

  useEffect(() => {
    if (userProfile?.id) {
      getPlaylistByUserId(userProfile.id).then((playlists) => {
        setPlaylists(playlists);
      });
    }
  }, [userProfile]);

  return (
    <Container>
      <Row>
        <Col md={4}>
          <div>
            <img
              src={userProfile?.image}
              alt="{userProfile?.bio}"
              style={{ width: "200px" }}
            />
            <h4>{userProfile?.username}</h4>
            <h4>{userProfile?.bio}</h4>
          </div>
          <Link to={`/playlistAddForm`}>
            <button>Create a Playlist</button>
          </Link>
        </Col>
        <Col md={8}>
          <div>
            <h3>Playlists</h3>

            <ul>
              <PlaylistList playlistParam={playlists} />
            </ul>
          </div>
        </Col>
      </Row>
    </Container>
  );
};

export default UserProfilePage;
