using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("price")] 
     public class price
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

          [DbColumn("AgentId")] 
          public int AgentId 
          { 
               get{return _agentid;} 
               set{ 
                      _agentid=value;
                     }
          } 

          [DbColumn("CityId")] 
          public int CityId 
          { 
               get{return _cityid;} 
               set{ 
                      _cityid=value;
                     }
          } 

          [DbColumn("PriceValue")] 
          public double PriceValue 
          { 
               get{return _pricevalue;} 
               set{ 
                      _pricevalue=value;
                     }
          } 

          [DbColumn("PortType")] 
          public string PortType 
          { 
               get{return _porttype;} 
               set{ 
                      _porttype=value;
                     }
          } 

          private int  _id;
           private int  _agentid;
           private int  _cityid;
           private double  _pricevalue;
           private string  _porttype;
      }
}


