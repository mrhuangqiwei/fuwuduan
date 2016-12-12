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
        private String ConServerStr = @"Data Source=3.3.3.2;Initial Catalog=hospital;Persist Security Info=True;User ID=sa;Password=ztkj";
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
        {
            List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"SELECT VIEW_BZCP_TB_LIS_Report.zyh ,           VIEW_BZCP_TB_LIS_Report.ylkh ,           VIEW_BZCP_TB_LIS_Report.jyxh ,           VIEW_BZCP_TB_LIS_Report.shrq ,           VIEW_BZCP_TB_LIS_Report.MZZYBZ ,           VIEW_BZCP_TB_LIS_Report.brxm ,           VIEW_BZCP_TB_LIS_Report.brxb ,           VIEW_BZCP_TB_LIS_Report.brnl_s ,           VIEW_BZCP_TB_LIS_Report.sqys ,           VIEW_BZCP_TB_LIS_Report.zxys ,           VIEW_BZCP_TB_LIS_Report.shry ,           VIEW_BZCP_TB_LIS_Report.ksbm ,           VIEW_BZCP_TB_LIS_Report.cwh ,           VIEW_BZCP_TB_LIS_Report.sqrq ,           VIEW_BZCP_TB_LIS_Report.cyrq ,           VIEW_BZCP_TB_LIS_Report.zxrq ,           VIEW_BZCP_TB_LIS_Report.bz ,           VIEW_BZCP_TB_LIS_Report.ybbm ,           VIEW_BZCP_TB_LIS_Report.BGDLBBM ,           VIEW_BZCP_TB_LIS_Report.sqdh ,           VIEW_BZCP_TB_LIS_Report.bbbh ,           VIEW_BZCP_TB_LIS_Report.czy ,           VIEW_BZCP_TB_LIS_Report.ybhsrq ,           VIEW_BZCP_TB_LIS_Report.BBZT ,           VIEW_BZCP_TB_LIS_Report.zxks ,           VIEW_BZCP_TB_LIS_Report.xmmc ,           VIEW_BZCP_TB_LIS_Report.JCJYJGDM ,           VIEW_BZCP_TB_LIS_Report.XGBZ ,           VIEW_BZCP_TB_LIS_Report.xmbm     FROM VIEW_BZCP_TB_LIS_Report where zyh='"+zyh+"'  ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); list.Add(reader[8].ToString()); list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString()); list.Add(reader[11].ToString()); list.Add(reader[12].ToString()); list.Add(reader[13].ToString()); list.Add(reader[14].ToString()); list.Add(reader[15].ToString()); list.Add(reader[16].ToString()); list.Add(reader[17].ToString()); list.Add(reader[18].ToString()); list.Add(reader[19].ToString());
                    list.Add(reader[20].ToString()); list.Add(reader[21].ToString()); list.Add(reader[22].ToString()); list.Add(reader[23].ToString()); list.Add(reader[24].ToString()); list.Add(reader[25].ToString()); list.Add(reader[26].ToString()); list.Add(reader[27].ToString()); list.Add(reader[28].ToString()); 
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
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
            String dt = dt1.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
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
            {
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
                  LODGEDATE = list[k + 11],
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
                    reportDate = list[k + 32],
                    reportDoctor = list[k + 33],
                   accession_num= list[k + 34]
                   
                }; pacxids.Add(pacxid);
            } pacxidList.GetPacxId = pacxids;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(pacxidList);
        }



    }
}