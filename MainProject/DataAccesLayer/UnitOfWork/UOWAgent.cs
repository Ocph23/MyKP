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

    }
}
