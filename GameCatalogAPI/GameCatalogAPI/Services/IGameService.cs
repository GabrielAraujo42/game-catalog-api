using GameCatalogAPI.InputModel;
using GameCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalogAPI.Services
{
    public interface IGameService : IDisposable
    {
        Task<ActionResult<List<GameViewModel>>> Get(int page, int amount);
        Task<ActionResult<GameViewModel>> Get(Guid gameId);
        Task<ActionResult<GameViewModel>> InsertGame(GameInputModel game);
        Task UpdateGame(Guid gameId, GameInputModel game);
        Task UpdateGame(Guid gameId, double price);
        Task DeleteGame(Guid gameId);
    }
}
