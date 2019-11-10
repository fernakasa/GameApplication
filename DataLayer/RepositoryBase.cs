using DataLayer.Models;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DataLayer
{
	public class RepositoryBase : IRepositoryBase
	{
        // TODO private string db_file = "videogame.db3";
        private string db_file = "videogame.sqlite";
		public SQLiteAsyncConnection SQLiteAsyncConnection { get; set; }

		public RepositoryBase()
		{
            var dbPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"db", db_file);

			SQLiteAsyncConnection = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
	
			GC.Collect();

            //En caso de ser necesario eliminar la BD descomentar la siguiente línea
            //File.Delete(dbPath);

            bool exists = File.Exists(dbPath);

			if (!exists)
			{
				CreateDataBaseAsync(SQLiteAsyncConnection).ConfigureAwait(false);
			}
		}
		private async Task CreateDataBaseAsync(SQLiteAsyncConnection conn)
		{
			try
			{
				await conn.CreateTableAsync<Jugador>().ConfigureAwait(false);
            }
			catch (SQLiteException ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
