using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.bean
{
    public class Tjjy
    {
     public string zjjy{ get; set; }
     public string jlmc { get; set; }
     public string ysxm { get; set; }
     public string czybm { get; set; }
     public string tjbh { get; set; }
    
    }
    public class TjjyList
    {
        public List<Tjjy> GetTjjy { get; set; }
    }
}