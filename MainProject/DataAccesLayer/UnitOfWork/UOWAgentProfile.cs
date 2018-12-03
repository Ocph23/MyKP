using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
   public class UOWAgentProfile
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


        

       

    }
}
