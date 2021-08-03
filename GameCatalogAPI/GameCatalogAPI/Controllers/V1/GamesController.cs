using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalogAPI.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<object>>> Get()
        {
            return Ok();
        }
        
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<object>> Get(Guid gameId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<object>> InsertGame(object game)
        {
            return Ok();
        }

        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> UpdateGame(Guid gameId, object game)
        {
            return Ok();
        }

        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame(Guid gameId, double price)
        {
            return Ok();
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> DeleteGame(Guid gameId)
        {
            return Ok();
        }
    }
}
