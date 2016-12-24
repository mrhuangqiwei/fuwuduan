using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.bean
{
    public class Tjxj
    {
     public string tjbh{ get; set; }
     public string ksbm { get; set; }
     public string xklr { get; set; }
     public string ksmc { get; set; }
    
    
    }
    public class TjxjList
    {
        public List<Tjxj> GetTjxj { get; set; }
    }
}