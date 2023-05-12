import React, { useEffect, useState } from "react";
import {
  getAllEpisodes,
  getEpisodeByPlaylistId,
} from "../modules/episodeManager";
import AudioPlayer from "./AudioPlayer";

import { Link, useParams } from "react-router-dom";

//PLAYLIST LISTS EPISODES
const EpisodeList = ({ isLoggedIn }) => {
  const { id } = useParams();
  const [episodes, setEpisodes] = useState([]);

  useEffect(() => {
    getEpisodeByPlaylistId(id).then(setEpisodes);
  }, []);

  //is logged in:
  // deleteEpisode - button > message: Are you sure you want to delete?
  // editEpisode - button > takes you to episodeeditform.js

  return (
    <div>
      <h2>Playlist</h2>
      {isLoggedIn ? (
        <>
          <Link to={`/episodeAdd/${id}`}>
            <button>Add an Episode</button>
          </Link>
        </>
      ) : null}
      <div>&nbsp;</div>

      <ul>
        {episodes.map((episode) => (
          <ul key={episode.id}>
            <div style={{ margin: "100px" }}>
              <img
                src={episode.image}
                alt="{episode.description}"
                style={{ width: "200px" }}
              />
            </div>
            <h3>{episode.title}</h3>
            <p>{episode.description}</p>
            <Link to={episode.url}>
              <div className="audio">
                <img
                  src="https://i.postimg.cc/150MD95T/sound.png"
                  alt={episode.title}
                />
              </div>
              <div>&nbsp;</div>
            </Link>
            {isLoggedIn ? (
              <>
                <Link to={`/episodeEdit/${episode.id}`}>
                  <button>Edit</button>
                </Link>
                <Link to={`/episodeDelete/${episode.id}`}>
                  <button>Delete</button>
                </Link>
              </>
            ) : null}
            <div>&nbsp;</div>
          </ul>
        ))}
      </ul>
    </div>
  );
};

export default EpisodeList;
//  <audio src={episode.url} controls />
//<AudioPlayer src={episode.url} />
