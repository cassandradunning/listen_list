import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getbyEpisodeId, deleteEpisode } from "../modules/episodeManager";
import { FormGroup } from "reactstrap";

const EpisodeDelete = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [episode, setEpisode] = useState({});

  useEffect(() => {
    getbyEpisodeId(id).then(setEpisode);
  }, [id]);

  const handleDeleteButtonClick = (e) => {
    e.preventDefault();

    deleteEpisode(episode.id, episode);
    navigate("../../playlist/{id}");
  };

  const handleCancelButtonClick = (e) => {
    navigate("../../playlist/{id}");
  };

  return (
    <fieldset>
      <h3>Are you sure you want to delete {episode.name}?</h3>
      <FormGroup>
        <button onClick={handleDeleteButtonClick}>Yes </button>
        <button onClick={handleCancelButtonClick}>No </button>
      </FormGroup>
    </fieldset>
  );
};
export default EpisodeDelete;
