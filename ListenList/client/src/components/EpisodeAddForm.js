import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
// import { Form, FormGroup, Label, Input } from "reactstrap";
import { Form, FormGroup, Label, Input } from "reactstrap";

import { addEpisode } from "../modules/episodeManager";
import { getAllCategories } from "../modules/categoryManager"; // Import the function to get categories

export default function EpisodeAddForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [categories, setCategories] = useState([]);

  const [episode, setEpisode] = useState({
    title: "",
    description: "",
    url: "",
    image: "",
    categoryId: "",
    playlistId: [id],
  });

  useEffect(() => {
    getAllCategories().then(setCategories);
  }, []);

  const handleSaveButtonClick = (e) => {
    e.preventDefault();

    addEpisode(episode).then(() => {
      const copy = { ...episode };
      copy.title = "";
      copy.description = "";
      copy.url = "";
      copy.image = "";
      copy.categoryId = "";
      window.alert("Episode added!");
      navigate(`/playlist/${id}`);
    });
  };

  return (
    <Form>
      <fieldset>
        <FormGroup>
          <Label for="title">Title</Label>
          <Input
            id="title"
            type="title"
            value={episode.title}
            className="form-control"
            placeholder="Episode Title"
            onChange={(e) => {
              const copy = { ...episode };
              copy.title = e.target.value;
              setEpisode(copy);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label for="description">Description</Label>
          <Input
            id="description"
            type="description"
            value={episode.description}
            className="form-control"
            placeholder="Episode Description"
            onChange={(e) => {
              const copy = { ...episode };
              copy.description = e.target.value;
              setEpisode(copy);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label for="url">Url</Label>
          <Input
            id="url"
            type="url"
            value={episode.url}
            className="form-control"
            placeholder="Episode Url"
            onChange={(e) => {
              const copy = { ...episode };
              copy.url = e.target.value;
              setEpisode(copy);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label for="image">Image</Label>
          <Input
            id="image"
            type="text"
            value={episode.image}
            className="form-control"
            placeholder="Episode Image"
            onChange={(e) => {
              const copy = { ...episode };
              copy.image = e.target.value;
              setEpisode(copy);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label for="category">Category</Label>

          <select
            id="category"
            value={episode.categoryId}
            className="form-control"
            onChange={(e) => {
              const copy = { ...episode };
              copy.categoryId = e.target.value;
              setEpisode(copy);
            }}
          >
            <option value="">Select a category</option>
            {categories.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
        </FormGroup>

        <FormGroup>
          <button onClick={handleSaveButtonClick}>Add Episode </button>
        </FormGroup>
      </fieldset>
    </Form>
  );
}
