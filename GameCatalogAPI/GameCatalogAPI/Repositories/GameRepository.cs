using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogAPI.Entities;

namespace GameCatalogAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>();

        public Task<List<Game>> Get(int pages, int amount)
        {
            // Skip: Skips that amount of elements
            // Take: Returns x amount of elements from starting point in dictionary
            return Task.FromResult(games.Values.Skip((pages - 1) * amount).Take(amount).ToList());
        }

        public Task<Game> Get(Guid id)
        {
            /* Fixed null case by returning a null Task casting as Game,
             * instead of the return null from tutorial
             */
            if (!games.ContainsKey(id))
                return Task.FromResult<Game>(null);

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Get(string name, string developer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Developer.Equals(developer)).ToList());
        }

        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Close database connection
        }
    }
}
