import { getToken } from "./authManager";

const baseUrl = "/api/episode";

export const getAllEpisodes = () => {
  return fetch(baseUrl).then((res) => res.json());
};

//get all, get by id, delete, add, edit

export const addEpisode = (episode) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(episode),
    }).then((resp) => {
      if (resp.ok) {
        console.log("Episode added successfully!");
        return resp.json();
      } else {
        throw new Error("An error occurred while trying to add an episode.");
      }
    });
  });
};

export const editEpisode = (id) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/${id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
  });
};
