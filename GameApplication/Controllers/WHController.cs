using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WHController : ControllerBase
    {
        GameRepository gameRepository { get; set; }

        public WHController()
        {
            gameRepository = new GameRepository();
        }


        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // GET api/values/5
        [HttpGet]
        [Route("players")]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var jugadores = await gameRepository.ObtenerJugadores();
                Dictionary<string, string> players = new Dictionary<string, string>();

                foreach (var jugador in jugadores)
                {
                    players.Add(jugador.Nickname, jugador.CantidadJugadas.ToString());
                }

                //var jsonPlayers = JsonConvert.SerializeObject(players);

                return Ok(players);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            
        }

        [HttpGet]
        [Route("guardar/{playerName}")]
        public async void SavePlay([FromRoute] string playerName)
        {
            var name = playerName;
            await gameRepository.GuardarAsync(playerName);
        }

        [HttpGet]
        [Route("guardarjugadas/{playerName}/{plays}")]
        public async void SavePlays([FromRoute] string playerName, string plays)
        {
            var intPlays = Convert.ToInt32(plays);
            await gameRepository.GuardarAsync(playerName, intPlays);

        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("funciona");
        }
    }
}
