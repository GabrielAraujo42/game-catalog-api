using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int pages, int amount);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string developer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
