using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.ViewModel;
using GameCatalogAPI.Services;
using GameCatalogAPI.Exceptions;

namespace GameCatalogAPI.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
        {
            var games = await _gameService.Get(page, amount);
            if (games.Value.Count() == 0)
            {
                return NoContent();
            }
            return Ok(games);
        }
        
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid gameId)
        {
            var game = await _gameService.Get(gameId);
            if (game == null)
            {
                return NoContent();
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.InsertGame(gameInputModel);

                return Ok(game);
            }
            catch(GameAlreadyRegisteredException ex)
            {
                return UnprocessableEntity("Catalog already contains a game with same name and developer.");
            }
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.UpdateGame(gameId, gameInputModel);

                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound("Game not found.");
            }
        }

        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await _gameService.UpdateGame(gameId, price);

                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound("Game not found.");
            }
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid gameId)
        {
            try
            {
                await _gameService.DeleteGame(gameId);

                return Ok();
            }
            catch (GameNotFoundException ex)
            {
                return NotFound("Game not found.");
            }
        }
    }
}
