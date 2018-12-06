using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("stt")] 
     public class stt 
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value;
                     }
          } 

          [DbColumn("STT")] 
          public string STT 
          { 
               get{return _stt;} 
               set{ 
                      _stt=value;
                     }
          } 

          [DbColumn("Shiper")] 
          public string Shiper 
          { 
               get{return _shiper;} 
               set{ 
                      _shiper=value;
                     }
          } 

          [DbColumn("Reciever")] 
          public string Reciever 
          { 
               get{return _reciever;} 
               set{ 
                      _reciever=value;
                     }
          } 

          [DbColumn("WeightType")] 
          public string WeightType 
          { 
               get{return _weighttype;} 
               set{ 
                      _weighttype=value;
                     }
          } 

          [DbColumn("Pcs")] 
          public int Pcs 
          { 
               get{return _pcs;} 
               set{ 
                      _pcs=value;
                     }
          } 

          [DbColumn("WeightValue")] 
          public double WeightValue 
          { 
               get{return _weightvalue;} 
               set{ 
                      _weightvalue=value;
                     }
          } 

          [DbColumn("ManifestId")] 
          public int ManifestId 
          { 
               get{return _manifestid;} 
               set{ 
                      _manifestid=value;
                     }
          }

        [DbColumn("CityId")]
        public int CityId { get; set; }

        private int  _id;
           private string  _stt;
           private string  _shiper;
           private string  _reciever;
           private string  _weighttype;
           private int  _pcs;
           private double  _weightvalue;
           private int  _manifestid;
      }
}


