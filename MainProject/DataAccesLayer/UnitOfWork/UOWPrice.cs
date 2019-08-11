using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
   public class UOWPrice
    {
        public List<price> GetPrice(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = from a in db.Prices.Where(O => O.AgentId == id)
                                 join b in db.Cities.Select() on a.CityId equals b.Id
                                 select new price { Id = a.Id, AgentId = a.AgentId, CityId = a.CityId, PortType = a.PortType, PriceValue = a.PriceValue, City = b };

                    if (result != null)
                        return result.ToList();
                    throw new SystemException("Data Agent Tidak Ditemukan");

                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public List<price> Get()
        {
            throw new NotImplementedException();
        }

        public price post(price value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var dataExist = db.Prices.Where(x => x.AgentId == value.AgentId && x.PortType == value.PortType).FirstOrDefault();
                    if(dataExist!=null)
                        throw new SystemException($"Data Tarif ke {value.City.Name} via {value.PortType} Sudah ada");

                    value.Id = db.Prices.InsertAndGetLastID(value);
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

        public price GetById(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Prices.Where(O => O.Id == id).FirstOrDefault();
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

        public price Update(price value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var saved = db.Prices.Update(O => new { O.PortType, O.PriceValue, O.CityId },
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
                    var saved = db.Prices.Delete(O => O.Id == id);
                    if (saved)
                        return true;
                    throw new SystemException("Data Tidak Berhasil Dihapus");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }
    }
}
