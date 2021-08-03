using GameCatalogAPI.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogAPI.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public Task<List<Game>> Get(int pages, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<Game> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Game>> Get(string name, string developer)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Game game)
        {
            throw new NotImplementedException();
        }

        public Task Update(Game game)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
