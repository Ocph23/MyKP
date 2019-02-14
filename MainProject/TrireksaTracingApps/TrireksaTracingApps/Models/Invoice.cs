using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaTracingApps.Models
{
    public class Invoice 
    {
       
        public int Id { get; set; }

        public int AgentId { get; set; }

        public int Number { get; set; }

        public string Note { get; set; }

        public DateTime DeadLine { get; set; }

        public DateTime CreatedDate { get; set; }

        public int PetugasId { get; set; }

        public double SendInvoiceCost { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string PaymentNote { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public InvoiceStatus Status { get; set; }


        public double Tax { get; set; }

        public List<invoiceitem> Items { get; set; }

        public double GrandTotal => TotalCostItem + SendInvoiceCost;

        public double TotalCostItem => CalculateItem();

        public agent Agent { get;  set; }

        public string NumberView => string.Format("{0:D5}/INV-TRP/{1}",Number,CreatedDate.Year);

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
