import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Container, Col, Row } from "reactstrap";
import { getPlaylistByUserId } from "../modules/playlistManager";
import PlaylistList from "./PlaylistList";

const UserProfilePage = ({ userProfileParam }) => {
  const [playlists, setPlaylists] = useState([]);

  useEffect(() => {
    if (userProfileParam?.id) {
      getPlaylistByUserId(userProfileParam.id).then((playlists) => {
        setPlaylists(playlists);
      });
    }
  }, [userProfileParam]);

  return (
    <Container>
      <Row>
        <Col md={4}>
          <div>
            <img
              src={userProfileParam?.image}
              alt={userProfileParam?.bio}
              style={{ width: "200px" }}
            />
            <h4>Hi I'm {userProfileParam?.username}</h4>
            <h5>{userProfileParam?.bio}</h5>
          </div>
          <div>&nbsp;</div>
          <Link to={`/playlistAddForm`}>
            <button>Create a Playlist</button>
          </Link>
        </Col>
        <Col md={8}>
          <div>
            <h3>Playlists</h3>

            <ul>
              <PlaylistList
                playlistParam={playlists}
                userProfileParam={userProfileParam}
              />
            </ul>
          </div>
        </Col>
      </Row>
    </Container>
  );
};

export default UserProfilePage;
