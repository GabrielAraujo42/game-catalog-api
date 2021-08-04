using GameCatalogAPI.Exceptions;
using GameCatalogAPI.InputModel;
using GameCatalogAPI.Services;
using GameCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Gets all games in pages
        /// </summary>
        /// <remarks>
        /// Not possible to get elements in a format other than pages
        /// </remarks>
        /// <param name="page">Which page is being checked. Minimum: 1</param>
        /// <param name="amount">How many elements per page. Minimum: 1 - Maximum: 50</param>
        /// <response code="200">Returns game list</response>
        /// <response code="204">Catalog is empty</response>
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
        
        /// <summary>
        /// Gets a game by Id
        /// </summary>
        /// <param name="gameId">Id of the searched game</param>
        /// <response code="200">Returns searched game</response>
        /// <response code="204">No game with given Id found</response>
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

        /// <summary>
        /// Inserts a new game into the repository
        /// </summary>
        /// <param name="gameInputModel">Game's information to be registered (Name, Developer, Price)</param>
        /// <response code="200">Game successfully registered</response>
        /// <response code="422">Game already exists in catalog</response>
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

        /// <summary>
        /// Updates a register in the repository
        /// </summary>
        /// <param name="gameId">Id of the game to be updated</param>
        /// <param name="gameInputModel">Game's updated information</param>
        /// <response code="200">Game successfully updated</response>
        /// <response code="404">Game to be updated not found</response>
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

        /// <summary>
        /// Updates price information of a registered game
        /// </summary>
        /// <param name="gameId">Id of the game to be updated</param>
        /// <param name="price">Game's updated price</param>
        /// <response code="200">Game's price successfully updated</response>
        /// <response code="404">Game to be updated not found</response>
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

        /// <summary>
        /// Deletes a game from the repository
        /// </summary>
        /// <param name="gameId">Id of the game to be deleted</param>
        /// <response code="200">Game successfully deleted</response>
        /// <response code="404">Game to be deleted not found</response>
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
