using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameCatalogAPI.Services;
using GameCatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using GameCatalogAPI.ViewModel;
using GameCatalogAPI.InputModel;

namespace GameCatalogAPI.Services
{
    public class GameService : IGameService
    {
        readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<ActionResult<List<GameViewModel>>> Get(int page, int amount)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<GameViewModel>> Get(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<GameViewModel>> InsertGame(GameInputModel game)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> UpdateGame(Guid gameId, GameInputModel game)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> UpdateGame(Guid gameId, double price)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> DeleteGame(Guid gameId)
        {
            throw new NotImplementedException();
        }
    }
}
