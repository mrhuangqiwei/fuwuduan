using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.bean
{
    public class Tjjg
    {
     public string ysxm{ get; set; }
     public string ksmc { get; set; }
     public string zhmc { get; set; }
     public TjzbjgList tjzbjg { get; set; }
    
    
    }
    public class TjjgList
    {
        public List<Tjjg> GetTjjg { get; set; }
    }
}