using DataLayer.Models;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class GameRepository : RepositoryBase, IGameRepository
	{
		public const string DefaultPlayerNick = "jugador_1";

		#region Guardar
		/// <summary>
		/// Guarda o actualiza en uno el número de jugadas del jugador por defecto
		/// </summary>
		/// <returns>bool</returns>
		public async Task<bool> GuardarAsync()
		{
			try
			{
				var jugador = await ObtenerOCrearJugador(DefaultPlayerNick);
				
				jugador.CantidadJugadas++;

				await SQLiteAsyncConnection.InsertOrReplaceWithChildrenAsync(jugador, recursive: true).ConfigureAwait(false);
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Guarda o actualiza en el número enviado como argumento la cantidad de jugadas del jugador por defecto
		/// </summary>
		/// <returns>bool</returns>
		/// <param name="cantidadJugadas">número de jugadas del jugador por defecto</param>
		public async Task<bool> GuardarAsync(int cantidadJugadas)
		{
			try
			{
				//Insert que tiene en cuenta las relaciones
				var jugador = await ObtenerOCrearJugador(DefaultPlayerNick);
				jugador.CantidadJugadas = cantidadJugadas;

				await SQLiteAsyncConnection.InsertOrReplaceWithChildrenAsync(jugador, recursive: true).ConfigureAwait(false);
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Guarda o actualiza en uno el número de jugadas del jugador cuyo nick es enviado como argumento
		/// </summary>
		/// <returns>bool</returns>
		/// <param name="nickName">nick del jugador</param>
		public async Task<bool> GuardarAsync(string nickName)
		{
			try
			{
				//Insert que tiene en cuenta las relaciones
				var jugador = await ObtenerOCrearJugador(nickName);
				jugador.CantidadJugadas ++;

				await SQLiteAsyncConnection.InsertOrReplaceWithChildrenAsync(jugador, recursive: true).ConfigureAwait(false);
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Guarda o actualiza en el número enviado como argumento la cantidad de jugadas del jugador cuyo nick es también enviado como argumento
		/// </summary>
 		/// <returns>bool</returns>
		/// <param name="cantidadJugadas">número de jugadas</param>
		/// <param name="nickName">nick del jugador</param>
		public async Task<bool> GuardarAsync(string nickName,int cantidadJugadas)
		{
			try
			{
				//Insert que tiene en cuenta las relaciones
				var jugador = await ObtenerOCrearJugador(nickName);
				jugador.CantidadJugadas = cantidadJugadas;

				await SQLiteAsyncConnection.InsertOrReplaceWithChildrenAsync(jugador, recursive: true).ConfigureAwait(false);
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}
		#endregion
		#region Obtener
		/// <summary>
		/// Obtiene el jugador con nick igual al valor enviado como argumento
		/// </summary>
		/// <returns>Jugador</returns>
		/// <param name="nickName">nick del jugador</param>
		public async Task<Jugador> ObtenerJugadorPorNickName(string nickName)
		{
			var jugadores = await SQLiteAsyncConnection.GetAllWithChildrenAsync<Jugador>().ConfigureAwait(false);

			return jugadores.FirstOrDefault(x => x.Nickname == nickName);
		}

		/// <summary>
		/// Obtiene el listado de todos los jugadores registrados
		/// </summary>
		/// <returns>List jugadores</returns>
		public async Task<List<Jugador>> ObtenerJugadores()
		{
			var jugadores = await SQLiteAsyncConnection.GetAllWithChildrenAsync<Jugador>().ConfigureAwait(false);
			var jugadoresOrdenados = from jugador in jugadores
									 orderby jugador.CantidadJugadas descending
									  select jugador;
			return jugadoresOrdenados.ToList();
		}
		#endregion
		private async Task<Jugador> ObtenerOCrearJugador(string nickName)
		{
			var jugador = await ObtenerJugadorPorNickName(nickName);
			if (jugador == null)
			{
				jugador = new Jugador
				{
					Nickname = nickName
				};
			}

			return jugador;
		}
	}
}
