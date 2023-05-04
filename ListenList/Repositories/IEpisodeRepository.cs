using ListenList.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    internal interface IEpisodeRepository
    {
        public List<Episode> GetAll();
        public object GetbyId(int id);
        public void Add(Episode quote);
        public void Update(Episode episode);
        public void Delete(int id);
    }
}