using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
   public class UOWStatus
    {
        public List<status> Get()
        {
            throw new NotImplementedException();
        }

        public status post(status value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    value.Id = db.Statuses.InsertAndGetLastID(value);
                    if (value.Id > 0)
                        return value;
                    throw new SystemException(" Agent Tidak Berhasil Ditambah");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public status GetById(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Statuses.Where(O => O.STTId == id).FirstOrDefault();
                    if (result != null)
                        return result;
                    throw new SystemException("Data Agent Tidak Ditemukan");


                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public status Update(status value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var saved = db.Statuses.Update(O => new { O.CourierId,O.RecieveCondition,O.RecieveDate,O.RecieverName,O.STTId},
                        value, O => O.Id == value.Id);
                    if (saved)
                        return value;
                    throw new SystemException("Data Agent Tidak Berhasil Diubah");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var saved = db.Statuses.Delete(O => O.Id == id);
                    if (saved)
                        return true;
                    throw new SystemException("Data Agent Tidak Berhasil Diubah");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }
    }
}
