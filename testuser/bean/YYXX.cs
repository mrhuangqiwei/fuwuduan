using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/**功能：病人预约信息
 *  开发时间：2016-9-1
 *  开发人：黄启位
 * **/
namespace testuser.bean
{
    public class YYXX
    { public String yyghid { get; set; }
        public String brid { get; set; }
        public String yyghrq { get; set; }
        public String brxm { get; set; }
        public String brxb { get; set; }
        public String brnl { get; set; }
        public String brnldw { get; set; }
        public String jtzz { get; set; }
        public String mzsbdd { get; set; }
        public String zcmc { get; set; }
        public String ysxm { get; set; }
        public String ksmc { get; set; }
    }
    public class YYXXList {
        public List<YYXX> GetYyxx { get; set; }
    }
}