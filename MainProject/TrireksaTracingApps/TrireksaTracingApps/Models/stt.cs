using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
 
 namespace TrireksaTracingApps.Models 
{ 
     public class stt 
   {
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value;
                     }
          } 

          public string STT 
          { 
               get{return _stt;} 
               set{ 
                      _stt=value;
                     }
          } 

          public string Shiper 
          { 
               get{return _shiper;} 
               set{ 
                      _shiper=value;
                     }
          } 

          public string Reciever 
          { 
               get{return _reciever;} 
               set{ 
                      _reciever=value;
                     }
          }

        public string ShiperAddress
        {
            get { return _shiperAddress; }
            set
            {
                _shiperAddress = value;
            }
        }

        public string RecieverAddress
        {
            get { return _recieverAddress; }
            set
            {
                _recieverAddress = value;
            }
        }



        [JsonConverter(typeof(StringEnumConverter))]
          public WeightType WeightType 
          { 
               get{return _weighttype;} 
               set{ 
                      _weighttype=value;
                     }
          } 

          public int Pcs 
          { 
               get{return _pcs;} 
               set{ 
                      _pcs=value;
                     }
          } 

          public double WeightValue 
          { 
               get{return _weightvalue;} 
               set{ 
                      _weightvalue=value;
                     }
          } 

          public int ManifestId 
          { 
               get{return _manifestid;} 
               set{ 
                      _manifestid=value;
                     }
          }


        [JsonConverter(typeof(StringEnumConverter))]
        public PortType ShippingBy { get; set; }

        public int CityId { get; set; }



        public double PriceValue { get; set; }
        public status Status { get; set; }
        public manifest Manifest { get; set; }
        public city City { get; set; }

        private int  _id;
           private string  _stt;
           private string  _shiper;
           private string  _reciever;
           private WeightType _weighttype;
           private int  _pcs;
           private double  _weightvalue;
           private int  _manifestid;
        private string _shiperAddress;
        private string _recieverAddress;
    }
}


