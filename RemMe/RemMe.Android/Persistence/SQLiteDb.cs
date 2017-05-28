using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using RemMe.Droid;
using RemMe.Persistence;

[assembly: Dependency(typeof(SQLiteDb))]

namespace RemMe.Droid
{
	public class SQLiteDb : ISQLiteDb
	{
		public SQLiteAsyncConnection GetConnection()
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "RemMeSQLite.db3");

			return new SQLiteAsyncConnection(path);
		}
	}
}

