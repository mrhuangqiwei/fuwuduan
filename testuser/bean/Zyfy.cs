using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.bean
{
    public class Zyfy
    {
     public string zyh{ get; set; }
     public string ryrq { get; set; }
     public string cyrq { get; set; }
     public string rycwid { get; set; }
     public string cyks { get; set; }
     public string ryks { get; set; }
     public string ksmc { get; set; }
     public string brxm { get; set; }
     public string brnl { get; set; }
     public string brnldw { get; set; }
     public string brxb { get; set; }
     public string jtzz { get; set; }
     public string fbmc { get; set; }
     public string fyje{ get; set; }
     public string yjje { get; set; }
   }
    public class ZyfyList
    {
        public List<Zyfy> GetZyfy { get; set; }
    }
}