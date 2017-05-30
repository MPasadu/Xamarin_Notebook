using SQLite;
using RemMe.Persistence;

namespace RemMe.Tests {
    public class SQLiteDb : ISQLiteDb {
        public SQLiteAsyncConnection GetConnection()
        {
            return new SQLiteAsyncConnection(":memory:");
        }
    }
}

