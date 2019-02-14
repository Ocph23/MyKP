using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.UnitOfWork
{
   public class UOWAgentDashboard
    {
        private  int AgentId;

        public UOWAgentDashboard(int agentId)
        {
            this.AgentId = agentId;
        }


        public int InvoiceCount()
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    return db.Invoices.Where(O => O.AgentId == AgentId).Count();
                }
            }
            catch (Exception )
            {

                return 0;
            }
        }

        public int ManifestCount()
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    return db.Manifests.Where(O => O.AgentId == AgentId).Count();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

          


        public object STTNotHaveStatus()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var command = db.CreateCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "SttNotHaveStatus";
                    command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("id", AgentId));
                    var result = command.ExecuteReader();
                    var datas = Ocph.DAL.Mapping.MySql.MappingProperties<STTStatus>.MappingTable(result);

                    result.Close();
                        command.CommandText = "AgentSTTCount";
                    var result2 = command.ExecuteScalar();
                    return new  { Count = result2, Datas = datas };
                  

                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public object InvoiceDeadLine()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    DateTime date = DateTime.Now;
                    var invoices = (from a in db.Invoices.Where(O=>O.AgentId==AgentId && O.DeadLine<=date)
                                    join b in db.Agents.Select() on a.AgentId equals b.AgentId
                                    select new Invoice
                                    {
                                        AgentId = a.AgentId,
                                        CreatedDate = a.CreatedDate,
                                        DeadLine = a.DeadLine,
                                        Id = a.Id,
                                        Number = a.Number,
                                        PetugasId = a.PetugasId,
                                        SendInvoiceCost = a.SendInvoiceCost,
                                        Tax = a.Tax,
                                        Agent = b
                                    }).ToList();

                    foreach (var item in invoices)
                    {
                        item.Items = (from a in db.InvoiceItems.Where(O => O.InvoiceId == item.Id)
                                      join b in db.STT.Select() on a.STTId equals b.Id
                                      select new invoiceitem { Id = a.Id, InvoiceId = a.InvoiceId, OtherCosts = a.OtherCosts, Price = a.Price, STTId = a.STTId, STT = b }).ToList();

                        item.Payments = db.Payments.Where(O => O.InvoiceId == item.Id).ToList();
                    }
                    return invoices.Where(O=>O.Status!= InvoiceStatus.Lunas).OrderByDescending(O => O.Number);
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }


    }
}
