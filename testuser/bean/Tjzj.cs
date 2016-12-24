using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.bean
{
    public class Tjzj
    {
     public string zjlr{ get; set; }
     public string jlbm { get; set; }
     public string tjbh { get; set; }
  }
    public class TjzjList
    {
        public List<Tjzj> GetTjzj { get; set; }
    }
}