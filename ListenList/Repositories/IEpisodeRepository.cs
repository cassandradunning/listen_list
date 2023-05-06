using ListenList.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    public interface IEpisodeRepository
    {
        public List<Episode> GetAllEpisodes();
        public object GetbyEpisodeId(int id);
        public void AddEpisode(Episode quote);
        public void UpdateEpisode(Episode episode);

        public void DeleteEpisode(int id);

    }
}