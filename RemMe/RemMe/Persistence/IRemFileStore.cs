using RemMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemMe.Persistence {
    /// <summary>
    /// Interface for SQLiteRemFileStore
    /// </summary>
    public interface IRemFileStore {
        Task<IEnumerable<RemFile>> GetRemFilesAsync();
        Task<RemFile> GetRemFile(int id);
        Task AddRemFile(RemFile remFile);
        Task UpdateRemFile(RemFile remFile);
        Task DeleteRemFile(RemFile remFile);
    }
}
