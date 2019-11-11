using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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


        // Este metodo es solo para testear que se llega a la API
        // En el juego es el metodo que se llama desde "Check Conn"
        // Aun despues de esto puede haber error en la conexion a la base de datos
        // Eso se contempla en el C++
        [HttpGet]
        [Route("test")]
        public IActionResult Test() {
            return Ok("Conexion a la api establecida");
        }


        // Este metodo guarda una jugada y la suma (1) si el jugador ya existe
        // Si no existe lo crea (esa implementacion esta en la capa datos)
        // Es un metodo GET y se le pasa el nombre del jugador por ruta
        // Por ejemplo '/api/WH/guardar/CosmeFulanito' se fija si existe CosmeFulanito
        // si no existe lo crea e inicializa sus jugadas en 1, de ya existir le suma a sus jugadas 1
        [HttpGet]
        [Route("guardar/{playerName}")]
        public async void SavePlay([FromRoute] string playerName) {
            var name = playerName;
            await gameRepository.GuardarAsync(playerName);
        }

        // Este metodo es una sobrecarga del anterior, donde se pasa por ruta el nombre del jugador
        // y un numero X de jugadas, no se sumariza, sobreescribe el numero anterior
        // este metodo NO sera implementado en el juego, solo creamos el controlador a efectos academicos
        [HttpGet]
        [Route("guardarjugadas/{playerName}/{plays}")]
        public async void SavePlays([FromRoute] string playerName, string plays) {
            var intPlays = Convert.ToInt32(plays);
            await gameRepository.GuardarAsync(playerName, intPlays);

        }

        // Sobrecarga del metodo, guarda partida en jugador default
        // NO se implementa en el juego
        [HttpGet]
        [Route("guardardefault")]
        public async void SaveDefaut() {
            await gameRepository.GuardarAsync();

        }

        // Sobrecarga del metodo, guarda partidas (sobreescribe) en jugador default
        // NO se implementa en el juego
        [HttpGet]
        [Route("guardardefault/{plays}")]
        public async void SaveDefaut(string plays) {
            var intPlays = Convert.ToInt32(plays);

            await gameRepository.GuardarAsync();

        }

        


        // Metodo que toma la lista de jugadores almacenados en la base de datos
       

        [HttpGet]
        [Route("players")]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var jugadores = await gameRepository.ObtenerJugadores();
                //Dictionary<string, string> players = new Dictionary<string, string>();

                //foreach (var jugador in jugadores) {
                //    players.Add(jugador.Nickname, jugador.CantidadJugadas.ToString());
                //}


                //var jsonPlayers = JsonConvert.SerializeObject(players);

                //var jsonPlayers = jugadores.First();

                var jsonPlayers = JsonConvert.SerializeObject(jugadores);                

                return Ok(jsonPlayers);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }            
        }


        [HttpGet]
        [Route("playersByIndex/{index}")]
        public async Task<IActionResult> GetPlayersByIndex([FromRoute] string index) {
            try {
                int indexInt = Convert.ToInt32(index);
                var jugadores = await gameRepository.ObtenerJugadores();

                var jugador = jugadores[indexInt];

                var jsonNumber = JsonConvert.SerializeObject(jugador);

                return Ok(jsonNumber);
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
        }




        [HttpGet]
        [Route("number")]
        public async Task<IActionResult> GetNumberOfPlayers() {
            try {
                var jugadores = await gameRepository.ObtenerJugadores();

                Int32 number = jugadores.Count();

                var jsonNumber = JsonConvert.SerializeObject(number);
               
                return Ok(jsonNumber);
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
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
    }
}
