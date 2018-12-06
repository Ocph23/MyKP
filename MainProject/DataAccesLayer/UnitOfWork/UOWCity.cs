using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
  public  class UOWCity
    {
        public List<city> Get()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Cities.Select().ToList();
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

        public city post(city value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    value.Id = db.Cities.InsertAndGetLastID(value);
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

        public city GetById(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Cities.Where(O => O.Id == id).FirstOrDefault();
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

        public city Update(city value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var saved = db.Cities.Update(O => new {O.Code,O.Name},
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
                    var saved = db.Cities.Delete(O =>  O.Id == id);
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
