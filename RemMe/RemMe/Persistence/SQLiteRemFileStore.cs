using RemMe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemMe.Models;
using SQLite;

namespace RemMe.Persistence {

    /// <summary>
    /// Db connection and tasks for creating, retreiving, deleting RemFiles from/to Db.
    /// </summary>
    public class SQLiteRemFileStore : IRemFileStore {

        private SQLiteAsyncConnection _connection;

        public SQLiteRemFileStore(ISQLiteDb db) {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<RemFile>();
        }

        public async Task AddRemFile(RemFile remFile) {
            await _connection.InsertAsync(remFile);
        }

        public async Task DeleteRemFile(RemFile remFile) {
            await _connection.DeleteAsync(remFile);
        }

        public async Task<RemFile> GetRemFile(int id) {
            return await _connection.FindAsync<RemFile>(id);
        }

        public async Task<IEnumerable<RemFile>> GetRemFileAsync() {
            return await _connection.Table<RemFile>().ToListAsync();
        }

        public async Task UpdateRemFile(RemFile remFile) {
            await _connection.UpdateAsync(remFile);
        }
    }
}
