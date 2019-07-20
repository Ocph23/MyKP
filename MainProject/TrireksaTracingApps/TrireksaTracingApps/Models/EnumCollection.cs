using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaTracingApps.Models
{
   public enum PortType
    {
        Udara,Laut,Darat
    }

    public enum WeightType
    {
        Berat,DimensiBerat,Dimensi
    }

    public enum InvoiceStatus
    {
        New, Pending, Paid, Cancel
    }
}
