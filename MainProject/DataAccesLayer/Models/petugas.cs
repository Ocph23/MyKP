using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("petugas")] 
     public class petugas  
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

          [DbColumn("Address")] 
          public string Address 
          { 
               get{return _address;} 
               set{ 
                      _address=value;
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

          private int  _id;
           private string  _name;
           private string  _address;
           private string  _handphone;
      }
}


