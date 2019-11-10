using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
	public interface IRepositoryBase
	{
		SQLiteAsyncConnection SQLiteAsyncConnection { get; set; }
	}
}
