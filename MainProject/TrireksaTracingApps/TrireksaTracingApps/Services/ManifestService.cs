using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaTracingApps.Models;

namespace TrireksaTracingApps.Services
{
    public class ManifestService : IManifestService<manifest>
    {

        public Task<bool> AddItemAsync(manifest item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<stt> FindSTT(int agentId, string stt)
        {
            try
            {
                using (var res = new RestServices())
                {
                    var result = await res.Get<List<stt>>(string.Format("api/manifest/find?id={0}&stt={1}", agentId,stt));
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
                return null;
            }
        }

        public async Task<manifest> GetItemAsync(string id)
        {
            try
            {
                using (var res = new RestServices())
                {
                    var result = await res.Get<manifest>(string.Format("api/manifest/{0}",id));
                    return result;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<manifest>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                using (var res = new RestServices())
                {
                    IEnumerable<manifest>result = await res.Get<List<manifest>>("api/manifest/newmanifest");
                    return result;
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateItemAsync(manifest item)
        {
            try
            {
                using (var res = new RestServices())
                {
                    item.RecieveOnPort = DateTime.Now;
                  var result = await res.Put<manifest>("api/manifest/"+item.Id,item);
                    if(result!=null)
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
    }
}
                                                                                                      