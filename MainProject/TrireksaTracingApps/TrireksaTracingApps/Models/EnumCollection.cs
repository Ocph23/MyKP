using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaTracingApps.Models
{
   public enum PortType
    {
        Air,Sea,Land
    }

    public enum WeightType
    {
        Weight,WeightVolume, Volume
    }

    public enum InvoiceStatus
    {
        New, Pending, Paid, Cancel
    }
}
