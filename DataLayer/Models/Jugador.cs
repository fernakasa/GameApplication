using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Jugador
	{
		[PrimaryKey,AutoIncrement]
		public int JugadorId { get; set; }
		public string Nickname { get; set; }
		public int CantidadJugadas { get; set; }

		public Jugador() { }
	}
}
