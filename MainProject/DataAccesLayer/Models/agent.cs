using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("agent")] 
     public class agent 
   {
          [PrimaryKey("AgentId")] 
          [DbColumn("AgentId")] 
          public int AgentId 
          { 
               get{return _agentid;} 
               set{ 
                      _agentid=value;
                     }
          } 

          [DbColumn("Name")] 
          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value;
                     }
          } 

          [DbColumn("NPWP")] 
          public string NPWP 
          { 
               get{return _npwp;} 
               set{ 
                      _npwp=value;
                     }
          } 

          [DbColumn("Address")] 
          public string Address 
          { 
               get{return _address;} 
               set{ 
                      _address=value;
                     }
          } 

          [DbColumn("ContactPerson")] 
          public string ContactPerson 
          { 
               get{return _contactperson;} 
               set{ 
                      _contactperson=value;
                     }
          } 

          [DbColumn("Telepon")] 
          public string Telepon 
          { 
               get{return _telepon;} 
               set{ 
                      _telepon=value;
                     }
          } 

          [DbColumn("Handphone")] 
          public string Handphone 
          { 
               get{return _handphone;} 
               set{ 
                      _handphone=value;
                     }
          } 

          [DbColumn("Handphone2")] 
          public string Handphone2 
          { 
               get{return _handphone2;} 
               set{ 
                      _handphone2=value;
                     }
          } 

          [DbColumn("Code")] 
          public string Code 
          { 
               get{return _code;} 
               set{ 
                      _code=value;
                     }
          } 

          private int  _agentid;
           private string  _name;
           private string  _npwp;
           private string  _address;
           private string  _contactperson;
           private string  _telepon;
           private string  _handphone;
           private string  _handphone2;
           private string  _code;
      }
}


