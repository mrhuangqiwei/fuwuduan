using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser.bean
{
    public class BRXX
    {public string brid { get; set; }
     public  string ghxh{ get; set; }
     public string ylkh{ get; set; }
     public string ghrq{ get; set; }
     public string sfzh{ get; set; }
     public string zylx { get; set; }

    
    }
    public class BRXXList
    {
        public List<BRXX> GetBrxx { get; set; }
    }
}