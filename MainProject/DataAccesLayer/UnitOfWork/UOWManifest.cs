using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
                    var result = from a in db.Manifests.Where(O => O.AgentId == agentId)
                                 join b in db.Agents.Select() on a.AgentId equals b.AgentId
                                 join c in db.AgentAdmins.Select() on a.AgentAdminId equals c.Id
                                 select new manifest { AgentAdminId=a.AgentAdminId, AgentId=a.AgentId,
                                     CreateDate =a.CreateDate, DetailInformation=a.DetailInformation,
                                  Id=a.Id, Number=a.Number, Package=a.Package, PortType=a.PortType,
                                     RecieveOnPort =a.RecieveOnPort, SendedDate=a.SendedDate,User=c, Agent=b};
                    
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public manifest GetManifestById(int id)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var item= db.Manifests.Where(O => O.Id == id).FirstOrDefault();
                    if(item!=null)
                    {
                        item.Items = (from b in db.STT.Where(O => O.ManifestId == item.Id)
                                     join c in db.Cities.Select() on b.CityId equals c.Id
                                     select new stt
                                     {
                                         CityId = b.CityId,
                                         Id = b.Id,
                                         ManifestId = b.ManifestId,
                                         Pcs = b.Pcs,
                                         Reciever = b.Reciever,
                                         RecieverAddress = b.RecieverAddress,
                                         Shiper = b.Shiper,
                                         ShiperAddress = b.ShiperAddress,
                                         STT = b.STT,
                                         WeightType = b.WeightType,
                                         WeightValue = b.WeightValue,   ShippingBy=b.ShippingBy, 
                                         City = c
                                     }).ToList();

                        item.User = db.AgentAdmins.Where(O => O.AgentId == item.AgentId).FirstOrDefault();
                        foreach(var data in item.Items)
                        {
                            data.Status = db.Statuses.Where(O => O.STTId == data.Id).FirstOrDefault();
                        }
                        return item;
                    }

                    throw new SystemException("Manifest Tidak Ditemukan");
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

        public object UpdateManifest(manifest value)
        {
            using (var db=new OcphDbContext())
            {
                try
                {
                    var updated = db.Manifests.Update(O => new { O.DetailInformation, O.RecieveOnPort, O.Number, O.Package, O.PortType, O.SendedDate }, value, O => O.Id == value.Id);
                    if (updated)
                        return value;
                    throw new SystemException("Data Tidak Tersimpan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public object GetNewManifest()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var command = db.CreateCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "newmanifest";
                    var result = command.ExecuteReader();
                    DataTable dataTable =  new DataTable();
                    dataTable.Load(result);
                    //  var datas = Ocph.DAL.Mapping.MySql.MappingProperties<manifest>.MappingTable(result);
                    var datas = Helper.DataTableToJsonArray(dataTable);
                    return datas;

                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
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

        public manifest ManifestByAgentId(int agentid, int manifestNumber)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var manifestData = db.Manifests.Where(O => O.AgentId == agentid && O.Number == manifestNumber).FirstOrDefault();
                    if (manifestData != null)
                    {
                        var result = (from b in db.STT.Where(O=>O.ManifestId== manifestData.Id)
                             join c in db.Cities.Select() on b.CityId equals c.Id
                             select new stt
                             {
                                 CityId = b.CityId,
                                 Id = b.Id,
                                 ManifestId = b.ManifestId,
                                 Pcs = b.Pcs,
                                 Reciever = b.Reciever,
                                 RecieverAddress = b.RecieverAddress,
                                 Shiper = b.Shiper,
                                 ShiperAddress = b.ShiperAddress,
                                 STT = b.STT,
                                 WeightType = b.WeightType,
                                 WeightValue = b.WeightValue,   ShippingBy=b.ShippingBy,
                                 City = c
                             }).ToList();


                        

                        foreach (var item in result)
                        {
                            item.Status = (from a in db.Statuses.Where(O => O.STTId == item.Id)
                                           join b in db.Workers.Select() on a.CourierId equals b.Id
                                           select new status
                                           {
                                               CourierId = a.CourierId,
                                               Id = a.Id,  Sign=a.Sign,
                                               RecieveCondition = a.RecieveCondition,
                                               RecieveDate = a.RecieveDate,
                                               RecieverName = a.RecieverName,
                                               STTId = a.STTId,
                                               Courier = b
                                           }

                                           ).FirstOrDefault();
                        }


                      manifestData.Items = result.ToList();
                        return manifestData;

                    }
                   
                    throw new SystemException("Data Tidak Ditemukan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public stt UpdateItem(int id, stt item)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var saved = db.STT.Update(O => new { O.Pcs, O.Reciever,  O.RecieverAddress,O.ShippingBy, O.Shiper, O.ShiperAddress, O.WeightType, O.WeightValue,O.CityId }, item, O => O.Id == item.Id);
                    if (saved)
                        return item;
                    throw new SystemException("Data Tidak Tersimpan");

                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public List<stt> Find(int agentId, string stt)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = from a in db.Manifests.Where(O => O.AgentId == agentId)
                                 join b in db.STT.Select() on a.Id equals b.ManifestId
                                 join c in db.Cities.Select() on b.CityId equals c.Id
                                 select new stt { CityId=b.CityId, ShippingBy=b.ShippingBy, Id=b.Id, ManifestId=b.ManifestId, Pcs=b.Pcs, Reciever=b.Reciever, RecieverAddress=b.RecieverAddress,
                                  Shiper=b.Shiper, ShiperAddress=b.ShiperAddress, STT=b.STT, WeightType=b.WeightType, WeightValue=b.WeightValue,Manifest=a, City=c};

                    var datas = result.Where(O => O.STT.Contains(stt)).ToList();
                    foreach (var item in datas)
                    {
                        item.Status = (from a in  db.Statuses.Where(O => O.STTId == item.Id)
                                        join b in db.Workers.Select() on a.CourierId equals b.Id
                                        select new status { CourierId=a.CourierId, Id=a.Id, Sign=a.Sign, RecieveCondition=a.RecieveCondition,
                                            RecieveDate =a.RecieveDate,  RecieverName=a.RecieverName, STTId=a.STTId , Courier=b}
                                       
                                       ).FirstOrDefault();
                    }


                    var dataResult= datas.ToList();
                    if (dataResult.Count > 0)
                        return dataResult;
                    throw new SystemException("Data Tidak Ditemukan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public stt FindBySTT(string stt)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = from b in db.STT.Where(O=>O.STT==stt)
                                 join c in db.Cities.Select() on b.CityId equals c.Id
                                 join d in db.Manifests.Select() on b.ManifestId equals d.Id
                                 select new stt
                                 {
                                     CityId = b.CityId,
                                     ShippingBy = b.ShippingBy,
                                     Id = b.Id,
                                     ManifestId = b.ManifestId,
                                     Pcs = b.Pcs,
                                     Reciever = b.Reciever,
                                     RecieverAddress = b.RecieverAddress,
                                     Shiper = b.Shiper,
                                     ShiperAddress = b.ShiperAddress,
                                     STT = b.STT,
                                     WeightType = b.WeightType,
                                     WeightValue = b.WeightValue,
                                     Manifest=d,
                                     City = c
                                 };

                    var datas = result.Where(O => O.STT.Contains(stt)).ToList();
                    foreach (var item in datas)
                    {
                        item.Status = (from a in db.Statuses.Where(O => O.STTId == item.Id)
                                       join b in db.Workers.Select() on a.CourierId equals b.Id
                                       select new status
                                       {
                                           CourierId = a.CourierId,
                                           Id = a.Id,
                                           Sign = a.Sign,
                                           RecieveCondition = a.RecieveCondition,
                                           RecieveDate = a.RecieveDate,
                                           RecieverName = a.RecieverName,
                                           STTId = a.STTId,
                                           Courier = b
                                       }

                                       ).FirstOrDefault();
                    }


                    var dataResult = datas.ToList();
                    if (dataResult.Count > 0)
                        return dataResult.FirstOrDefault();
                    throw new SystemException("Data Tidak Ditemukan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public bool DeleteItem(int sttId)
        {
            try
            {
                using (var db = new OcphDbContext())
                {

                   var deleted = db.STT.Delete(O=>O.Id==sttId);
                    if (!deleted)
                        throw new SystemException("Data tidak berhasil dihapus");
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
