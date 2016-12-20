using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.bean
{
    public class PacxReport
    {public string laybe1 { get; set; }
     public  string laybe2{ get; set; }
  }
    public class PacxReportList
    {
        public List<PacxReport> GetPacxReport { get; set; }
    }
}