using SQLite;

namespace RemMe.Persistence {

    /// <summary>
    /// ISQLiteDb interface used for android and ios implementation of SQLiteDb
    /// See "Persistence" folders
    /// </summary>
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}

