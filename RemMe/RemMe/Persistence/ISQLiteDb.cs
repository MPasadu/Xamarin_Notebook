using SQLite;

namespace RemMe.Persistence {
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

