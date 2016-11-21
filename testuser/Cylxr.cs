using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser
{
    public class Cylxr
    {
        public string sfzh { get; set; }
        public string brxm { get; set; }
        public string brnl { get; set; }
        public string brxb { get; set; }
        public string brjtzz { get; set; }
        public string ph { get; set; }
        public string brdh { get; set; }
    }

    public class CylxrList
    {
        public List<Cylxr> GetCylxr { get; set; }
    }
}