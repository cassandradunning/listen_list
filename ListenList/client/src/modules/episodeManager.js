import { getToken } from "./authManager";

const baseUrl = "/api/episode";

export const getAllEpisodes = () => {
  return getToken().then((token) => {
    return fetch(baseUrl + "/GetAll", {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get episodes."
        );
      }
    });
  });
};

export const getbyEpisodeId = (id) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/getEpisodeById/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get the Episode By Id."
        );
      }
    });
  });
};

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

export const updateEpisode = (id) => {
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

export const deleteEpisode = (id) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/${id}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
  });
};
