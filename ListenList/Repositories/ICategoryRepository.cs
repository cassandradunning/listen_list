using ListenList.Models;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}