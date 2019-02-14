using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace TrireksaTracingApps.Models 
{ 
     public class city   
   {
        
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value;
                     }
          } 

          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value;
                     }
          } 

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


