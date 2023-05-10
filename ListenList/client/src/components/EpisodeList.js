import React, { useEffect, useState } from "react";
import { getAllEpisodes } from "../modules/episodeManager";

import { Link } from "react-router-dom";

//PLAYLIST LISTS EPISODES
const EpisodeList = ({ userProfileId }) => {
  const [episode, setEpisode] = useState([]);

  const getEpisode = () => {
    getAllEpisodes().then((episode) => setEpisode(episode));
  };

  useEffect(() => {
    getEpisode();
  }, [userProfileId]);

  //is logged in:
  // deleteEpisode - button > message: Are you sure you want to delete?
  // editEpisode - button > takes you to episodeeditform.js

  return (
    <div>
      <h2> {userProfileId}'s playlist:</h2>
      <Link to={`/episodeList`}>
        <button>Add an Episode</button>
      </Link>

      <div>&nbsp;</div>

      <ul>
        {episode.map((episode) => (
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
            <audio src={episode.url} controls />
            <div>&nbsp;</div>

            <Link to={`/episodes/\${episode.id}/edit`}>
              <button>Edit</button>
            </Link>
            <Link to={`/episodes/\${episode.id}/delete`}>
              <button>Delete</button>
            </Link>
            <div>&nbsp;</div>
          </ul>
        ))}
      </ul>
    </div>
  );
};

export default EpisodeList;
