using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ocph.DAL;

 
 namespace DataAccesLayer.Models 
{ 
     [TableName("manifest")] 
     public class manifest 
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

          [DbColumn("CreateDate")] 
          public DateTime CreateDate 
          { 
               get{return _createdate;} 
               set{ 
                      _createdate=value;
                     }
          } 

          [DbColumn("SendedDate")] 
          public DateTime SendedDate 
          { 
               get{return _updatedate;} 
               set{ 
                      _updatedate=value;
                     }
          } 

          [DbColumn("Number")] 
          public int Number 
          { 
               get{return _number;} 
               set{ 
                      _number=value;
                     }
          }


        [DbColumn("Package")]
        public int Package
        {
            get { return _package; }
            set
            {
                _package = value;
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [DbColumn("PortType")] 

          public PortType PortType 
          { 
               get{return _porttype;} 
               set{ 
                      _porttype=value;
                     }
          } 




          [DbColumn("DetailInformation")] 
          public string DetailInformation 
          { 
               get{return _detailinformation;} 
               set{ 
                      _detailinformation=value;
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

          [DbColumn("AgentAdminId")] 
          public int AgentAdminId 
          { 
               get{return _agentadminid;} 
               set{ 
                      _agentadminid=value;
                     }
          } 

          [DbColumn("RecieveOnPort")] 
          public DateTime? RecieveOnPort 
          { 
               get{return _recieveonport;} 
               set{ 
                      _recieveonport=value;
                     }
          }

        public List<stt> Items { get;  set; }
        public agentadmin User { get; set; }
        public agent Agent { get; internal set; }

        public string NumberView
        {
            get
            {
                if (Agent != null)
                    return string.Format("{0}/{1}-TRP/{2}", Number, Agent.Code, CreateDate.Year);
                else
                    return Number.ToString();
            }
        }


        public string AgentName { get; set; }

        private int  _id;
           private DateTime  _createdate;
           private DateTime  _updatedate;
           private int  _number;
           private PortType  _porttype;
           private string  _detailinformation;
           private int  _agentid;
           private int  _agentadminid;
           private DateTime?  _recieveonport;
        private int _package;
    }
}


