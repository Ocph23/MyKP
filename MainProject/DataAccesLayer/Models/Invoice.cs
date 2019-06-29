using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    [TableName("invoice")]
    public class Invoice 
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id { get; set; }

        [DbColumn("AgentId")]
        public int AgentId { get; set; }

        [DbColumn("Number")]
        public int Number { get; set; }

        [DbColumn("DeadLine")]
        public DateTime DeadLine { get; set; }

        [DbColumn("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [DbColumn("PetugasId")]
        public int PetugasId { get; set; }

        [DbColumn("SendInvoiceCost")]
        public double SendInvoiceCost { get; set; }

        [DbColumn("Tax")]
        public double Tax { get; set; }

        public List<invoiceitem> Items { get; set; }

        public double GrandTotal => TotalCostItem + SendInvoiceCost;

        public double TotalCostItem => CalculateItem();

        public agent Agent { get;  set; }

        public string NumberView => string.Format("{0:D5}/INV-TRP/{1}",Number,CreatedDate.Year);

        public IEnumerable<pembayaran> Payments { get; internal set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public InvoiceStatus Status { get {
                if (Payments != null)
                {
                    double total = Payments.Sum(O => O.AmountPaid);
                    if (total <= 0)
                        return InvoiceStatus.Baru;
                    if (total > 0 && total < GrandTotal)
                        return InvoiceStatus.Panjar;
                    if (total == GrandTotal)
                        return InvoiceStatus.Lunas;

                }
                return InvoiceStatus.Baru;
            } }


        private double CalculateItem()
        {
            if(Items!=null  && Items.Count>0)
            {
               return Items.Sum(O => O.Costs);
            }
            return 0;
        }



    }

}
