using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Models;
using Ocph.DAL;
using Ocph.DAL.Provider.MySql;
using Ocph.DAL.Repository;

namespace DataAccesLayer
{
  public  class OcphDbContext :MySqlDbConnection
    {
        public OcphDbContext()
        {
            ConnectionString = "Server=localhost;database=dbtrireksaincoming;UID=root;password=;CharSet=utf8;Persist Security Info=True";
        }

        public IRepository<manifest> Manifests { get { return new Repository<manifest>(this); } }
        public IRepository<agent> Agents { get { return new Repository<agent>(this); } }
        public IRepository<agentadmin> AgentAdmins{ get { return new Repository<agentadmin>(this); } }
        public IRepository<city> Cities { get { return new Repository<city>(this); } }
        public IRepository<petugas> Workers { get { return new Repository<petugas>(this); } }
        public IRepository<price> Prices { get { return new Repository<price>(this); } }
        public IRepository<status> Statuses { get { return new Repository<status>(this); } }
        public IRepository<stt> STT { get { return new Repository<stt>(this); } }





    }
}
