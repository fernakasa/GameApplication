using DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
	public interface IGameRepository
	{
		Task<bool> GuardarAsync();
		Task<bool> GuardarAsync(int cantidadJugadas);
		Task<bool> GuardarAsync(string nickName);
		Task<bool> GuardarAsync(string nickName, int cantidadJugadas);
		Task<Jugador> ObtenerJugadorPorNickName(string nickName);
		Task<List<Jugador>> ObtenerJugadores();
	}
}