import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Form, FormGroup, Label, Input } from "reactstrap";
import { addPlaylist } from "../modules/playlistManager";

export default function PlaylistAddForm() {
  const navigate = useNavigate();
  const [playlist, setPlaylist] = useState({
    name: "",
    image: "",
    episodeId: [],
  });

  const handleSaveButtonClick = (e) => {
    e.preventDefault();

    addPlaylist(playlist)
      .then(() => {
        const copy = { ...playlist };
        copy.name = "";
        copy.image = "";
        window.alert("Playlist added!");
        navigate("/userProfile");
      })
      .catch((err) => alert(`An error ocurred: ${err.message}`));
  };

  return (
    <Form>
      <fieldset>
        <FormGroup>
          <Label for="name">Name</Label>
          <Input
            id="name"
            type="name"
            value={playlist.name}
            className="form-control"
            placeholder="Playlist Name"
            onChange={(e) => {
              const copy = { ...playlist };
              copy.name = e.target.value;
              setPlaylist(copy);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label for="image">Image</Label>
          <Input
            id="image"
            type="text"
            value={playlist.image}
            className="form-control"
            placeholder="Playlist Image"
            onChange={(e) => {
              const copy = { ...playlist };
              copy.image = e.target.value;
              setPlaylist(copy);
            }}
          />
        </FormGroup>
        <FormGroup>
          <button onClick={handleSaveButtonClick}>Add Playlist </button>
        </FormGroup>
      </fieldset>
    </Form>
  );
}
