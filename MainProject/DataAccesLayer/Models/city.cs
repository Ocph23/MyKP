using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("city")] 
     public class city   
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

          [DbColumn("Name")] 
          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value;
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

          [DbColumn("ShiperAddress")] 
          public string ShiperAddress 
          { 
               get{return _shiperaddress;} 
               set{ 
                      _shiperaddress=value;
                     }
          } 

          [DbColumn("RecieverAddress")] 
          public string RecieverAddress 
          { 
               get{return _recieveraddress;} 
               set{ 
                      _recieveraddress=value;
                     }
          } 

          [DbColumn("DestinationId")] 
          public int DestinationId 
          { 
               get{return _destinationid;} 
               set{ 
                      _destinationid=value;
                     }
          } 

          private int  _id;
           private string  _name;
           private string  _code;
           private string  _shiperaddress;
           private string  _recieveraddress;
           private int  _destinationid;
      }
}


