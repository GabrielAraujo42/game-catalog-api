using GameCatalogAPI.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public async Task<List<Game>> Get(int pages, int amount)
        {
            var games = new List<Game>();
            var command = $"select * from Games order by Id offset {((pages - 1) * amount)} rows fetch next {amount} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = Guid.Parse(sqlDataReader["Id"].ToString()),
                    Name = (string)sqlDataReader["Name"],
                    Developer = (string)sqlDataReader["Developer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<Game> Get(Guid id)
        {
            Game game = null;
            var command = $"select * from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while(sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = Guid.Parse(sqlDataReader["Id"].ToString()),
                    Name = (string)sqlDataReader["Name"],
                    Developer = (string)sqlDataReader["Developer"],
                    Price = (double)sqlDataReader["Price"]
                };
            }

            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> Get(string name, string developer)
        {
            var games = new List<Game>();
            var command = $"select * from Games where Name = '{name}' and Developer = '{developer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = Guid.Parse(sqlDataReader["Id"].ToString()),
                    Name = (string)sqlDataReader["Name"],
                    Developer = (string)sqlDataReader["Developer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task Insert(Game game)
        {
            var command = $"insert into Games (Id, Name, Developer, Price) values ('{game.Id}', '{game.Name}', '{game.Developer}', '{game.Price.ToString().Replace(",",".")}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var command = $"update Games set Name = '{game.Name}', Developer = '{game.Developer}', Price = '{game.Price.ToString().Replace(",", ".")}' where Id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Delete(Guid id)
        {
            var command = $"delete from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
