using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrireksaTracingApps.Models;

namespace TrireksaTracingApps.Services
{
    public interface IManifestService<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<stt> FindSTT(int agentId, string stt);
        Task<stt> FindSTT(string stt);
    }
}
