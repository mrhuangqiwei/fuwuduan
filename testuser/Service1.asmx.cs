using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace testuser
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        odbc odb = new odbc();
        odbcjson odbc = new odbcjson();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod(Description = "增加一条用户信息")]
        public bool insertuserInfo(String userid, String idcard, String username, String password,String jtzz)
        {
            return odb.insertUserInfo(userid, idcard, username, password,jtzz);

        }

        [WebMethod(Description = "增加一常用就诊人信息")]
        public bool insertuserFriend(String sfzh, String brxm, String brnl, String brxb, String brjtzz, String ph, String brdh)
        {
            return odb.insertUserFriend( sfzh,  brxm,  brnl,  brxb,  brjtzz,  ph,  brdh);

        }

        [WebMethod(Description = "获取用户账号和密码")]
        public string[] getUserInfo()
        {
            return odb.getUserInfo().ToArray();
         
        }
        [WebMethod(Description = "获取所有费用的信息")]
        public string[] getUserFF(String zyh)
        {
            return odb.getUserFF(zyh).ToArray();
        }


        [WebMethod(Description = "门诊费用信息")]
        public string[] getbqcybz(String mzh)
        {
            return odb.getUserMzfy(mzh).ToArray();

        }

        [WebMethod(Description = "门诊基本信息")]
        public string[] getusermzjbxx(String ghxh)
        {
            return odb.getUserMzJbxx(ghxh).ToArray();

        }
        [WebMethod(Description = "获取门诊中药处方号")]
        public string[] getusermzzycfh(String ghxh)
        {
            return odb.getUserMzZycfh( ghxh).ToArray();

        }
        [WebMethod(Description = "获取门诊其他处方号")]
        public string[] getusermzqtcfh(String ghxh)
        {
            return odb.getUserMzQtcfh(ghxh).ToArray();

        }
        [WebMethod(Description = "获取门诊中药处方信息")]
        public string[] getusermzzycfxx(String cfh)
        {
            return odb. getUserMzZYcfxx(cfh).ToArray();

        }

        [WebMethod(Description = "获取门诊中药诊断与用法信息")]
        public string[] getusermzzyyfyzdxx(String cfh)
        {
            return odb.getUserMzZYzdyfy(cfh).ToArray();

        }
        [WebMethod(Description = "获取门诊治疗处置信息")]
        public string[] getusermzZlc(String ghxh)
        {
            return odb.getUserZlczxx(ghxh).ToArray();

        }
        [WebMethod(Description = "获取门常用就诊人信息")]
        public string[] getuserfriendinfo(string manageid)
        {
            return odb.getuserfriendinfo(manageid).ToArray();

        }

        [WebMethod(Description = "获取用户信息")]
        public string[] getUserxx(String userid)
        {
            return odb.getUserxx(userid).ToArray();

        }
        [WebMethod(Description = "获取科室信息")]
        public string[] getKsxx()
        {
            return odb.getKsxx().ToArray();

        }
        [WebMethod(Description = "获取科室医生信息")]
        public string[] getKsys(String ksbm)
        {
            return odb.getKsys(ksbm).ToArray();

        }

        [WebMethod(Description = "医生排班信息")]
        public string[] getKsyssbsj(String ksbm)
        {
            return odb.getKsyssbsj(ksbm).ToArray();

        }
        [WebMethod(Description = "业务序号表，病人ID")]
        public string[] getywxuxhbrid()
        {
            return odb.getywxuxhbrid().ToArray();

        }
        [WebMethod(Description = "就诊人id")]
        public string[] getuserfriendid(string manageid)
        {
            return odb.getuserfriendid(manageid).ToArray();

        }
        [WebMethod(Description = "更新用户表")]
        public bool updateUser(String friendid,String sfzh, String manageid)
        {
            return odb.updateUser( friendid,sfzh,  manageid);

        }
        [WebMethod(Description = "获取预约id的序号")]
        public string getywxuxhyyid(int xh1)
        {
            return odb.getywxuxhyyid( xh1);

        }

        [WebMethod(Description = "插入注册表")]
        public bool Insertbrzc(String brid, String brxm, String brxb, String brnl, String sfzh, String jtzz, String sj, String zcrq)
        {
            return odb.Insertbrzc( brid,  brxm,  brxb,  brnl,  sfzh,  jtzz,  sj,  zcrq);

        }
        [WebMethod(Description = "获取输入时间和当前时间的天数差")]
        public int getdatedifference(string date)
        {
            return odb.getdatedifference( date);

        }
        [WebMethod(Description = "获取预约挂号ID")]
        public double getyyghid()
        {
            return odb.getyyghid();

        }

         [WebMethod(Description = "插入预约表")]
        public bool Insertyygh( String yyghid,String ywckbm,String czybm,String yyghrq,String brxm,String brxb,String brsr,String brnl,String brnldw,String sfzh,String jtzz,String sj,String yyys, String yyks, String yydjrq, String yyyxrq, String czyks)
        {
            return odb.Insertyygh(yyghid, ywckbm, czybm, yyghrq, brxm, brxb, brsr,brnl, brnldw, sfzh, jtzz, sj,yyys,  yyks,  yydjrq,yyyxrq, czyks);

        }


         [WebMethod(Description = "病人预约挂号")]
         public String appointment(String yyghrq, String brxm, String brxb, String brnl, String sfzh, String jtzz, String sj, String yyys, String yyks, String yydjrq, String yyyxrq)
         {
             return odb.appointment( yyghrq, brxm, brxb,  brnl,  sfzh,  jtzz,  sj, yyys,  yyks,  yydjrq,  yyyxrq);
 
         }
        /**预约挂号**/

         [WebMethod(Description = "通过医生账号获取科室编码")]
         public String getksbmbyyszh(String czybm)
         {
             return odb.getksbmbyyszh( czybm);

         }
         [WebMethod(Description = "获取简单预约病人信息")]
         public string[] getsamplefriendinfo(string manageid)
         {
             return odb.getsamplefriendinfo(manageid).ToArray();

         }
         [WebMethod(Description = "获取预约明细医生上下班时间")]
         public string[] getyssbbyyymx(String ysbm, String yyghrq)
         {
             return odb.getyssbbyyymx(ysbm,yyghrq).ToArray();
         }


         [WebMethod(Description = "取消预约")]
         public bool deleteyyxx(String brid, String yyghid)
         {
             return odb.deleteyyxx(brid, yyghid);

         }

         [WebMethod(Description = "删除常用就诊人")]
         public bool deletefriend(String userid, String sfzh)
         {
             return odb.deletefriend( userid, sfzh);

         }

        /**测试**/
         [WebMethod(Description = "测试json")]
         public String getksys()
         {
             return odb.getksys();

         }
         /**获取科室简介JSON**/
         [WebMethod(Description = "获取科室简介JSON")]
         public String getksjj()
         {
             return odb.getksjj();

         }
         /**获取常用就诊人JSON**/
         [WebMethod(Description = "获取常用就诊人JSONJSON")]
         public String getkjzr(String phone)
         {
             return odb.getkjzr( phone);

         }

         /**获取满意度调查项目JSON**/
         [WebMethod(Description = "获取满意度调查项目JSON*")]
         public String getmyddcxm()
         {
             return odb.getmyddcxm();
         }
         /**满意度调查结果**/
         [WebMethod(Description = "插入满意度调查结果")]
         public bool insertUserMyddc(String yhid, String myddcid, String dcfs, String data1)
         {
             return odb.insertUserMyddc(yhid, myddcid, dcfs, data1);
         }
         /**满意度调查结果建议**/
         [WebMethod(Description = "插入满意度调查结果建议")]
         public bool insertUserMyddcYhjy(String yhid, String yhjy)
         {
             return odb.insertUserMyddcYhjy(yhid, yhjy);

         }
         /**获取医生排班上午JSON**/
         [WebMethod(Description = "获取医生上午排班时间返回JSON*")]
         public String yspbswjson(String ksbm)
         { return odb.yspbswjson(ksbm);}
         /**获取医生排班下午JSON**/
         [WebMethod(Description = "获取医生下午排班时间返回JSON*")]
         public String yspbxwjson(String ksbm)
         { return odb.yspbxwjson(ksbm); }


         [WebMethod(Description = "获取常用就诊人明细信息")]
         public string[] getfriendmx(String sfzh, String brxm)
         {
             return odb.getfriendmx(sfzh, brxm).ToArray();

         }
         [WebMethod(Description = "根据身份证号获取住院号")]
         public string[] getzygbysfzg(String sfzh)
         {
             return odb.getzygbysfzg(sfzh).ToArray();

         }
         [WebMethod(Description = "根据身份证号获取挂号序号")]
         public string[] getghxhbysfzg(String sfzh)
         {
             return odb.getghxhbysfzg(sfzh).ToArray();

         }
         [WebMethod(Description = "根据身份证号获取住院病人基本信息")]
         public string[] getfriendinfobysfzh(String sfzh)
         {
             return odb.getfriendinfobysfzh(sfzh).ToArray();
         }
         [WebMethod(Description = "根据身份证号获取门诊病人基本信息")]
         public string[] getmzfriendinfobysfzh(String sfzh)
         {
             return odb.getmzfriendinfobysfzh(sfzh).ToArray();
         }
         [WebMethod(Description = "hhhhhhhhhhhhhhhhhh")]
         public string[] yspbsw(int k, String ksbm)
         {
             return odb. yspbsw( k,  ksbm).ToArray();
         }


         [WebMethod(Description = "根据身份证号获取门诊病人基本信息返回JSON")]
         public string getmzfriendinfobysfzhtoJson(String sfzh)
         {
             return odb.getmzfriendinfobysfzhtoJson(sfzh);
         }
         [WebMethod(Description = "通过挂号序号或者住院号判断设备名称")]
         public string[] checkdevices(String cxh)
         {
             return odb.checkdevices(cxh).ToArray();
         }
         [WebMethod(Description = "测试11111111111111111111")]
         public string[] jcbg(String sfzh)
         {
             return odb.jcbg(sfzh).ToArray();
         }
         /**从网站预约挂号**/
         [WebMethod(Description = "从网站预约挂号")]
         public bool insertyyghfromwz(String ylkh, String jtzz, String sj, String ghys, String yysj)
         {
             return odb.insertyyghfromwz(ylkh, jtzz, sj, ghys, yysj);
         }
         /**通过手机号或者病人id获取预约信息返回JSON**/
         [WebMethod(Description = "通过手机号或者病人id获取预约信息返回JSON*")]
         public String getyyxxbysjtojson(String sj)
         { return odb.getyyxxbysjtojson( sj); }
         /**获取日期和星期**/
         [WebMethod(Description = "获取日期和星期")]
         public string[] getyyrqandxq()
         {
             return odb.getyyrqandxq().ToArray();
         }

         /**通过身份证号或者医疗卡号与入院时间获取挂号序号或者住院号N**/
         [WebMethod(Description = "通过身份证号或者医疗卡号与入院时间获取挂号序号或者住院号JSON*")]
         public String getbrxx(String sfzh)
         { return odbc.getbrxx(sfzh ); }

         /**通过住院号或者门诊号获取检验号返回JSON数据**/
         [WebMethod(Description = "通过住院号或者门诊号获取检验号返回JSON数据*")]
         public String getLisId(String zyh)
         { return odbc.getLisId(zyh); }
         [WebMethod(Description = "添加常用联系人")]
         public bool insertUserFriendbycard(string sfzh, string ylkh, string ph)
         {
             return odb.insertUserFriendbycard( sfzh,  ylkh,  ph);

         }
        /**获取检验申请抬头**/
         [WebMethod(Description = "获取检验申请抬头返回JSON")]
         public string getLisId1(String zyh)
         {return odbc.getLisId1( zyh);

         }
         /**获取检验明细**/
         [WebMethod(Description = "返回检验明细返回JSON")]
         public string getLisIreportmx(String jyxh)
         {
             return odbc.getLisIreportmx(jyxh);

         }
         /**根据注册人信息获取常用就诊人信息返回json**/
         [WebMethod(Description = "根据注册人信息获取常用就诊人信息返回json")]
         public string getbrxx1(string ph)
         {
             return odbc. getbrxx1(ph);
         }/**获取医院名字**/
         [WebMethod(Description = "获取医院名字")]
         public string gethosname()
         {
             return odbc.gethosname();

         }
         /**获取检查申请抬头JSON**/
         [WebMethod(Description = "获取检查申请抬头返回JSON")]
         public string getPacx(String zyh)
         {
             return odbc.getPacx(zyh);

         }


    }
    }
