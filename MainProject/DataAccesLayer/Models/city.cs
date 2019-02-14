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

        

        private int  _id;
           private string  _name;
           private string  _code;
         
      }
}


