using RemMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemMe.ViewModels {
    public interface IRemFileStore {
        Task<IEnumerable<RemFile>> GetRemFileAsync();
        Task<RemFile> GetRemFile(int id);
        Task AddRemFile(RemFile remFile);
        Task UpdateRemFile(RemFile remFile);
        Task DeleteRemFile(RemFile remFile);
    }
}
