using GameCatalogAPI.Entities;
using GameCatalogAPI.Exceptions;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.Repositories;
using GameCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogAPI.Services
{
    public class GameService : IGameService
    {
        readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<ActionResult<List<GameViewModel>>> Get(int page, int amount)
        {
            var games = await _gameRepository.Get(page, amount);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            }).ToList();
        }

        public async Task<ActionResult<GameViewModel>> Get(Guid gameId)
        {
            var game = await _gameRepository.Get(gameId);

            if (game == null) return null;

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            };
        }

        public async Task<ActionResult<GameViewModel>> InsertGame(GameInputModel game)
        {
            var gameEntity = await _gameRepository.Get(game.Name, game.Developer);

            if (gameEntity.Count() > 0) 
                throw new GameAlreadyRegisteredException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            };
        }

        public async Task UpdateGame(Guid gameId, GameInputModel game)
        {
            var gamesList = await _gameRepository.Get(game.Name, game.Developer);

            if (gamesList.Count() > 0)
                throw new GameAlreadyRegisteredException();

            var gameEntity = await _gameRepository.Get(gameId);

            if (gameEntity == null)
                throw new GameNotFoundException();

            gameEntity.Name = game.Name;
            gameEntity.Developer = game.Developer;
            gameEntity.Price = game.Price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task UpdateGame(Guid gameId, double price)
        {
            var gameEntity = await _gameRepository.Get(gameId);

            if (gameEntity == null)
                throw new GameNotFoundException();

            gameEntity.Price = price;

            await _gameRepository.Update(gameEntity);
        }

        public async Task DeleteGame(Guid gameId)
        {
            var gameEntity = await _gameRepository.Get(gameId);

            if (gameEntity == null)
                throw new GameNotFoundException();

            await _gameRepository.Delete(gameId);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
