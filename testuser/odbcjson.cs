using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using testuser.bean;
using System.Globalization;

namespace testuser
{
    public class odbcjson : IDisposable
    {
        public static SqlConnection sqlCon;
        private String ConServerStr = @"Data Source=PC201610221724;Initial Catalog=hospital;Integrated Security=True";
        public odbcjson()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection();
                sqlCon.ConnectionString = ConServerStr;
                sqlCon.Open();
            }
        }
        public void Dispose()
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }
        /**通过身份证号或者医疗卡号与入院时间获取挂号序号或者住院号JSON**/
        public String getbrxx(String sfzh )
        {
            List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select  RTRIM (v_his_brxx.brid)as'brid',RTRIM (v_his_brxx.ghxh)as'ghxh',RTRIM (v_his_brxx.ylkh)as 'ylkh',v_his_brxx.ghrq,RTRIM (v_his_brxx.sfzh) as'sfzh',RTRIM (v_his_brxx.zylx) as 'zylx' from v_his_brxx where (sfzh='" + sfzh + "'or ylkh='" + sfzh + "')");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); 
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            BRXXList BRXXList = new BRXXList();
            
            List<BRXX> brxxs = new List<BRXX>();
            int i = 0;
            for (i = 0; i < list.Count(); i = i + 6)
            {
               string  mm = "";
               if (list[i + 3].Length>2)
               {
                   DateTime time = Convert.ToDateTime(list[i + 3]);
                   mm = time.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
               }
               else { mm = ""; }
                BRXX brxx = new BRXX()
                {
                    brid = list[i],
                    ghxh = list[i + 1],
                    ylkh = list[i + 2],
                    ghrq = mm,
                    sfzh = list[i + 4],
                    zylx=list[i+5]
                    
                }; brxxs.Add(brxx);
            } BRXXList.GetBrxx =brxxs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(BRXXList);
        }

        /**通过住院号或者门诊号来获取检验申请单号和申请项目**/
        public String getLisId(String zyh)
        { List<string> list = new List<string>();
            try
            {string sql = String.Format(@"SELECT VIEW_BZCP_TB_LIS_Report.zyh ,           VIEW_BZCP_TB_LIS_Report.ylkh ,           VIEW_BZCP_TB_LIS_Report.jyxh ,           VIEW_BZCP_TB_LIS_Report.shrq ,           VIEW_BZCP_TB_LIS_Report.MZZYBZ ,           VIEW_BZCP_TB_LIS_Report.brxm ,           VIEW_BZCP_TB_LIS_Report.brxb ,           VIEW_BZCP_TB_LIS_Report.brnl_s ,           VIEW_BZCP_TB_LIS_Report.sqys ,           VIEW_BZCP_TB_LIS_Report.zxys ,           VIEW_BZCP_TB_LIS_Report.shry ,           VIEW_BZCP_TB_LIS_Report.ksbm ,           VIEW_BZCP_TB_LIS_Report.cwh ,           VIEW_BZCP_TB_LIS_Report.sqrq ,           VIEW_BZCP_TB_LIS_Report.cyrq ,           VIEW_BZCP_TB_LIS_Report.zxrq ,           VIEW_BZCP_TB_LIS_Report.bz ,           VIEW_BZCP_TB_LIS_Report.ybbm ,           VIEW_BZCP_TB_LIS_Report.BGDLBBM ,           VIEW_BZCP_TB_LIS_Report.sqdh ,           VIEW_BZCP_TB_LIS_Report.bbbh ,           VIEW_BZCP_TB_LIS_Report.czy ,           VIEW_BZCP_TB_LIS_Report.ybhsrq ,           VIEW_BZCP_TB_LIS_Report.BBZT ,           VIEW_BZCP_TB_LIS_Report.zxks ,           VIEW_BZCP_TB_LIS_Report.xmmc ,           VIEW_BZCP_TB_LIS_Report.JCJYJGDM ,           VIEW_BZCP_TB_LIS_Report.XGBZ ,           VIEW_BZCP_TB_LIS_Report.xmbm     FROM VIEW_BZCP_TB_LIS_Report where zyh='"+zyh+"'  ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString()); list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString()); list.Add(reader[11].ToString()); list.Add(reader[12].ToString()); list.Add(reader[13].ToString()); list.Add(reader[14].ToString()); list.Add(reader[15].ToString()); list.Add(reader[16].ToString()); list.Add(reader[17].ToString()); list.Add(reader[18].ToString()); list.Add(reader[19].ToString());
                    list.Add(reader[20].ToString()); list.Add(reader[21].ToString()); list.Add(reader[22].ToString()); list.Add(reader[23].ToString()); list.Add(reader[24].ToString()); list.Add(reader[25].ToString()); list.Add(reader[26].ToString()); list.Add(reader[27].ToString()); list.Add(reader[28].ToString()); 
                }reader.Close();
                cmd.Dispose();
            }catch (Exception)
            { }
            LisIdList listidList = new LisIdList();

            List<LisId> listids = new List<LisId>();
            int i = 0;
            for (i = 0; i < list.Count(); i = i + 29)
            {
                LisId listid = new LisId()
                { zyh = list[i],
                    ylkh = list[i + 1],
                    jyxh = list[i + 2],
                    shrq = list[i + 3],
                    MZZYBZ = list[i + 4],
                    brxm = list[i + 5],
                    brxb = list[i + 6],
                    brnl_s = list[i + 7],
                    sqys = list[i + 8],
                    zxys = list[i + 9],
                    shry = list[i + 10],
                    ksbm = list[i + 11],
                    cwh = list[i + 12],
                    sqrq = list[i + 13],
                    cyrq = list[i + 14],
                    zxrq = list[i + 15],
                    bz = list[i + 16],
                    ybbm = list[i + 17],
                    BGDLBBM = list[i + 18],
                    sqd = list[i + 19],
                    bbbh = list[i + 20],
                    czy = list[i + 21],
                    ybhsrq = list[i + 22],
                    BBZT = list[i + 23],
                    zxks = list[i + 24],
                    xmmc = list[i + 25],
                    JCJYJGDM = list[i + 26],
                    XGMZ = list[i + 27],
                    xmbm = list[i + 28],

                }; listids.Add(listid);
            } listidList.GetLisId = listids;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(listidList);
        }

        /**通过住院号或者门诊号来获取检验申请单号和申请项目**/
        public string getLisId1(String zyh)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select jyxh,brxm,xb,cwh,lx ,bah,nl,nldw,VIEW_his_jy.ksbm,RTRIM(gyb_ks.ksmc),RTRIM(sqys),VIEW_his_jy.ybbm,lis_ybbm.ybmc,lczd ,sqrq,cyrq,jyxm,lis_jyxm.mc ,bbbh,VIEW_his_jy.djrq,RTRIM(zxys),zxsb,lis_jysb.HOSTNAME,shrq,RTRIM(shry) from  VIEW_his_jy,lis_jysb ,lis_ybbm,lis_jyxm ,gyb_ks where bah='" + zyh + "' AND(VIEW_his_jy.zxsb=lis_jysb.sbbm)and(lis_ybbm.ybbm=VIEW_his_jy.ybbm) and (VIEW_his_jy.jyxm=lis_jyxm.bm)and (VIEW_his_jy.ksbm=gyb_ks.ksbm)  ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString()); list.Add(reader[9].ToString());list.Add(reader[10].ToString()); list.Add(reader[11].ToString()); list.Add(reader[12].ToString()); list.Add(reader[13].ToString()); list.Add(reader[14].ToString()); list.Add(reader[15].ToString()); list.Add(reader[16].ToString()); list.Add(reader[17].ToString()); list.Add(reader[18].ToString()); list.Add(reader[19].ToString()); list.Add(reader[20].ToString()); list.Add(reader[21].ToString()); list.Add(reader[22].ToString()); list.Add(reader[23].ToString()); list.Add(reader[24].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }

            List<string> list1 = new List<string>();
            list1 = getIdInfo();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for(int i=0;i<list1.Count;i=i+2){
                dic.Add(list1[i], list1[i + 1]);  
            }
            LisId1List listid1List = new LisId1List();
            List<LisId1> listid1s = new List<LisId1>();
            for (int k = 0; k < list.Count; k = k + 25) {
                string sqysxm, zxysxm, shryxm;
                if (dic.ContainsKey(list[k + 10])) { sqysxm = dic[list[k + 10]]; }else { sqysxm = ""; }
                if (dic.ContainsKey(list[k + 20])){ zxysxm = dic[list[k + 20]]; }else { zxysxm = ""; }
                if (dic.ContainsKey(list[k +24])){ shryxm = dic[list[k + 24]]; }else { shryxm = ""; }
                string sqrq, cyrq, djrq, shrq;
                sqrq = converttime(list[k + 14]); cyrq = converttime(list[k + 15]); djrq = converttime(list[k + 19]); shrq = converttime(list[k + 23]);
                LisId1 lis1tid = new LisId1() { jyxh = list[k], brxm = list[k + 1], brxb = list[k + 2], cwh = list[k + 3], lx = list[k + 4], bah = list[k + 5], brnl = list[k + 6], nldw = list[k + 7], ksbm = list[k + 8], ksmc = list[k + 9], sqys = list[k + 10], sqysxm = sqysxm, ybbm = list[k + 11], ybmc = list[k + 12], lczd = list[k + 13], sqrq = sqrq, cyrq = cyrq, jyxm = list[k + 16], mc = list[k + 17], bbbh = list[k + 18], djrq = djrq, zxys = list[k + 20], zxysxm = zxysxm, zxsb = list[k + 21], sbmc = list[k + 22], shrq =shrq, shry = list[k + 24], shryxm = shryxm
                }; listid1s.Add(lis1tid);
            } listid1List.GetLisId = listid1s;
    return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(listid1List);
        }


        /**通过检验序号获取检验明细项目**/
        public string getLisIreportmx(String jyxh)
        {List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@" select jyxh,lx,bah ,bz ,xh ,Convert(decimal(18,2),RTRIM(value_N)),RTRIM(value_L),RTRIM(value_T),RTRIM(n_min),RTRIM(n_max),zwmc,ywmc,sjlx ,dw ,cklx ,xsws,value_N_1,tjdw ,yblx,ckz_t from VIEW_his_jymx where jyxh='" + jyxh + "' order by xh  ,zbxm desc ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString()); list.Add(reader[9].ToString()); list.Add(reader[10].ToString()); list.Add(reader[11].ToString()); list.Add(reader[12].ToString()); list.Add(reader[13].ToString()); list.Add(reader[14].ToString()); list.Add(reader[15].ToString()); list.Add(reader[16].ToString()); list.Add(reader[17].ToString()); list.Add(reader[18].ToString()); list.Add(reader[19].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }

            List<string> list1 = new List<string>();
            list1 = getLisXzjg();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < list1.Count; i = i + 2)
            {
                dic.Add(list1[i], list1[i + 1]);
            }
            LismxList listmxList = new LismxList();
            List<Lismx> lismxs = new List<Lismx>();
            for (int k = 0; k < list.Count; k = k + 20)
            {
                string jg; string zt="2";
                if (list[k + 5].Equals("0.00") && (list[k + 6] != null))
                {
                    if (dic.ContainsKey(list[k + 6])) { jg = dic[list[k +6]]; zt = "2"; } else { jg = ""; zt = "2"; }
                }
                else
                {
                    double temp, max, min;
                    jg = list[k + 5]; temp = Convert.ToDouble(list[k + 5]); min = Convert.ToDouble(list[k+8]); max = Convert.ToDouble(list[k+9]);
                    if (temp < min) { zt = "0"; }else if (min <= temp && temp <= max) { zt = "2"; }else if (temp > max) { zt = "1"; }
                
                }
                Lismx listmx = new Lismx()
                {
                    jyxh = list[k],
                    lx = list[k + 1],
                    bah = list[k + 2],
                    bz = list[k + 3],
                    xh = list[k + 4],
                    value_N = list[k + 5],
                    value_L = list[k + 6],
                    value_T = list[k + 7],
                    jg=jg,
                    n_min = list[k + 8],
                    n_max = list[k + 9],
                    zt=zt,
                    zwmc = list[k + 10],

                    ywmc = list[k + 11],
                    sjlx = list[k + 12],
                    dw = list[k + 13],
                    cklx = list[k + 14],
                    xsws = list[k + 15],
                    value_N_1 = list[k + 16],
                    tjdw = list[k + 17],
                    yblx = list[k + 18],
                    ckz_t = list[k + 19],    
                }; lismxs.Add(listmx);
            } listmxList.GetLisId = lismxs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(listmxList);
        }


        ///获取操作员账号和姓名
        public List<String> getIdInfo()
        {
            List<string> list = new List<string>();
            try{string sql = "select RTRIM(czybm) , RTRIM (czyxm) from  gyb_czy";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                   list.Add(reader[1].ToString());}
                reader.Close();cmd.Dispose();
            }
            catch (Exception){ }
            return list;
        }
        ///获取操作员账号和姓名
        public List<String> getLisXzjg()
        {
            List<string> list = new List<string>();
            try
            {
                string sql = " SELECT RTRIM(lis_xzjg.bm), RTRIM(lis_xzjg.mc)  FROM lis_xzjg     where stop <> '1'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                }
                reader.Close(); cmd.Dispose();
            }
            catch (Exception) { }
            return list;
        }

        /**通过身份证号或者医疗卡号与入院时间获取挂号序号或者住院号JSON**/
        public String getbrxx1(string ph)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select RTRIM(sfzh)as sfzh,RTRIM(brxm)as brxm,RTRIM(brnl)as brnl,RTRIM(brxb)as'brxb',RTRIM(brjtzz)as'brjtzz',RTRIM(ph)as ph,RTRIM(brdh)asbrdh,RTRIM(ylkh)as ylkh,RTRIM(JDSJ)as JDSJ, RTRIM(brid) as brid,RTRIM(brnldw)as brnldw from gyb_user_friend where ph='"+ph+"'");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString()); list.Add(reader[9].ToString()); list.Add(reader[10].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            } Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = dicnldw();
                

            BRXX1List BRXX1List = new BRXX1List();

            List<BRXX1> brxx1s = new List<BRXX1>();
            int i = 0;
            for (i = 0; i < list.Count(); i = i + 11)
            {
                string nldw = null;
                if (dic.ContainsKey(list[i + 10])) { nldw = dic[list[i +10]];  } else { nldw = "";  }
                BRXX1 brxx1 = new BRXX1()
                {
                    sfzh = list[i],
                    brxm = list[i + 1],
                    brnl = list[i + 2],
                    brxb = list[i + 3],
                    brjtzz = list[i + 4],
                    ph = list[i + 5],
                    brdh = list[i + 6],
                    ylkh = list[i + 7],
                    JDSJ = list[i + 8],
                    brid = list[i + 9],
                    brnldw=list[i+10],
                    nldwmc=nldw


                }; brxx1s.Add(brxx1);
            } BRXX1List.GetBrxx = brxx1s;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(BRXX1List);
        }//年龄单位map
        private Dictionary<string, string> dicnldw()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1", "岁"); dic.Add("2", "月"); dic.Add("3", "日"); dic.Add("4", "时");
            return dic;
        }
        public String gethosname()
        {
            string hosname="";
            try
            {
                string sql = String.Format(@"select hosname from sys_hospitalconfig ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    hosname = reader[0].ToString();

                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return hosname;
        }
        //时间转化
        private string converttime(string sj)
        {
            DateTime dt1 = Convert.ToDateTime(sj);
            String dt = dt1.ToString("yyyy-MM-dd HH:mm", DateTimeFormatInfo.InvariantInfo);
            return dt;
        }
        //时间转化2016-12-39
        private string converttimed(string sj)
        {
            DateTime dt1 = Convert.ToDateTime(sj);
            String dt = dt1.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            return dt;
        }

        /**通过住院号或者门诊号来获取检查申请单号和申请项目**/
        public string getPacx(String zyh)
        {
            List<string> list = new List<string>();
            try
            {  
                string sql = String.Format(@"select RTRIM(NAME)as NAME,RTRIM(SEX) as SEX,RTRIM(PINYIN) as PINYIN,RTRIM(CLINICNO)as CLINICNO,RTRIM(INPATIENTNO)as INPATIENTNO,RTRIM(PATIENTID) as PATIENTID,RTRIM(STUDYID)as STUDYID,RTRIM(AGE)as AGE,RTRIM(AGEUNIT) as AGEUNIT ,RTRIM(LODGESECTION) as LODGESECTION,RTRIM(LODGEDOCTOR)as LODGEDOCTOR,LODGEDATE,RTRIM(BEDNO)as BEDNO,RTRIM(applyNO)as applyNO,RTRIM(applySerialNumber)as applySerialNumber,RTRIM(applyitem)as applyitem,RTRIM(applyitemAll)as applyitemAll,RTRIM(applyID)as applyID,RTRIM(CLIISINPAT)as CLIISINPAT,RTRIM(ENROLDOCTOR)as ENROLDOCTOR,ENROLDATE,RTRIM (SURGERYRESULT)as SURGERYRESULT,RTRIM(CHECKPURPOSE)as CHECKPURPOSE,RTRIM(STATUS)as STATUS,RTRIM(CLASSNAME)as CLASSNAME,RTRIM(PHOTONO)as PHOTONO,RTRIM(TOTALFEE)as TOTALFEE,RTRIM(INHOSPITALNO)as INHOSPITALNO,RTRIM(MODALITYNAME)as MODALITYNAME,CHECKDATE,RTRIM(CHECKDOCTOR)as CHECKDOCTOR ,RTRIM(PARTOFCHECK)as PARTOFCHECK,reportDate,RTRIM(reportDoctor)as reportDoctor,RTRIM(accession_num)as accession_num from  view_pacs_id where (CLINICNO='"+zyh+"'OR INPATIENTNO='"+zyh+"') ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString()); list.Add(reader[9].ToString()); list.Add(reader[10].ToString()); list.Add(reader[11].ToString()); list.Add(reader[12].ToString()); list.Add(reader[13].ToString()); list.Add(reader[14].ToString()); list.Add(reader[15].ToString()); list.Add(reader[16].ToString()); list.Add(reader[17].ToString()); list.Add(reader[18].ToString()); list.Add(reader[19].ToString()); list.Add(reader[20].ToString()); list.Add(reader[21].ToString()); list.Add(reader[22].ToString()); list.Add(reader[23].ToString()); list.Add(reader[24].ToString()); list.Add(reader[25].ToString()); list.Add(reader[26].ToString()); list.Add(reader[27].ToString()); list.Add(reader[28].ToString()); list.Add(reader[29].ToString()); list.Add(reader[30].ToString()); list.Add(reader[31].ToString()); list.Add(reader[32].ToString()); list.Add(reader[33].ToString()); list.Add(reader[34].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            PacxIdList pacxidList = new PacxIdList();

            List<PacxId> pacxids = new List<PacxId>();
            for (int k = 0; k < list.Count; k = k + 35)
            { String sqsj,bgsj;
            sqsj = converttime(list[k+11]);
            bgsj = converttime(list[k + 32]);
                PacxId pacxid = new PacxId()
                {
                    NAME = list[k],
                    SEX = list[k + 1],
                    PINYIN = list[k + 2],
                    CLINICNO = list[k + 3],
                   INPATIENTNO = list[k + 4],
                    PATIENTID = list[k + 5],
                    STUDYID = list[k + 6],
                   AGE = list[k + 7],
                    AGEUNIT = list[k + 8],
                   LODGESECTION = list[k + 9],
                LODGEDOCTOR = list[k + 10],
                    LODGEDATE = sqsj,
                  BEDNO = list[k + 12],
                   applyNO = list[k + 13],
                    applySerialNumber = list[k + 14],
                   applyitem= list[k + 15],
                   applyitemAll = list[k + 16],
                  applyID = list[k + 17],
                 CLISINPAT= list[k + 18],
                  ENROLDOCTOR = list[k + 19],
                  ENOLDATE = list[k + 20],
                  SUGERYRSESULT = list[k + 21],
                   CHECKPURPOSE = list[k + 22],
                   STATUS = list[k + 23],
                    CLASSNAME = list[k + 24],
                    PHOTONO = list[k + 25],
                    TOTALFEE = list[k + 26],
                    INHOSPITALNO = list[k + 27],
                    MODALITYNAME = list[k + 28],
                    CHECKDATE = list[k + 29],
                    CHECKDOCTOR = list[k + 30],
                    PARTOFCHECK = list[k + 31],
                    reportDate = bgsj,
                    reportDoctor = list[k + 33],
                   accession_num= list[k + 34]
                   
                }; pacxids.Add(pacxid);
            } pacxidList.GetPacxId = pacxids;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(pacxidList);
        }

        /**通过身份证号获取体检病人基本信息返回JSON**/
        public string getTjjbxx(String sfzh)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select RTRIM(tjbh)as tjbh ,RTRIM(grbh) as grbh,RTRIM(tjzt)as tjzt,(tjrq)as tjrq,RTRIM(fzdj)as fzdj,RTRIM(xm)as xm,RTRIM(pydm)as pydm,RTRIM(xb)as xb,RTRIM(kh)as kh,RTRIM(bgdy)as bgdy,(bgdyrq)as bgdyrq,RTRIM(nl)as nl,RTRIM(dwbm)as dwbm,RTRIM(dwmc)as dwmc,RTRIM(sfzh)as sfzh,RTRIM(sj)as sj,RTRIM(jtdz)as jtdz,(djshrq)as djshrq,RTRIM(djshry)as djshry,RTRIM(czyxm)as czyxm from view_tjgl_jbxx where(sfzh='"+sfzh+"')or (tjbh='"+sfzh+"') ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString()); list.Add(reader[9].ToString()); list.Add(reader[10].ToString()); list.Add(reader[11].ToString()); list.Add(reader[12].ToString()); list.Add(reader[13].ToString()); list.Add(reader[14].ToString()); list.Add(reader[15].ToString()); list.Add(reader[16].ToString()); list.Add(reader[17].ToString()); list.Add(reader[18].ToString()); list.Add(reader[19].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            TjjbxxList tjjbxxList = new TjjbxxList();

            List<Tjjbxx> tjjbxxs = new List<Tjjbxx>();
            for (int k = 0; k < list.Count; k = k + 20)
            {
                String tjrq, bgdyrq, djshrq;
                tjrq = converttime(list[k + 3]);
                bgdyrq = converttime(list[k + 10]);
                djshrq = converttime(list[k + 17]);
               Tjjbxx tjjbxx = new Tjjbxx ()
                {
                    tjbh = list[k],
                    grbh = list[k + 1],
                    tjzt = list[k + 2],
                    tjrq = tjrq,
                    fzdj = list[k + 4],
                    xm = list[k + 5],
                    pydm = list[k + 6],
                    xb = list[k + 7],
                    kh = list[k + 8],
                    bgdy = list[k + 9],
                    bgdyrq =bgdyrq,
                    nl = list[k + 11],
                    dwbm = list[k + 12],
                    dwmc = list[k + 13],
                    sfzh = list[k + 14],
                    sj = list[k + 15],
                    jtdz = list[k + 16],
                    djshrq = djshrq,
                    dishry = list[k + 18],
                    czyxm = list[k + 19]

                }; tjjbxxs.Add(tjjbxx);
            } tjjbxxList.GetTjjbxx=tjjbxxs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(tjjbxxList);
        }
        /**通过体检编号获取体检病人结果信息返回JSON**/
        public string getTjjbg(String tjbh)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            try
            {
                string sql = String.Format(@"select tjbh,zhbm,zhmc,ksmc,ysxm from view_tjgl_tjjg where (tjbh='"+tjbh+"') order by  ksxssx,zhbm,zhxsxh,xmxh");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); 
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            if (list.Count > 4)
            {
                string zhbm1 = list[1];
                list1.Add(zhbm1);

                for (int i = 0; i < list.Count; i = i + 5) {
                    if (!zhbm1.Equals(list[i + 1])) {
                        list1.Add(list[i + 1]);
                        zhbm1 = list[i + 1];
                    }
                }
                TjjgList tjjgList = new TjjgList();
                List<Tjjg> tjjgs = new List<Tjjg>();
                for (int k = 0; k < list1.Count; k++) {
                    List<string> list2 = new List<string>();
                    list2 = getTjdxzbjg(tjbh, list1[k]);
                    String ysxm, ksmc, zhmc;
                    ysxm = list2[4]; ksmc = list2[3]; zhmc = list2[2];
                    TjzbjgList tjzbjgList = new TjzbjgList();
                    List<Tjzbjg> tjzbjgs = new List<Tjzbjg>();
                    for (int j = 0; j < list2.Count; j = j + 12)
                    {Tjzbjg tjzbjg = new Tjzbjg()
                        {
                            xmbm = list2[j + 5],
                            zhbm = list2[j + 1],
                            ckxx = list2[j + 6],
                            cksx = list2[j + 7],
                            ycts = list2[j + 8],
                            zhmc = list2[j + 2],
                            xmmc = list2[j + 9],
                            xmdw = list2[j + 10],
                            ysxm = list2[j + 4],
                            jcjg = list2[j + 11],
                            tjbh = list2[j + 0]

                        }; tjzbjgs.Add(tjzbjg);
                    } tjzbjgList.GetTjzbjg = tjzbjgs;


                    Tjjg tjjg = new Tjjg()
                    { ysxm = ysxm,
                        ksmc =ksmc,
                        zhmc = zhmc,
                      tjzbjg = tjzbjgList
                    }; tjjgs.Add(tjjg);
                } tjjgList.GetTjjg = tjjgs;
                return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(tjjgList);
            }
            else { return ""; }

        }

        /**获取体检单项指标结果**/
        public List<String> getTjdxzbjg(String tjbh,String zhbh)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "select tjbh,zhbm,zhmc,ksmc,ysxm,xmbm,ckxx,cksx,ycts,xmmc,xmdw,jcjg from view_tjgl_tjjg where (tjbh='"+tjbh+"')and (zhbm='"+zhbh+"') order by  ksxssx,zhbm,zhxsxh,xmxh";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString()); list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString()); list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString()); list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString()); list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString()); list.Add(reader[10].ToString());
                    list.Add(reader[11].ToString()); 
                } reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { }

           
            return list;


        }

        /**获取体检小结结果**/
        public String  getTjxj(String tjbh)
        { List<string> list = new List<string>();
            try {string sql = "select tjbh,ksbm,xjlr,ksmc from view_tjgl_tjxj where tjbh= '"+tjbh+"' order by xssx";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString());list.Add(reader[3].ToString()); 
                } reader.Close();
                cmd.Dispose(); }
            catch (Exception)
            { }
            TjxjList tjxjList = new TjxjList();
            List<Tjxj> tjxjs = new List<Tjxj>();
            for (int k = 0; k < list.Count; k = k + 4)
            { Tjxj tjxj= new Tjxj()
                { tjbh=list[k], ksbm=list[k+1], xklr=list[k+2],  ksmc=list[k+3]
                }; tjxjs.Add(tjxj);
            } tjxjList.GetTjxj = tjxjs;
           return  new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(tjxjList);
        }
        /**获取体检建议**/
        public String  getTjjy(String tjbh)
        { List<string> list = new List<string>();
        try
        {
            string sql = "  select zjjy jlmc,ysxm,czybm,tjbh from view_tjgl_tjjy where  ( ( tjbh = '"+tjbh+"' ) )   ";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString());list.Add(reader[3].ToString()); 
                } reader.Close();
                cmd.Dispose(); }
            catch (Exception)
            { }
            TjjyList tjjyList = new TjjyList();
            List<Tjjy> tjjys = new List<Tjjy>();
            for (int k = 0; k < list.Count; k = k + 4)
            { Tjjy tjjy= new Tjjy()
                { jlmc=list[k],
                    ysxm=list[k+1],
                    czybm=list[k+2],
                    tjbh=list[k+3]
                }; tjjys.Add(tjjy);
            } tjjyList.GetTjjy = tjjys;
           return  new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(tjjyList);
        }


        /**获取体检总结结果**/
        public String getTjzj(String tjbh)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "  select zjlr,jlbm ,tjbh from view_tjgl_tjzj  where  ( ( tjbh = '"+tjbh+"' ) )  ";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); 
                } reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { }
            TjzjList tjzjList = new TjzjList();
            List<Tjzj> tjzjs = new List<Tjzj>();
            for (int k = 0; k < list.Count; k = k + 3)
            {
                Tjzj tjzj = new Tjzj()
                { zjlr = list[k],
                    jlbm = list[k + 1],
                   tjbh = list[k + 2],
                   
                }; tjzjs.Add(tjzj);
            } tjzjList.GetTjzj = tjzjs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(tjzjList);
        }

        /**通过住院号获取病人费用明细**/
        public String getBrmxfy(String zyh)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            list1 = Getmxfyxm();
            try
            {
                string sql = String.Format(@"select Rtrim(zyh)as zyh,Rtrim(xlbm) as xlbm,Rtrim(dlbm) as dlbm,Rtrim(mxfyxmbm) as mxfyxmbm,Rtrim(ypmc)as ypmc,Rtrim(fysl) as fysl,Rtrim(fydj) as fydj,Rtrim(fyje) as fyje,Rtrim(yhje)as yhje, Rtrim(tclb)as tclb,Rtrim(nbtclb)as nbtclb,Rtrim(fygg)as fygg,Rtrim(jldwmc)as jldwmc,Rtrim(sgbm) as sgbm  from view_brfy_mx where zyh='"+zyh+"' ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString()); list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString()); list.Add(reader[11].ToString()); list.Add(reader[12].ToString()); list.Add(reader[13].ToString()); 
                } reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { } 
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < list1.Count; i = i + 2)
            { dic.Add(list1[i], list1[i + 1]); }
            BrfymxList brfymxList = new BrfymxList();
            List<Brfymx> brfymxs = new List<Brfymx>();
            for (int i = 0; i < list.Count(); i = i + 14)
            {
                string ypmc="";
                if (list[i + 4].Equals(""))
                {
                    if (dic.ContainsKey(list[i + 3]))
                    {
                        ypmc = dic[list[i + 3]];
                    }
                }
                else { ypmc = list[i + 4]; }
                
                Brfymx brfymx = new Brfymx()
                {
                    zyh = list[i],
                   xlbm=list[i+1],
                   dlbm=list[i+2],
                   mxfyxmbm=list[i+3],
                   ypmc=ypmc,
                   fysl=list[i+5],
                   fydj=list[i+6],
                   fyje=list[i+7],
                   yhje=list[i+8],
                   tclb=list[i+9],
                   nbtclb=list[i+10],
                   fygg=list[i+11],
                   jldwmc=list[i+12],
                   sgbm=list[i+13]
               
                }; brfymxs.Add(brfymx);
            } brfymxList.GetBrfymx= brfymxs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(brfymxList);
       
        }

        //获取明细费用项目KEY
        public List<String> Getmxfyxm()
        {
            List<string> list = new List<string>(); try
            {
                string sql = String.Format(@"select Rtrim(mxfyxmbm) as mxfyxmbm,Rtrim(mxfyxmmc) as mxfyxmmc from gyb_mxfyxm");
                SqlCommand cmd = new SqlCommand(sql, sqlCon); SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {list.Add(reader[0].ToString()); list.Add(reader[1].ToString());
                } reader.Close(); cmd.Dispose();
            }
            catch (Exception) { }
            return list;
        }
        /**获取住院费用**/
        public String getzyfy(String zyh)
        {List<string> list = new List<string>();
            try
            { string sql = "select Rtrim(zyh) as zyh,ryrq,cyrq,Rtrim(rycwid) as rycwid,cyks,ryks,ksmc,brxm,cast(v_zyb_zcxx.brnl as int) as brnl,Rtrim(v_zyb_rydj.brnldw) as brnldw, Rtrim(brxb)as brxb ,jtzz,fbmc,(SELECT sum ( fyje ) FROM v_zyb_brfy zyb_brfy WITH ( NOLOCK ) WHERE zyh ='" + zyh + "'AND yxbz ='1' ) as fyje ,(SELECT sum ( yjje ) FROM v_zyb_yjjl WHERE zyh ='" + zyh + "' ) as yjje from v_zyb_rydj,v_zyb_zcxx,gyb_brfb,gyb_ks where zyh='" + zyh + "' and v_zyb_rydj.brid=v_zyb_zcxx.brid and v_zyb_rydj.fbbm=gyb_brfb.fbbm and(v_zyb_rydj.ryks=gyb_ks.ksbm)";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString()); list.Add(reader[2].ToString());list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString()); list.Add(reader[10].ToString());list.Add(reader[11].ToString()); list.Add(reader[12].ToString());list.Add(reader[13].ToString()); list.Add(reader[14].ToString()); } reader.Close();
                cmd.Dispose();  }
            catch (Exception)
            { }
            ZyfyList zyfylist = new ZyfyList();
            List<Zyfy> zyfys = new List<Zyfy>();
            for (int k = 0; k < list.Count; k = k + 15) {
                String ryrq, cyrq;
                ryrq = converttimed(list[k + 1]);
                cyrq = converttimed(list[k + 2]); 
                Zyfy zyfy = new Zyfy { 
                zyh=list[k],
                ryrq=ryrq,
                cyrq=cyrq,
                rycwid=list[k+3],
                cyks=list[k+4],
                ryks=list[k+5],
                ksmc=list[k+6],
                brxm=list[k+7],
                brnl=list[k+8],
                brnldw=list[k+9],
                brxb=list[k+10],
                jtzz=list[k+11],
                fbmc=list[k+12],
                fyje=list[k+13],
                yjje=list[k+14]
                }; zyfys.Add(zyfy);

            } zyfylist.GetZyfy = zyfys;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(zyfylist);

        }


    }
}