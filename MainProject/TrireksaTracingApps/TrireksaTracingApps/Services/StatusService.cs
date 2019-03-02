using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrireksaTracingApps.Models;

namespace TrireksaTracingApps.Services
{
    public class StatusService : IDataStore<stt>
    {
        public async Task<bool> AddItemAsync(stt item)
        {
            try
            {
                using (var res = new RestServices())
                {
                    item.Status.RecieveDate = DateTime.Now;
                    item.Status.STTId = item.Id;
                    var result = await res.Post<status>("api/status", item.Status);
                    if (result != null)
                        return true;
                    throw new SystemException("data tidak tersimpan");
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
                return false;
            }
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<stt> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<stt>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(stt item)
        {
            throw new NotImplementedException();
        }
    }
}
