import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Form, FormGroup, Label, Input } from "reactstrap";
import { updateEpisode, getbyEpisodeId } from "../modules/episodeManager";
import { getAllCategories } from "../modules/categoryManager"; // Import the function to get categories

const EpisodeEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  //const [dropdownText, setDropdownText] = useState("Categories");
  const [categories, setCategories] = useState([]);
  const [episode, setEpisode] = useState({
    title: "",
    description: "",
    url: "",
    image: "",
    categoryId: "",
    playlistId: [id], //we are not giving them the opportunity to change playlists
  });

  useEffect(() => {
    getbyEpisodeId(id).then((fetchedEpisode) => {
      setEpisode(fetchedEpisode);
    });
  }, []);

  useEffect(() => {
    getAllCategories().then((categories) => {
      setCategories(categories);
    });
  }, []);

  const handleSaveButtonClick = (evt) => {
    evt.preventDefault();
    updateEpisode(episode);
    window.alert("Episode updated!");
    navigate(`/userProfile`);
  };
  const handleInputChange = (evt) => {
    const key = evt.target.id;
    const value = evt.target.value;

    const episodeCopy = { ...episode };
    episodeCopy[key] = value;
    setEpisode(episodeCopy);
  };

  // const handleDropDownCategories = (e) => {
  //   e.preventDefault();
  //   setDropdownText(e.target.name);
  // };

  return (
    <Form>
      <fieldset>
        {console.log(episode)}
        <FormGroup>
          <Label for="title">Title</Label>
          <Input
            id="title"
            type="title"
            value={episode.title}
            className="form-control"
            placeholder="Episode Title"
            onChange={handleInputChange}
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
            onChange={handleInputChange}
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
            onChange={handleInputChange}
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
            onChange={handleInputChange}
          />
        </FormGroup>
        {/* <FormGroup>
          <Label for="category">Category</Label> */}
        {/* <select
            id="category"
            value={episode.categoryId}
            className="form-control"
          >
            <option>Select a category</option>
            {categories.map((category) => (
              <option
                onClick={(e) => {
                  const copy = { ...episode };
                  copy.categoryId = e.target.value;
                  setEpisode(copy);
                }}
                key={category.id}
                value={category.id}
              >
                {category.name}
              </option>
            ))}
          </select> */}

        <FormGroup>
          <Input type="select" id="categoryId">
            <option value="">Select Category</option>
            {categories.map((t) => {
              return (
                <option
                  onClick={handleInputChange}
                  value={t.id}
                  key={t.id}
                  id="categoryId"
                >
                  {t.name}
                </option>
              );
            })}
          </Input>
        </FormGroup>

        {/* <Label for="category">Category</Label>
          <Input
            type="select"
            id="categoryId"
            value={episode.categoryId}
            onChange={handleInputChange} // Add onChange event handler
          >
            <option value="">Select Category</option>
            {categories.map((t) => (
              <option key={t.id} value={t.id}>
                {t.name}
              </option>
            ))}
          </Input>
        </FormGroup> */}

        <FormGroup>
          <button onClick={handleSaveButtonClick}>Update Episode </button>
        </FormGroup>
      </fieldset>
    </Form>
  );
};
export default EpisodeEdit;
