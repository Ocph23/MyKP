using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
   public class UOWUserProfile
    {
       public agent GetAgentProfile(int agentId)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Agents.Where(O => O.AgentId == agentId).FirstOrDefault();
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


        public agentadmin GetUserProfile(string userid)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.AgentAdmins.Where(O => O.UserId == userid).FirstOrDefault();
                    if (result != null)
                        return result;
                    throw new SystemException("Profile User Tidak Ditemukan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }


        public petugas GetAdminProfile(string userid)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Workers.Where(O => O.UserId == userid).FirstOrDefault();
                    if (result != null)
                        return result;
                    throw new SystemException("Profile User Tidak Ditemukan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public object GetPetugas()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                   var data  = db.Workers.Select().ToList();
                    if (data!=null)
                        return data;
                    throw new SystemException("Data Tidak Ditemukan");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public petugas UpdatePetugas(petugas item)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var saved= db.Workers.Update(O=> new {O.Address, O.Handphone,O.Name } ,item,O=>O.Id==item.Id);
                    if (saved)
                        return item;
                    throw new SystemException("Tidak Berhasil Tambah Petugas");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public petugas AddNewUser(petugas item)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    item.Id = db.Workers.InsertAndGetLastID(item);
                    if (item.Id >0)
                        return item;
                    throw new SystemException("Tidak Berhasil Tambah Petugas");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }
    }
}
