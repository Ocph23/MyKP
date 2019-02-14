using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
   public class UOWAgent
    {
        public List<agent> Get()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Agents.Select().ToList();
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

        public agent post(agent value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    value.AgentId = db.Agents.InsertAndGetLastID(value);
                    if (value.AgentId>0)
                        return value;
                    throw new SystemException(" Agent Tidak Berhasil Ditambah");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public agent GetById(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Agents.Where(O=>O.AgentId==id).FirstOrDefault();
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

        public agent Update(agent value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var saved = db.Agents.Update(O => new { O.Address, O.Code, O.ContactPerson, O.Handphone, O.Handphone2, O.Name, O.NPWP, O.Telepon },
                        value, O => O.AgentId == value.AgentId);
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
            throw new SystemException("Data Tidak Dizinkan Untuk Dihapus");
        }

        public object AgentUsers(int agentId)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.AgentAdmins.Where(O=>O.AgentId==agentId).ToList();
                    if (result != null && result.Count>0)
                        return result;
                    throw new SystemException("Data  Tidak Ditemukan");

                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }


        public object AddNewUser(int agentId, agentadmin value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    value.AgentId = agentId;
                    value.Id = db.AgentAdmins.InsertAndGetLastID(value);
                    if (value.Id > 0)
                        return value;
                    throw new SystemException(" Admin Agent Tidak Berhasil Ditambah");
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public object ChangeActive(int agentId, agentadmin value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    value.Status = !value.Status;
                    var saved = db.AgentAdmins.Update(O => new { O.Status }, value, O => O.AgentId == value.AgentId);
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

        public object UpdateProfile(int agentId, agentadmin value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var saved = db.AgentAdmins.Update(O => new { O.Name,O.Email, O.Address, O.Telepon, O.Photo }, value, O => O.Id == value.Id);
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
    }
}
