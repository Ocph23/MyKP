using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
   public class UOWPenjualan
    {

        public IEnumerable<penjualan> GetByAdmin(DateTime dari, DateTime sampai)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var command = db.CreateCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "penjualan";
                    command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("dateStart", dari));
                    command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("dateEnd", sampai));

                    var result = command.ExecuteReader();
                    var datas = Ocph.DAL.Mapping.MySql.MappingProperties<penjualan>.MappingTable(result);

                    if (datas != null)
                    {
                        return datas;
                    }

                    throw new SystemException("Data Tidak Ada");

                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }
    }
}
