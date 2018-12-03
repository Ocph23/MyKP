using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
    public class UOWManifest
    {

       public List<manifest> GetManifest(int agentId)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var result = db.Manifests.Where(O => O.AgentId == agentId);
                    foreach(var item in result)
                    {
                        item.Items = db.STT.Where(O => O.ManifestId == item.Id).ToList();
                    }

                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }



        public manifest CreateManifest(manifest data)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    data.Id = db.Manifests.InsertAndGetLastID(data);
                    if (data.Id <= 0)
                        throw new SystemException("Data tidak tersimpan");
                    else
                        return data;
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public stt AddNewItem(int manifestId, stt item)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var result = db.Manifests.Where(O => O.Id == manifestId).FirstOrDefault();
                    if(result==null)
                    {
                        throw new SystemException("Data Manifest Tidak Ditemukan");
                    }
                    else
                    {
                        item.Id = db.STT.InsertAndGetLastID(item);
                        if (item.Id <= 0)
                            throw new SystemException("Data tidak tersimpan");
                        else
                            return item;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public bool DeleteItem(int sttId)
        {
            try
            {
                using (var db = new OcphDbContext())
                {

                   var deleted = db.STT.Delete(O=>O.Id==sttId);
                    if (deleted)
                        throw new SystemException("Data tidak tersimpan");
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }


        public bool DeleteManifest(int manifestId)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    var datas = db.STT.Where(O => O.ManifestId == manifestId).ToList();
                    if(datas!=null)
                    {
                        foreach (var item in datas)
                        {
                            var deleted = db.Manifests.Delete(O => O.Id == item.Id);
                            if (!deleted)
                            {
                                throw new SystemException("item data tidak terhapus");
                            }
                        }
                    }
                    if (!db.Manifests.Delete(O => O.Id == manifestId))
                        throw new SystemException("Manifest tidak terhapus");

                    trans.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
                
            }
           
        }


    }
}
