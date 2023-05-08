const baseUrl = "/api/episode";

export const getAllEpisodes = () => {
  return fetch(baseUrl).then((res) => res.json());
};

export const addPlaylist = (playlist) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(playlist),
  });
};
