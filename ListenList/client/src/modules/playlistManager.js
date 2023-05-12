import { getToken } from "./authManager";
const baseUrl = "/api/playlist";

export const getAllPlaylist = () => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/GetAllPlaylist`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get playlists."
        );
      }
    });
  });
};

export const getPlaylistByUserId = (userProfileId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/userProfile/${userProfileId}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get the Playlist By UserId."
        );
      }
    });
  });
};

export const getbyPlaylistId = (id) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/getPlaylistById/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get the Playlist By Id."
        );
      }
    });
  });
};

export const addPlaylist = (playlist) => {
  return getToken().then((token) => {
    return fetch(baseUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(playlist),
    }).then((resp) => {
      if (resp.ok) {
        console.log("Episode added successfully!");
        return resp.json();
      } else {
        throw new Error("An error occurred while trying to add a playlist.");
      }
    });
  });
};
