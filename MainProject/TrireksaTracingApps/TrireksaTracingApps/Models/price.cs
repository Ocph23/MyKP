using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
 
 namespace TrireksaTracingApps.Models 
{ 
     public class price
   {
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value;
                     }
          } 

          public int AgentId 
          { 
               get{return _agentid;} 
               set{ 
                      _agentid=value;
                     }
          } 

          public int CityId 
          { 
               get{return _cityid;} 
               set{ 
                      _cityid=value;
                     }
          } 

          public double PriceValue 
          { 
               get{return _pricevalue;} 
               set{ 
                      _pricevalue=value;
                     }
          }


        [JsonConverter(typeof(StringEnumConverter))]
          public PortType PortType 
          { 
               get{return _porttype;} 
               set{ 
                      _porttype=value;
                     }
          }

        public city City { get; set; }

        private int  _id;
           private int  _agentid;
           private int  _cityid;
           private double  _pricevalue;
           private PortType _porttype;
      }
}


