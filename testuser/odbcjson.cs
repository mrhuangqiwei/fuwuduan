using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using testuser.bean;

namespace testuser
{
    public class odbcjson : IDisposable
    {
        public static SqlConnection sqlCon;
        private String ConServerStr = @"Data Source=3.3.3.2;Initial Catalog=hospital;User ID=sa;Password=ztkj";
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
        public String getbrxx(String sfzh,String rysj )
        {
            List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select  RTRIM (v_his_brxx.brid)as'brid',RTRIM (v_his_brxx.ghxh)as'ghxh',RTRIM (v_his_brxx.ylkh)as 'ylkh',v_his_brxx.ghrq,RTRIM (v_his_brxx.sfzh) as'sfzh'from v_his_brxx where (sfzh='" + sfzh + "'or ylkh='" + sfzh + "') and ghrq ='" + rysj + "'");
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
            BRXXList BRXXList = new BRXXList();
            
            List<BRXX> brxxs = new List<BRXX>();
            int i = 0;
            for (i = 0; i < list.Count(); i = i + 5)
            {
                BRXX brxx = new BRXX()
                {
                    brid = list[i],
                    ghxh = list[i + 1],
                    ylkh = list[i + 2],
                    ghrq = list[i + 3],
                    sfzh = list[i + 4],
                    
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
                {
                    zyh = list[i],
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

    }
}