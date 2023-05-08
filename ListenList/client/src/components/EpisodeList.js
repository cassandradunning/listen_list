import React, { useEffect, useState } from "react";
import { getAllEpisodes } from "../modules/episodeManager";
import Episode from "./Episode";

const EpisodeList = () => {
  const [episode, setEpisode] = useState([]);

  const getEpisode = () => {
    getAllEpisodes().then((episode) => setEpisode(episode));
  };

  useEffect(() => {
    getEpisode();
  }, []);

  return (
    <div className="container">
      <div className="row justify-content-center">
        {episode.map((episode) => (
          <Episode episode={episode} key={episode.id} />
        ))}
      </div>
    </div>
  );
};

export default EpisodeList;
