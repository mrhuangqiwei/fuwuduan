using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using testuser.bean;



namespace testuser
{
    public class odbc : IDisposable
    { 
        public static SqlConnection sqlCon;
        private String ConServerStr = @"Data Source=PC201610221724;Initial Catalog=hospital;Integrated Security=True";
        public odbc()
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
        /// <summary>
        /// 插入用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="idcard"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool insertUserInfo(String userid, String idcard, String username, String password,String jtzz)
        {try
            { string sql = "insert into gyb_user(userid, idcard,username,password,jtzz)values('" + userid + "','" + idcard + "','" + username + "','" + password + "','" + jtzz + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        ///插入预约挂号表从网站
        public bool insertyyghfromwz(String ylkh,String jtzz, String sj,String ghys,String yysj)
        { List<string> list = new List<string>();
          List<string> list1 = new List<string>();
          List<string> list2 = new List<string>();
          String yyys = "", yyks = "";
            //获取预约挂号的ywxhid
              list1 = getywxuxhyyid();
            ///根据医疗卡号获取当前病人信息  
            list = getuserinfobyylkh(ylkh);
            //通过医生姓名获取医生信息
            list2 =getDoctorInfoByxm(ghys);
            if (list2.Count < 3) { return false; } else { yyys = list2[1]; yyks = list2[2]; }
            if (list[0].Length > 2)
            {   String brxm2 = list[1], brxb2 = list[2], brnl2 = list[3], brnldw2 = list[4], sfzh2 = list[5], brjtzz2 = list[6], brsj2 = list[7], brid2 = list[8];
                String sj1 = sj, ghys1 = ghys, yysj1 = yysj;
                updateghb_zcxx(sj, jtzz, brid2);
                String yy = "";
                double yyid1 = getyyghid();
                int k = getdatedifference(list1[2]);
                string xh = "000001";
                String ssrq = getdatetime();
                String xh1 = "";
                int m = int.Parse(list1[1]) + 1;
                DateTime dt1 = Convert.ToDateTime(yysj);
     
                String yyyxrq = dt1.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                xh1 = getywxuxhyyid(m);
                //获取系统当前日期用来作为登记日期需要格式为2016-07-07 21:28:08.133
                DateTime dt = DateTime.Now;
                String yydjrq = dt.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);

                if (k >= 1)
                {   updateYwxhb1(xh, ssrq);
                    double f = getyyghid() + 1;
                    String yyid = f.ToString();
                    Insertyygh(yyid, "01", "0269", yysj, brxm2, brxb2, "", brnl2,
                        "1", sfzh2, jtzz, sj, yyys, yyks, yydjrq, yyyxrq, "0001");
                    updateyyghbrid(brid2, yyid); 
                    return true;
                }
                else
                {
                    yy = xh1;
                    updateYwxhb1(xh1, ssrq);
                    double f1 = yyid1 + m;
                    String yyid = f1.ToString();
                    Insertyygh(yyid, "01", "0269", yysj, brxm2, brxb2, "", brnl2,
                        "1", sfzh2, jtzz, sj, yyys, yyks, yydjrq, yyyxrq, "0001");
                    updateyyghbrid(brid2, yyid); 
                    return true;
                }
            }
            else { return false; }
    
        }
        /// 更新注册表
        public bool updateghb_zcxx(String sj, String jtzz,String brid)
        { try
            {string sql = "update ghb_zcxx set sj='"+sj+"',jtzz='"+jtzz+"' where brid="+brid+"";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;  }
            catch (Exception)
            {   return false;
            } }
        /**通过医生姓名获取医生信息**/
        public List<String> getDoctorInfoByxm(String ysxm)
        { List<string> list = new List<string>();
           try
           {
               string sql = "select czyxm,czybm,ksbm,ksmc,zcbm,zcmc,mzsbdd from View_CZY where czyxm ='"+ysxm+"'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {   list.Add(reader[0].ToString());
                list.Add(reader[1].ToString()); list.Add(reader[2].ToString());
                list.Add(reader[3].ToString()); list.Add(reader[4].ToString());
                list.Add(reader[5].ToString()); list.Add(reader[6].ToString());
                     }reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {  }
            return list;
        }




        ///获取用户账号和密码
        public List<String> getUserInfo()
        { List<string> list = new List<string>();
            try
            {string sql = "select userid , password from gyb_user";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {     }
            return list;
        }


        ///获取用户费用信息

        public List<String> getUserFF(string zyll)
        {
            List<string> list = new List<string>();
            string k = getbqcybz(zyll);
            if (k.Equals("0"))
            {


                try
                {
                    string sql = String.Format(@"  select (select ylkzhje from zyb_zyylkxx   where zyh='" + zyll + "')+ (select jzxe from gyb_brfb where fbbm=(select fbbm from zyb_rydj where zyh='" + zyll + "'))-(SELECT sum ( fyje ) FROM zyb_brfy Where zyh='" + zyll + "') as '记账限额',  (select ylkzhje from zyb_zyylkxx    where zyh='" + zyll + "') as '预交金额',(SELECT sum ( fyje ) FROM zyb_brfy Where zyh='" + zyll + "') as '费用合计'  ,(select (select ylkzhje from zyb_zyylkxx where zyh='" + zyll + "' )-(SELECT sum ( fyje ) FROM zyb_brfy where zyh='" + zyll + "')) as '账户余额'  ,(select brxm from zyb_zcxx where brid=(select  brid from zyb_rydj where zyh='" + zyll + "' )) as '病人姓名'");





                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //将结果集信息添加到返回向量中
                        list.Add(reader[0].ToString());
                        list.Add(reader[1].ToString());
                        list.Add(reader[2].ToString());
                        list.Add(reader[3].ToString());
                        list.Add(reader[4].ToString());

                    }

                    reader.Close();
                    cmd.Dispose();

                }
                catch (Exception)
                {

                }
            }

            else
            {try
                {
                    string sql = String.Format(@"  select (select zfy from bagl_basy_fyxx where  zyh='" + zyll + "') as '总费用',(select ybylfwf+zybzlzf+zybzlzhzf+ybzlczf+hlf+qtfy  from bagl_basy_fyxx where  zyh='" + zyll + "')as '综合医疗服务费',(select blzdf+syszdf+yxxzdf+lczdxmf from bagl_basy_fyxx where  zyh='" + zyll + "')as '诊断费',(select fsszlxmf+lcwlzlf+sszlf +mzf +ssf from bagl_basy_fyxx where zyh='" + zyll + "')as '治疗类',(select kff from bagl_basy_fyxx where  zyh='" + zyll + "')as '康复类',(select  zyzd+zyzl+zywz+zygs+zcyjf+zytlzl+zygczl+zytszl+zyqt+zytstpjg+bzss   from bagl_basy_fyxx where  zyh='" + zyll + "')as'中医类',(select xyf+kjywfy from bagl_basy_fyxx where  zyh='" + zyll + "')as'西药类',(select cyf+yljgzyzjf+zyf from bagl_basy_fyxx where  zyh='" + zyll + "')as '中药类',(select xf+bdblzpf+qdblzpf+nxyzlzpf+xbyzlzpf   from bagl_basy_fyxx where  zyh='" + zyll + "')as'血液和血液制品类',(select jcclf+zlclf+ssclf from bagl_basy_fyxx where  zyh='" + zyll + "')as '耗材类',(select qtf from bagl_basy_fyxx where  zyh='" + zyll + "') as '其他费',(select brxm from bagl_basy_jbxx where zyh='" + zyll + "') as '病人姓名'");
                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //将结果集信息添加到返回向量中
                        list.Add(reader[0].ToString());list.Add(reader[1].ToString());list.Add(reader[2].ToString()); list.Add(reader[3].ToString());list.Add(reader[4].ToString());list.Add(reader[5].ToString());list.Add(reader[6].ToString());list.Add(reader[7].ToString());list.Add(reader[8].ToString());list.Add(reader[9].ToString());list.Add(reader[10].ToString());list.Add(reader[11].ToString());
                 }reader.Close();
                   cmd.Dispose();

                }
                catch (Exception)
                {

                }


            }

            return list;
        }
        public String getbqcybz(String zyh)
        {
            String i = "";
            try
            {
                String sqltext = "select bqcybz from zyb_rydj where zyh=" + zyh;
                SqlCommand cmd = new SqlCommand(sqltext, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i = reader[0].ToString();
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }

            return i;
        }

        ///获取用户门诊费用信息

        public List<String> getUserMzfy(String mzh)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = String.Format(@"select ( select brxm  from ghb_zcxx where brid='" + mzh + "')as '病人姓名',(select jzrq from ghb_brgh where brid='" + mzh + "  ')as'就诊日期',( select jtzz from ghb_zcxx where brid='" + mzh + " ')as '家庭住址',(select SUM(mzb_brfy.fyje) from mzb_brfy where rybrid='" + mzh + "')as '总费用',( select  gyb_czy.czyxm from ghb_brgh,gyb_czy where ghb_brgh.brid='" + mzh + "' and gyb_czy.czybm=ghb_brgh.jzys )as '就诊医生',(select sum(fyje) from mzb_brfy where rybrid='" + mzh + " ' and xlbm='10' )as '诊查费',(select SUM (fyje) from mzb_brfy where rybrid='" + mzh + "  ' and xlbm='13')as '挂号费', (select sum(fyje) from mzb_brfy where rybrid='" + mzh + "  ' and xlbm!='10' and xlbm!='13') as '其他费'");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }
///获取用户门诊基本信息信息
  public List<String> getUserMzJbxx(String ghxh)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            try
            {
                string sql = String.Format(@" select top 1 yfb_ypcf.brxm as '病人姓名',gyb_ks.ksmc as'科室名称',gyb_czy.czyxm as'就诊医生',yfb_ypcf.cfrq as'处方日期',ghb_zcxx.brnl as'年龄',ghb_zcxx.jtzz as '家庭住址',ghb_zcxx.sfzh as '身份证号' from yfb_ypcf,gyb_czy,gyb_ks,ghb_zcxx where (yfb_ypcf.brid=ghb_zcxx.brid)and (yfb_ypcf.ksbm=gyb_ks.ksbm)and (yfb_ypcf.ysbm=gyb_czy.czybm)and (yfb_ypcf.ghxh='" + ghxh + "')");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                }

                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            } list1 = getUserMzZycfh(ghxh);
            for (int i = 0; i < list1.Count; i++)
            {
                list.Add(list1[i]);
            }
            return list;
        }

        ///获取用户中药处方号：

        public List<String> getUserMzZycfh(String ghxh)
        { List<string> list2 = new List<string>();
            List<string> list1 = new List<string>();
            List<string> list = new List<string>();
            try
            {string sql = "select cfh as '处方号' from yfb_ypcf where ghxh='" + ghxh + "' and cflxbm='03'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                }
                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
            }
            if (list.Count == 0)
            {
                list2.Add("尊敬的患者您好！当日没有您的中药处方信息！");
            }
            else
            {
                list2.Add("mzzycf");

                for (int i = 0; i < list.Count; i++)
                {list1 = getUserMzZYzdyfy(list[i]);
                    list1.Add("mzzycf");
                    for (int j = 0; j < list1.Count; j++)
                    {
                        list2.Add(list1[j]);
                    }
                }
            }
            return list2;
        }

        ///获取用户其他处方号：

        public List<String> getUserMzQtcfh(String ghxh)
        {List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            try
            {string sql = "select yfb_ypcf.cfh from yfb_ypcf where ghxh='" + ghxh + "' and  (cflxbm='01' or cflxbm='02')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            if (list.Count == 0)
            {
                list2.Add("尊敬的患者您好！当日没有您的西药处方信息！");
            }
            else
            {
                list3 = getUserMzxx(ghxh);
                for (int j = 0; j < list3.Count; j++)
                {
                    list2.Add(list3[j]);
                }

                list2.Add("mzxycf");

                for (int i = 0; i < list.Count; i++)
                {
                    list1 = getUserMzXyzd(list[i]);

                    list1.Add("mzxycf");

                    for (int j = 0; j < list1.Count; j++)
                    {
                        list2.Add(list1[j]);
                    }
                }
            }
            return list2;
        }
        ///获取用户门诊中药处方信息

        public List<String> getUserMzZYcfxx(String cfh)
        { List<string> list = new List<string>();
  try
            {string sql = String.Format(@"  SELECT  ykb_ypzd.ypmc, yfb_yppf.zl ,ykb_jldwbm.jldwmc FROM yfb_yppf , ykb_ypzd , ykb_ypzl ,ykb_jldwbm WHERE ( yfb_yppf.ryypbm = ykb_ypzd.ypbm )  and  ( ykb_ypzl.ypzlbm = ykb_ypzd.ypzlbm ) and ( yfb_yppf.ypbz = 1 ) and( yfb_yppf.cfh = '" + cfh + "' ) and  (ykb_jldwbm.jldwid=yfb_yppf.jldw)");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {

            }

            return list;
        }

        ///获取用户门诊中药用法与中药副数

        public List<String> getUserMzZYzdyfy(String cfh)
        {
            List<string> list2 = new List<string>();
            List<string> list1 = new List<string>();
            list1 = getUserMzZYcfxx(cfh);
            List<string> list = new List<string>();

            try
            {
               string sql = String.Format(@"   select fysm as '服药说明',fyts as'中药副数' ,mzzd as '临床诊断'from yfb_ypcf where cfh='" + cfh + "'");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            for (int i = 0; i < list1.Count; i++)
            { list.Add(list1[i]); }
            return list;
        }
        ///获取用户门诊西药处方信息

        public List<String> getUserMzXycfxx(String cfh)
        {

            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select   ykb_ypzd.ypmc,ykb_ypzd.ypgg, yfb_yppf.zl,(select jldwmc  from ykb_jldwbm where yfb_yppf.yfdw = ykb_jldwbm.jldwid)as'yfdw',yfb_yppf.fyjl,   ykb_jldwbm.jldwmc, gyb_gytj.tjmc,gyb_pc.pcmc FROM yfb_yppf,    ykb_ypzd,ykb_ypzl,ykb_jldwbm,gyb_gytj,gyb_pc WHERE ( yfb_yppf.ryypbm = ykb_ypzd.ypbm ) and(ykb_jldwbm.jldwid=yfb_yppf.jldw)and  ( ykb_ypzl.ypzlbm = ykb_ypzd.ypzlbm ) and  (gyb_gytj.tjbm=yfb_yppf.yyff)and(gyb_pc.pcbm=yfb_yppf.pcbm)and( yfb_yppf.ypbz = 1 ) AND  ( yfb_yppf.cfh = '" + cfh + "' )");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }



        ///获取用户门诊西药诊断信息

        public List<String> getUserMzXyzd(String cfh)
        { List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            list1 = getUserMzXycfxx(cfh);
            try
            {string sql = String.Format(@" select  yfb_ypcf.mzzd, gyb_brfb.fbmc from ghb_brgh,yfb_ypcf,gyb_brfb where ghb_brgh.ghxh=yfb_ypcf.ghxh and ghb_brgh.fbbm=gyb_brfb.fbbm and yfb_ypcf.cfh='" + cfh + "'");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
 while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());}

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            for (int i = 0; i < list1.Count; i++)
            { list.Add(list1[i]); }
            return list;
        }
        //获取门诊基本信息（单纯之）
        public List<String> getUserMzxx(String ghxh)
        { List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            try
            {

                string sql = String.Format(@" select top 1 yfb_ypcf.brxm as '病人姓名',gyb_ks.ksmc as'科室名称',gyb_czy.czyxm as'就诊医生',yfb_ypcf.cfrq as'处方日期',ghb_zcxx.brnl as'年龄',ghb_zcxx.jtzz as '家庭住址',ghb_zcxx.sfzh as '身份证号' from yfb_ypcf,gyb_czy,gyb_ks,ghb_zcxx where (yfb_ypcf.brid=ghb_zcxx.brid)and (yfb_ypcf.ksbm=gyb_ks.ksbm)and (yfb_ypcf.ysbm=gyb_czy.czybm)and (yfb_ypcf.ghxh='" + ghxh + "')");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                   list.Add(reader[6].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }

        //获取治疗处置基本信息
        public List<String> getUserZlczxx(String ghxh)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            try
            {string sql = String.Format(@" select top 1 gyb_ks.ksmc,gyb_czy.czyxm,(select Convert(varchar(100), sqsj,111)) ,ghb_zcxx.brxm from mzys_zlcz,gyb_czy,gyb_ks,ghb_brgh,ghb_zcxx WHERE ( mzys_zlcz.ryghxh= '" + ghxh + "' )   and(gyb_czy.czybm=mzys_zlcz.sqys)and(mzys_zlcz.zlks=gyb_ks.ksbm)and(ghb_zcxx.brid=ghb_brgh.brid)and(ghb_brgh.ghxh=mzys_zlcz.ryghxh)");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());



                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            list1 = getUserZlXmmx(ghxh);
            if (list1.Count != 0)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    list.Add(list1[i]);
                }
            }
            else
            {
                list.Clear();
                list.Add("当日没有您的治疗处置!");
            }
            return list;
        }



        //获取治疗处置项目明细
        public List<String> getUserZlXmmx(String ghxh)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            try
            { string sql = String.Format(@"   SELECT   gyb_mxfyxm.mxfyxmmc,mzys_zlczmx.sl,mzys_zlczmx.dj, mzys_zlczmx.je FROM mzys_zlczmx,mzys_zlcz,gyb_mxfyxm WHERE mzys_zlcz.ryghxh = '" + ghxh + "'  and(mzys_zlczmx.kskfbz = '0' or mzys_zlczmx.kskfbz = '' OR mzys_zlczmx.kskfbz is null)and(mzys_zlcz.zlczh=mzys_zlczmx.zlczh)and(mzys_zlczmx.mxfyxmbm=gyb_mxfyxm.mxfyxmbm)");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }



        //获取用户信息
        public List<String> getUserxx(String userid)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            try
            {

                string sql = String.Format(@"  select  username,idcard,jtzz,userid  from gyb_user where userid='" + userid + "'");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }
        //获取科室信息
        public List<String> getKsxx()
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            try
            {
              string sql = String.Format(@" select ksbm,ksmc  from gyb_ks  where tybz=0 and sfghks=1");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }
        //获取科室医生
        public List<String> getKsys(String ksbm)
        {
            List<string> list = new List<string>();

            try
            {
                   string sql = String.Format(@"select czybm,czyxm from gyb_czy where (ghzlbm='03'or ghzlbm='02' and ksbm='" + ksbm + "' )and tybz='0'and hsbz='0'");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }
        //获取时间
        public List<String> getKsyssbsj(String ksbm)
        {
            List<string> list = new List<string>();
               for (int i = 1; i < 3; i++)
            {
                list.Add("shangban");
                list.Add("xingqi");
                for (int j = 0; j < 7; j++)
                {
                    // list.Add("xingqi");\ 
                    List<string> list1 = new List<string>();

                    list1 = getYspbxx(j, i, ksbm);
                    for (int k = 0; k < list1.Count; k++)
                    {
                        list.Add(list1[k]);
                    }
                    list.Add("xingqi");
                }


            }


            return list;
        }


        //获取医生排班信息
        public List<String> getYspbxx(int starttime, int banci, String ksbm)
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select ghb_yzhb.czybm,czyxm, bcfamc,ghb_yzhb.bcfabm ,gyb_zcbm.zcmc,(select datename(weekday,ghb_yzhb.Yzrq) ),(SELECT CONVERT(VARCHAR(5),(select  sbsj from gyb_bcfa where bcfabm=ghb_yzhb.bcfabm),8))as'上班时间' ,(SELECT CONVERT(VARCHAR(5),(select  xbsj from gyb_bcfa where bcfabm=ghb_yzhb.bcfabm),8))as'下班时间', (select RIGHT(CONVERT(varchar(10), GETDATE()+" + starttime + ", 120), 5)) as '上班日期',gyb_czy.mzsbdd,gyb_ks.ksmc,(select CONVERT(varchar(10),GETDATE(),120)+' 00:00:59.000')as'预约登记时间',(select CONVERT(varchar(30),GETDATE()+" + starttime + ",120)+':'+DATENAME(MILLISECOND,GETDATE()))as'预约挂号时间' ,(select CONVERT(varchar(30),GETDATE()+" + starttime + "+1,120)+':'+DATENAME(MILLISECOND,GETDATE()))as'预约有效时间' from gyb_czy,gyb_ks,gyb_bcfa,ghb_yzhb,gyb_zcbm where ghb_yzhb.czybm=gyb_czy.czybm and ghb_yzhb.bcfabm=gyb_bcfa.bcfabm and (ghb_yzhb.Yzrq>=(SELECT DATEADD(day, +" + starttime + ", (select convert(varchar(10),getdate(),120)+' 00:00:00.000'))))and(ghb_yzhb.Yzrq<(SELECT DATEADD(day, +" + starttime + ", (select convert(varchar(10),getdate(),120)+' 23:59:59.000'))))and gyb_bcfa.bcfabm=" + banci + " and gyb_czy.ksbm=" + ksbm + " and gyb_czy.zcbm=gyb_zcbm.zcbm and gyb_ks.ksbm=gyb_czy.ksbm");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                  
                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[11].ToString());
                    list.Add(reader[12].ToString());
                    list.Add(reader[13].ToString());

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }
        /**医生排班上午按时间返回JSON**/
        public String yspbswjson(String ksbm) {
            List<string> list1 = new List<string>(); List<string> list2 = new List<string>(); List<string> list3 = new List<string>();
            List<string> list4 = new List<string>(); List<string> list5 = new List<string>(); List<string> list6 = new List<string>(); List<string> list7 = new List<string>();
            list1 = yspbsw(0, ksbm); list2 = yspbsw(1, ksbm);list3 = yspbsw(2, ksbm);list4 = yspbsw(3, ksbm);list5 = yspbsw(4, ksbm);list6 = yspbsw(5, ksbm);
            list7 = yspbsw(6, ksbm);
            int[] arry = { list1.Count, list2.Count, list3.Count, list4.Count, list5.Count, list6.Count,list7.Count};
            int max = arry[0];
            for (int i = 0; i < arry.Length; i++)
            { if (arry[i] > max)
                    max = arry[i];    }
            string tc = "";
            leather(list1, tc, max);leather(list2, tc, max);leather(list3, tc, max); leather(list4, tc, max);leather(list5, tc, max);leather(list6, tc, max); leather(list7, tc, max);
            YspbList yspblist = new YspbList();
            List<Yspb> yspbs = new List<Yspb>();
            for (int i = 0; i < max - 15; i = i + 15) { 
            Yspb yspb=new Yspb(){Yzrq=list1[i],sbsj=list1[i+1],xbsj=list1[i+2],xhzs=list1[i+3],xyzs=list1[i+4],czybm=list1[i+5],czyxm=list1[i+6],zcmc=list1[i+7],xq=list1[i+8],mzsbdd=list1[i+9],ksmc=list1[i+10],ksbm=list1[i+11],yydjsj=list1[i+12],yyghsj=list1[i+13],yyyxsj=list1[i+14]};
            yspbs.Add(yspb);
            yspb = new Yspb() {Yzrq = list2[i],sbsj = list2[i + 1], xbsj = list2[i + 2], xhzs = list2[i + 3], xyzs = list2[i + 4], czybm = list2[i + 5], czyxm = list2[i + 6], zcmc = list2[i + 7], xq = list2[i + 8], mzsbdd = list2[i + 9], ksmc = list2[i + 10], ksbm = list2[i + 11], yydjsj = list2[i + 12], yyghsj = list2[i + 13], yyyxsj = list2[i + 14] };
            yspbs.Add(yspb);
            yspb = new Yspb() { Yzrq = list3[i], sbsj = list3[i + 1], xbsj = list3[i + 2], xhzs = list3[i + 3], xyzs = list3[i + 4], czybm = list3[i + 5], czyxm = list3[i + 6], zcmc = list3[i + 7], xq = list3[i + 8], mzsbdd = list3[i + 9], ksmc = list3[i + 10], ksbm = list3[i + 11], yydjsj = list3[i + 12], yyghsj = list3[i + 13], yyyxsj = list3[i + 14] };
            yspbs.Add(yspb);
            yspb = new Yspb() { Yzrq = list4[i], sbsj = list4[i + 1], xbsj = list4[i + 2], xhzs = list4[i + 3], xyzs = list4[i + 4], czybm = list4[i + 5], czyxm = list4[i + 6], zcmc = list4[i + 7], xq = list4[i + 8], mzsbdd = list4[i + 9], ksmc = list4[i + 10], ksbm = list4[i + 11], yydjsj = list4[i + 12], yyghsj = list4[i + 13], yyyxsj = list4[i + 14] };
            yspbs.Add(yspb);
            yspb = new Yspb() { Yzrq = list5[i], sbsj = list5[i + 1], xbsj = list5[i + 2], xhzs = list5[i + 3], xyzs = list5[i + 4], czybm = list5[i + 5], czyxm = list5[i + 6], zcmc = list5[i + 7], xq = list5[i + 8], mzsbdd = list5[i + 9], ksmc = list5[i + 10], ksbm = list5[i + 11], yydjsj = list5[i + 12], yyghsj = list5[i + 13], yyyxsj = list5[i + 14] };
            yspbs.Add(yspb);
            yspb = new Yspb() { Yzrq = list6[i], sbsj = list6[i + 1], xbsj = list6[i + 2], xhzs = list6[i + 3], xyzs = list6[i + 4], czybm = list6[i + 5], czyxm = list6[i + 6], zcmc = list6[i + 7], xq = list6[i + 8], mzsbdd = list6[i + 9], ksmc = list6[i + 10], ksbm = list6[i + 11], yydjsj = list6[i + 12], yyghsj = list6[i + 13], yyyxsj = list6[i + 14] };
            yspbs.Add(yspb);
            yspb = new Yspb() { Yzrq = list7[i], sbsj = list7[i + 1], xbsj = list7[i + 2], xhzs = list7[i + 3], xyzs = list7[i + 4], czybm = list7[i + 5], czyxm = list7[i + 6], zcmc = list7[i + 7], xq = list7[i + 8], mzsbdd = list7[i + 9], ksmc = list7[i + 10], ksbm = list7[i + 11], yydjsj = list7[i + 12], yyghsj = list7[i + 13], yyyxsj = list7[i + 14] };
            yspbs.Add(yspb);   
            }
            yspblist.GetYspb = yspbs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(yspblist);     
        }
        /**医生排班下午按时间返回JSON**/
        public String yspbxwjson(String ksbm)
        {
            List<string> list1 = new List<string>(); List<string> list2 = new List<string>(); List<string> list3 = new List<string>();
            List<string> list4 = new List<string>(); List<string> list5 = new List<string>(); List<string> list6 = new List<string>(); List<string> list7 = new List<string>();
            list1 = yspbxw(0, ksbm); list2 = yspbxw(1, ksbm); list3 = yspbxw(2, ksbm); list4 = yspbxw(3, ksbm); list5 = yspbxw(4, ksbm); list6 = yspbxw(5, ksbm);
            list7 = yspbxw(6, ksbm);
            int[] arry = { list1.Count, list2.Count, list3.Count, list4.Count, list5.Count, list6.Count, list7.Count };
            int max = arry[0];
            for (int i = 0; i < arry.Length; i++)
            {
                if (arry[i] > max)
                    max = arry[i];
            }
            string tc = "";
            leather(list1, tc, max); leather(list2, tc, max); leather(list3, tc, max); leather(list4, tc, max); leather(list5, tc, max); leather(list6, tc, max); leather(list7, tc, max);
            YspbList yspblist = new YspbList();
            List<Yspb> yspbs = new List<Yspb>();
            for (int i = 0; i < max - 15; i = i + 15)
            {
                Yspb yspb = new Yspb() { Yzrq = list1[i], sbsj = list1[i + 1], xbsj = list1[i + 2], xhzs = list1[i + 3], xyzs = list1[i + 4], czybm = list1[i + 5], czyxm = list1[i + 6], zcmc = list1[i + 7], xq = list1[i + 8], mzsbdd = list1[i + 9], ksmc = list1[i + 10], ksbm = list1[i + 11], yydjsj = list1[i + 12], yyghsj = list1[i + 13], yyyxsj = list1[i + 14] };
                yspbs.Add(yspb);
                yspb = new Yspb() { Yzrq = list2[i], sbsj = list2[i + 1], xbsj = list2[i + 2], xhzs = list2[i + 3], xyzs = list2[i + 4], czybm = list2[i + 5], czyxm = list2[i + 6], zcmc = list2[i + 7], xq = list2[i + 8], mzsbdd = list2[i + 9], ksmc = list2[i + 10], ksbm = list2[i + 11], yydjsj = list2[i + 12], yyghsj = list2[i + 13], yyyxsj = list2[i + 14] };
                yspbs.Add(yspb);
                yspb = new Yspb() { Yzrq = list3[i], sbsj = list3[i + 1], xbsj = list3[i + 2], xhzs = list3[i + 3], xyzs = list3[i + 4], czybm = list3[i + 5], czyxm = list3[i + 6], zcmc = list3[i + 7], xq = list3[i + 8], mzsbdd = list3[i + 9], ksmc = list3[i + 10], ksbm = list3[i + 11], yydjsj = list3[i + 12], yyghsj = list3[i + 13], yyyxsj = list3[i + 14] };
                yspbs.Add(yspb);
                yspb = new Yspb() { Yzrq = list4[i], sbsj = list4[i + 1], xbsj = list4[i + 2], xhzs = list4[i + 3], xyzs = list4[i + 4], czybm = list4[i + 5], czyxm = list4[i + 6], zcmc = list4[i + 7], xq = list4[i + 8], mzsbdd = list4[i + 9], ksmc = list4[i + 10], ksbm = list4[i + 11], yydjsj = list4[i + 12], yyghsj = list4[i + 13], yyyxsj = list4[i + 14] };
                yspbs.Add(yspb);
                yspb = new Yspb() { Yzrq = list5[i], sbsj = list5[i + 1], xbsj = list5[i + 2], xhzs = list5[i + 3], xyzs = list5[i + 4], czybm = list5[i + 5], czyxm = list5[i + 6], zcmc = list5[i + 7], xq = list5[i + 8], mzsbdd = list5[i + 9], ksmc = list5[i + 10], ksbm = list5[i + 11], yydjsj = list5[i + 12], yyghsj = list5[i + 13], yyyxsj = list5[i + 14] };
                yspbs.Add(yspb);
                yspb = new Yspb() { Yzrq = list6[i], sbsj = list6[i + 1], xbsj = list6[i + 2], xhzs = list6[i + 3], xyzs = list6[i + 4], czybm = list6[i + 5], czyxm = list6[i + 6], zcmc = list6[i + 7], xq = list6[i + 8], mzsbdd = list6[i + 9], ksmc = list6[i + 10], ksbm = list6[i + 11], yydjsj = list6[i + 12], yyghsj = list6[i + 13], yyyxsj = list6[i + 14] };
                yspbs.Add(yspb);
                yspb = new Yspb() { Yzrq = list7[i], sbsj = list7[i + 1], xbsj = list7[i + 2], xhzs = list7[i + 3], xyzs = list7[i + 4], czybm = list7[i + 5], czyxm = list7[i + 6], zcmc = list7[i + 7], xq = list7[i + 8], mzsbdd = list7[i + 9], ksmc = list7[i + 10], ksbm = list7[i + 11], yydjsj = list7[i + 12], yyghsj = list7[i + 13], yyyxsj = list7[i + 14] };
                yspbs.Add(yspb);
            }
            yspblist.GetYspb = yspbs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(yspblist);
        }

        //填充为空的数据
        private void leather(List<String> arr, String str, int k)
        {
            if (arr.Count < k)
            {
                for (int i = arr.Count; i < k; i++)
                {
                    arr.Add(str);
                }
            }

        }

        /**医生排版上午
         * k:时间  ksbm :科室编码
         * **/
        public List<String> yspbsw(int k, String ksbm)
        {
            List<string> list = new List<string>();
            try
            {string sql = String.Format(@" select Yzrq,sbsj,xbsj, xhzs,xyzs,czybm,czyxm,zcmc,星期,mzsbdd,ksmc , ksbm,(select CONVERT(varchar(10), GETDATE(),120)+' 00:00:59.000')as'预约登记时间',(select CONVERT(varchar(30)  ,GETDATE()+" + k + ",120)+':'+DATENAME(MILLISECOND,GETDATE()))as'预约挂号时间' ,(select CONVERT(varchar(30),GETDATE()+1+" + k + ",120)+':'+DATENAME(MILLISECOND,GETDATE())) as'预约有效时间'  from v_yspb where Yzrq>=(select CONVERT(varchar(10),GETDATE()+" + k + ",120)+' 00:00:00.000')and Yzrq<(select CONVERT(varchar(10),GETDATE()+1+" + k + ",120)+' 00:00:00.000') and  sbsj>='1900-01-01 06:00:00.000' and sbsj<'1900-01-01 12:00:00.000'  and xbsj>='1900-01-01 12:00:00.000' and ksbm='" + ksbm + "'");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {//将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[11].ToString());
                    list.Add(reader[12].ToString());
                    list.Add(reader[13].ToString());
                    list.Add(reader[14 ].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {      }
            return list;
        }
        /**医生排版下午
         * k:时间  ksbm :科室编码
         * **/
        public List<String> yspbxw(int k, String ksbm)  
        {
            List<string> list = new List<string>();
            try
            {string sql = String.Format(@" select Yzrq,sbsj,xbsj, xhzs,xyzs,czybm,czyxm,zcmc,星期,mzsbdd,ksmc , ksbm,(select CONVERT(varchar(10), GETDATE(),120)+' 00:00:59.000')as'预约登记时间',(select CONVERT(varchar(30)  ,GETDATE()+" + k + ",120)+':'+DATENAME(MILLISECOND,GETDATE()))as'预约挂号时间' ,(select CONVERT(varchar(30),GETDATE()+1+" + k + ",120)+':'+DATENAME(MILLISECOND,GETDATE())) as'预约有效时间'  from v_yspb where Yzrq>=(select CONVERT(varchar(10),GETDATE()+" + k + ",120)+' 00:00:00.000')and Yzrq<(select CONVERT(varchar(10),GETDATE()+1+" + k + ",120)+' 00:00:00.000')   and xbsj>='1900-01-01 17:30:00.000' and ksbm='" + ksbm + "'");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {//将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());  
                    list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[11].ToString());
                    list.Add(reader[12].ToString());
                    list.Add(reader[13].ToString());
                    list.Add(reader[14].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { }
            return list;
        }



        /**插入就诊人信息**/
        public bool insertUserFriend(String sfzh, String brxm, String brnl, String brxb, String brjtzz, String ph, String brdh)
        {
            List<string> list = new List<string>();
            String ss="111111111"; 

            try
            {
                string sql = "insert into gyb_user_friend(sfzh,brxm,brnl,brxb,brjtzz,ph,brdh)values('" + sfzh + "','" + brxm + "','" + brnl + "','" + brxb + "','" + brjtzz + "','" + ph + "','" + brdh + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                ss = "5555555555555555555555555555555555";
                list=getuserfriendid(ph);
                if (list[0].Length < 3) { updateUser("friendid1", sfzh, ph); }
                else if (list[1].Length < 3) { updateUser("friendid2", sfzh, ph); }
                else if (list[2].Length < 3) { updateUser("friendid3", sfzh, ph); }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /**插入就诊人信息当用户有就诊卡时**/
        public bool insertUserFriend1(String sfzh, String brxm, String brnl, String brxb, String brjtzz, String ph, String brdh)
        {
            List<string> list = new List<string>();
            String ss = "111111111";

            try
            {
                string sql = "insert into gyb_user_friend(sfzh,brxm,brnl,brxb,brjtzz,ph,brdh)values('" + sfzh + "','" + brxm + "','" + brnl + "','" + brxb + "','" + brjtzz + "','" + ph + "','" + brdh + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                ss = "5555555555555555555555555555555555";
                list = getuserfriendid(ph);
                if (list[0].Length < 3) { updateUser("friendid1", sfzh, ph); }
                else if (list[1].Length < 3) { updateUser("friendid2", sfzh, ph); }
                else if (list[2].Length < 3) { updateUser("friendid3", sfzh, ph); }

                return true;
                //"yyyyyyyyyyy"+"kkkkkkkkk"+list.Count()+"zzzzzzzzzzzzzzz"+list[1]+"rrrrrrrrrrrr"+list[0];
            }
            catch (Exception)
            {
                return false;
            }

        }


        /**跟新用户表**/
        public bool updateUser(String friendid ,String sfzh,String manageid)
        {
         

            try
            {
                string sql = " update gyb_user set  " + friendid+ "='" + sfzh + "' where userid="+manageid;
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
            return false;
            }

        }


        /**跟新业务序号**/
        public bool updateYwxhb1(String xh, String ssrq )
        {
    try
            {
                string sql = String.Format(@"BEGIN TRAN update ghb_ywxhb Set xh ='" + xh + "' , ssrq ='" + ssrq + "' Where xhlx ='yyghid' COMMIT TRAN");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //通过医疗卡或者身份证号获取病人基本信息
        public List<String>  checkylk(String sfzh, string ylkh)
        {
         List<string> list = new List<string>();
         try
         { string sql = String.Format(@"select top 1   RTRIM(ylkh)as ylkh,RTRIM(sfzh)as sfzh ,RTRIM(ylklxbm) as ylklxbm ,RTRIM(brid)AS brid,RTRIM(ghxh)as ghxh,RTRIM(ghrq)as ghrq,RTRIM(mzbm) as mzbm, RTRIM(brxm)as brxm,RTRIM(brnl)as brnl,RTRIM(brnldw)as brnldw,RTRIM(brxb)as brxb,RTRIM(jtzz)as jtzz,RTRIM(sj)  as sj from v_his_brjbxx where sfzh='"+sfzh+"'  and ylkh='"+ylkh+"'  order by ghrq  desc  ");
             SqlCommand cmd = new SqlCommand(sql, sqlCon);SqlDataReader reader = cmd.ExecuteReader();
             while (reader.Read())
             {list.Add(reader[0].ToString());list.Add(reader[1].ToString());list.Add(reader[2].ToString()); list.Add(reader[3].ToString());list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString());list.Add(reader[8].ToString()); list.Add(reader[9].ToString()); list.Add(reader[10].ToString()); list.Add(reader[11].ToString()); list.Add(reader[12].ToString());       
          } reader.Close();
             cmd.Dispose();   }
         catch (Exception) { }
         return list;    
        }
        /**插入就诊人信息**/
        public bool insertUserFriendbycard(string sfzh, string ylkh, string ph) {
            List<string> list = new List<string>();
            list = checkylk(sfzh, ylkh);
            if (list.Count > 3)
            {
                if ((sfzh.Equals(list[1]) && ylkh.Equals(list[0])) || (list[1]) == null)
                {
                    String ylkh1 = list[0], sfzh1 = list[1], ylklxbm = list[2], brid = list[3], ghxh = list[4], ghrq = list[5], mzbm = list[6], brxm = list[7], brnl = list[8], brnldw = list[9], brxb = list[10], jtzz = list[11], sj = list[12];
                    DateTime dt = DateTime.Now;
                    string dt24 = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    try
                    {
                        string sql = String.Format(@"insert into gyb_user_friend(sfzh,brxm,brnl,brxb,brjtzz,ph,brdh,ylkh,JDSJ,brid,brnldw)values('" + sfzh + "','" + brxm + "','" + brnl + "','" + brxb + "','" + jtzz + "','" + ph + "','" + sj + "','" + ylkh1 + "','" + dt24 + "','" + brid + "','" + brnldw + "')");
                        SqlCommand cmd = new SqlCommand(sql, sqlCon);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        list = getuserfriendid(ph);
                        if (list[0].Length < 3) { updateUser("friendid1", sfzh, ph); }
                        else if (list[1].Length < 3) { updateUser("friendid2", sfzh, ph); }
                        else if (list[2].Length < 3) { updateUser("friendid3", sfzh, ph); }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else return false;
            }
            else return false;

        }




        /**跟新业务序号**/
        public bool updateYwxhb2(String xh, String ssrq)
        {
            try
            {
                string sql = String.Format(@"BEGIN TRAN  update ghb_ywxhb Set xh ='" + xh + "' , ssrq ='" + ssrq + "' Where xhlx ='brid' COMMIT TRAN");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        
        
        
        /** 用户的就诊人用来判断是否有就诊人**/
        public List<String> getuserfriendid(string manageid)
        {
            List<string> list = new List<string>();
            try
            { string sql = String.Format(@"select friendid1,friendid2,friendid3 from gyb_user where userid='"+manageid+"' ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
         
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }
        /** 通过医疗卡号获取病人档案信息**/
        public List<String> getuserinfobyylkh(string ylkh)
        {List<string> list = new List<string>();
            try
            {string sql = String.Format(@"select ylkh,brxm,brxb,brnl,brnldw,sfzh,jtzz,sj,brid from yygh_ylkxx where ylkh='"+ylkh+"'  ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                 while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());
             }

                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {  }

            return list;
        }
        /***获取常用就诊人信息*/
        public List<String> getuserfriendinfo(string manageid)
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
             List<string> list3 = new List<string>();
            list1 = getuserfriendid(manageid);
            for (int i = 0; i < list1.Count(); i++) {
                if (list1[i].Length > 3) {
                    list2.Add(list1[i]);
                }
            
            }
            if (list2.Count() > 0)
            {
                for (int k = 0; k < list2.Count(); k++)
                {
                    list3 = getuserfriendidsingle(list2[k]);
                    for (int j = 0; j < list3.Count(); j++)
                    {
                        list.Add(list3[j]);
                    }
                }
            }
           
            return list;
        }


        /***预约挂号*/
        public String appointment(String yyghrq,String brxm,String brxb,String brnl,String sfzh,String jtzz,String sj,String yyys,String yyks,String yydjrq,String yyyxrq)
        {
            String yy ="";
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            list = getywxuxhyyid();
            list2 = getusertime();
            double yyid1 = getyyghid();
            int m = int.Parse(list[1]) + 1;
            String xh1 = "";
            xh1 = getywxuxhyyid(m);
            string s = list2[1];
            int k2 = int.Parse(s) + 1;
            String bridxh = getywxuxhyyid(k2);
            String hu = list[2];
            int k=getdatedifference( list[2]);
            string xh = "000001";
       //     DateTime dt = DateTime.Now; 
          //  dt.ToString();//2005-11-5 13:21:25 
           // String ssrq = dt.ToString(); 
          String ssrq = getdatetime();
            int k1 = int.Parse(list2[1]) + 1;
           // String zcrq = dt.ToString();
           String zcrq = getdatetime();

            if (k >= 1)
            {
                updateYwxhb1(xh, ssrq);
                double f = getyyghid() + 1;
                String yyid = f.ToString();
                Insertyygh(yyid, "01", "0000", yyghrq, brxm, brxb, "", brnl,
                    "1", sfzh, jtzz, sj, yyys, yyks, yydjrq, yyyxrq, "0001");
                int j = getdatedifference(list2[2]);
                if (j > 1)
                {
                    updateYwxhb2("000001", ssrq);
                    double q = yyid1 + 1;
                    String brid = q.ToString();
                
                    Insertbrzc(brid, brxm, brxb, brnl, sfzh, jtzz, sj, zcrq);
                    updateyyghbrid(brid, yyid);

                }
                else {
                   
               
                updateYwxhb2(bridxh, ssrq);
                double f1 = yyid1 + k1;
                String brid = f1.ToString();
           
                Insertbrzc(brid, brxm, brxb, brnl, sfzh, jtzz, sj, zcrq);
                updateyyghbrid(brid, yyid);
                }


            }
            else
            {
                yy = xh1;
            updateYwxhb1(xh1, ssrq);
            double f1 = yyid1 + m ;
            String yyid = f1.ToString();
            
            Insertyygh(yyid, "01", "0000", yyghrq, brxm, brxb, "", brnl,
                "1", sfzh, jtzz, sj, yyys, yyks, yydjrq, yyyxrq, "0001");
           
           int ww = getdatedifference(list2[2]);
                        if (ww > 1)
                        {
                            updateYwxhb2("000001", ssrq);
                            double q = yyid1 + 1;
                            String brid = q.ToString();
                         
                            Insertbrzc(brid, brxm, brxb, brnl, sfzh, jtzz, sj, zcrq);
                            updateyyghbrid(brid, yyid);

                        }
                        else
                        {
                            updateYwxhb2(bridxh, ssrq);
                            double f2 = yyid1 + k2;
                            String brid = f2.ToString();
                            Insertbrzc(brid, brxm, brxb, brnl, sfzh, jtzz, sj, zcrq);
                            updateyyghbrid(brid, yyid);
                        }

               
            
            }
            



            return "chenggong" ;
        }


        /**获取单个就诊人信息**/
        public List<String> getuserfriendidsingle(string sfzh)
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select * from gyb_user_friend where sfzh='"+sfzh+"'");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString()); 

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }



        /**获取简单预约挂号信息**/
        public List<String> getsamplefriendinfo(string manageid)
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select  ghb_yygh.brxm,ghb_yygh.sfzh, ghb_yygh.yyys,gyb_czy.czyxm,(select  CONVERT(varchar,ghb_yygh.yyghrq,120))as '预约日期明细',ghb_yygh.sj,gyb_ks.ksmc,(select  CONVERT(varchar(10),ghb_yygh.yyghrq,120))as '预约日期', ghb_yygh.yyghid,ghb_yygh.brid,gyb_czy.mzsbdd from ghb_yygh,gyb_czy,gyb_ks ,gyb_user where gyb_czy.czybm=ghb_yygh.yyys and gyb_ks.ksbm=ghb_yygh.yyks and(ghb_yygh.sfzh=gyb_user.friendid1 or ghb_yygh.sfzh=gyb_user.friendid2 or ghb_yygh.sfzh=gyb_user.friendid3)and ghb_yygh.yyghrq>(SELECT DISTINCT GetDate ( ) FROM sysfilegroups )and gyb_user.userid='" + manageid + "'");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString());

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }

        /**获取输入时间和当前时间的天数差**/
        public int getdatedifference(string date)
        {
           int k=0;
            String s="";
          DateTime time = Convert.ToDateTime(date);
          String mm=time.ToString("yyyy-MM-dd HH:mm:ss",DateTimeFormatInfo.InvariantInfo);

            try
            {

                string sql = String.Format(@" SELECT DATEDIFF(day, '"+mm+"', getdate()) ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    s=reader[0].ToString();
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
                return 55555;
            }
            k = int.Parse(s);
            return k;
        }
        /**获取当前日期**/
        public String getdatetime()
        {
            string k = "";

            try
            {

                string sql = String.Format(@" SELECT DISTINCT GetDate ( ) FROM sysfilegroups   ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    k = reader[0].ToString();
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
                return "sssssssssss";
            }
            DateTime time = Convert.ToDateTime(k);
            String mm = time.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            return mm;
        }

        /**获取预约挂号ID**/
        public double getyyghid()
        {
            String f = "";

            try
            {

                string sql = String.Format(@"select CONVERT(varchar(12) , getdate(), 112 ) ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    f = reader[0].ToString();


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
             
            }
            f = f + "000000";
           
            return double.Parse(f); ;
        }
    

  
        /**插入预约挂号表**/
        public bool Insertyygh( String yyghid,String ywckbm,String czybm,String yyghrq,String brxm,String brxb,String brsr,String brnl,String brnldw,String sfzh,String jtzz,String sj,String yyys, String yyks, String yydjrq, String yyyxrq, String czyks)
        {


            try
            {
                string sql = String.Format(@"BEGIN TRAN  INSERT INTO ghb_yygh ( yyghid, ywckbm, czybm, yyghrq, brxm, brxb, brsr, brnl, brnldw, sfzh, jtzz, sj,yyys, yyks, yydjrq, yyyxrq, czyks ) VALUES ( '" + yyghid + "', '" + ywckbm + "', '" + czybm + "', '" + yyghrq + "', '" + brxm + "', '" + brxb + "', '" + brsr + "'," + brnl + ", '" + brnldw + "', '" + sfzh + "', '" + jtzz + "', '" + sj + "', '" + yyys + "', '" + yyks + "', '" + yydjrq + "', '" + yyyxrq + "', '" + czyks + "' )COMMIT TRAN");

              
            
                SqlCommand mcmd = new SqlCommand(sql, sqlCon);
               //using (cmd = new SqlCommand(sql, sqlCon))
                  

                       mcmd.ExecuteNonQuery();
                     
                       mcmd.Dispose();

                       return true;
                

            }
            catch (Exception)
            {
                return false;
            }

        }


        /**插入用户注册表**/
        public bool Insertbrzc(String brid,String brxm, String brxb,  String brnl, String sfzh, String jtzz, String sj, String zcrq)
        {


            try
            {
                string sql = String.Format(@"     INSERT into ghb_zcxx ( brid , czybm , ywckbm , brxm , pydm , brxb , brsr , brnl , brnldw , brxx , sfzh , jtzz , gzdw , dwdz , sj , email , yzbm , lxr , lxrdh , zcrq , czyks )VALUES ( '"+brid+"' , '0000' , '01' , '"+brxm+"' , 'ZRT' , '"+brxb+"' , null , '"+brnl+"' , '1' , null , '"+sfzh+"' ,'"+jtzz+"' , null , null , '"+sj+"' , null , null , null , null , '"+zcrq+"' , '0001' ) ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /**更新挂号表表**/
        public bool updateyyghbrid(String brid, String yyghid)
        {


            try
            {
                string sql = String.Format(@"     update ghb_yygh set brid ='"+brid+"' where yyghid ='"+yyghid+"' ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /**为病人注册做准备**/
        public List<String> getusertime()
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"   SELECT Top 1 xhlx , xh , ssrq , cslx From ghb_ywxhb Where xhlx ='brid' ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                   
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }/**获取和生成业务序号**/
        public String getywxuxhyyid(int  xh1)
        {
            String ss = "";

            try
            {

                string sql = String.Format(@"  declare @book_code varchar(50)set @book_code=(select max(right('000000',6)) where '000000' like '%')+"+xh1+" set @book_code=''+right('000000'+@book_code,6) select @book_code");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                  ss=(reader[0].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            System.Console.Write(ss);
            string kk = ss;
            return ss;

        }
        /**获取和生成业务序号**/
        public List<String> getywxuxhbrid()
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@" SELECT Top 1 xhlx , xh , ssrq , cslx From ghb_ywxhb Where xhlx ='brid'  ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }

        /**获取和业务序号表中的yyid**/
        public List<String> getywxuxhyyid()
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@" SELECT Top 1 xhlx , xh , ssrq , cslx From ghb_ywxhb Where xhlx ='yyghid'  ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }




        /**通过医生账号获取科室编码**/
        public String getksbmbyyszh(String czybm)
        {
            string k = "";

            try
            {

                string sql = String.Format(@"select ksbm from gyb_czy where czybm='"+czybm+"' ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    k = reader[0].ToString();


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
                return "sssssssssss";
            }

            return k;
        }



        /**获取预约明细中医生的上下班时间**/
        public List<String> getyssbbyyymx(String ysbm,String yyghrq)
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select (SELECT CONVERT(VARCHAR(5),(select  sbsj from gyb_bcfa where bcfabm=(select top 1 bcfabm from ghb_yzhb where ghb_yzhb.Yzrq>= (SELECT DATEADD(day, +0, (select convert(varchar(10),'"+yyghrq+"',120)+' 00:00:00.000')))and ghb_yzhb.Yzrq< (SELECT DATEADD(day, +0, (select convert(varchar(10),'"+yyghrq+"',120)+' 23:59:00.000')))and ghb_yzhb.czybm='"+ysbm+"')),8))as'上班时间', (SELECT CONVERT(VARCHAR(5),(select  xbsj from gyb_bcfa where bcfabm=(select top 1 bcfabm from ghb_yzhb where ghb_yzhb.Yzrq>= (SELECT DATEADD(day, +0, (select convert(varchar(10),'"+yyghrq+"',120)+' 00:00:00.000')))and ghb_yzhb.Yzrq< (SELECT DATEADD(day, +0, (select convert(varchar(10),'"+yyghrq+"',120)+' 23:59:00.000')))and ghb_yzhb.czybm='"+ysbm+"' order by bcfabm desc)),8))as'下班时间' ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    


                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }


        /**获取科室与医生信息JSOn**/
        public String getksys()
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select czyxm ,ksmc from gyb_czy,gyb_ks where gyb_czy.ksbm=gyb_ks.ksbm ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());



                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            ProductList productlist = new ProductList();
            List<Product> products = new List<Product>();
           for (int i=0;i<list.Count();i=i+2){
           Product pro=  new Product(){Name=list[i],Ks=list[i+1]};
           products.Add(pro);
           

        } productlist.GetProducts = products;  
          return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(productlist);

    


        }

        /**获取科室简介json**/
        public String getksjj()
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select ksbm,ksmc,ksjj from gyb_ks where ksjj IS not NULL");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            KsjjList ksjjlist = new KsjjList();
            List<Ksjj> ksjjs = new List<Ksjj>();
            for (int i = 0; i < list.Count(); i = i + 3)
            {
                Ksjj pro = new Ksjj() { Kabm = list[i], Ksmc = list[i + 1], Ksjjmx= list[i + 2] };
                ksjjs.Add(pro);     
            } ksjjlist.GeKsjjs = ksjjs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ksjjlist);
        }

        
        /**获取常用就诊人json**/
        public String getkjzr(String phone)
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select * from gyb_user_friend where ph="+phone+" ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
          

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            CylxrList cylxrlist = new CylxrList();
            List<Cylxr> cylxrs = new List<Cylxr>();
            for (int i = 0; i < list.Count(); i = i + 7)
            {
                Cylxr pro = new Cylxr() { sfzh = list[i], brxm = list[i + 1], brnl = list[i + 2], brxb = list[i + 3], brjtzz = list[i + 4], ph = list[i + 5], brdh = list[i + 6] };
                cylxrs.Add(pro);
            } cylxrlist.GetCylxr = cylxrs;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cylxrlist);
        }
        /**获取满意度调查json**/
        public String getmyddcxm()
        {
            List<string> list = new List<string>();

            try
            {
                string sql = String.Format(@"select * from gyb_myddcxm");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            MyddcxmList myddcxmlist = new MyddcxmList();
            List<Myddcxm> myddcxms = new List<Myddcxm>();
            for (int i = 0; i < list.Count(); i = i + 3)
            {
                Myddcxm pro = new Myddcxm() { myddcid = list[i], myddcmc = list[i + 1], myddcqybz = list[i + 2] };
                myddcxms.Add(pro);
            } myddcxmlist.Gemyddcxms = myddcxms;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(myddcxmlist);
        }
        /** 插入满意度调查结果表
         * **
         * */
        public bool insertUserMyddc(String yhid, String myddcid, String dcfs, String data1)
        {


            try
            {
                string sql = "insert into gyb_user_myddc(yhid,myddcid,dcfs,date1)values('" + yhid + "','" + myddcid + "','" + dcfs + "','" + data1 + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /** 插入满意度调查结果建议
         * **
         * */
        public bool insertUserMyddcYhjy(String yhid, String yhjy)
        {
            try
            {
                string sql = "insert into gyb_user_yhjy(yhid, yhjy)values('" + yhid + "','" + yhjy + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /**删除挂号表表**/
        public bool deleteyyxx(String brid, String yyghid)
        {


            try
            {
                string sql = String.Format(@"  DELETE FROM ghb_yygh WHERE yyghid = '"+yyghid+"' AND brid = '"+brid+"' ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /**获取常用就诊人详细信息**/
        public List<String> getfriendmx(String sfzh, String brxm)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select * from gyb_user_friend where sfzh='"+sfzh+"' and brxm='"+brxm+"' ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }

        /**根据登录用户名获取常用就诊人ID**/
        public List<String> getfriendid(String userid)
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@" select RTRIM (friendid1), RTRIM (friendid2), RTRIM (friendid3) from gyb_user where userid='"+userid+"'");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());

                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }
            return list;
        }
        /**删除常用就诊人信息**/
        public bool deletefriend(String userid, String sfzh)
        {
            List<string> list1 = new List<string>();
          
            String friendid="";
       
            

            int j=512;
            try
            {
                try
                {
                    list1 = getfriendid(userid);
                }
                catch { }

                for (int i = 0; i < list1.Count(); i++)
                {
                    if (list1[i].Equals(sfzh))
                    {
                        j = i;
                        break;
                    }
                }}
             catch { }

                if (j == 0)
                {
                    friendid = "friendid1";
                }
                else if (j == 1)
                {
                    friendid = "friendid2";
                }
                else if (j == 2)
                {
                    friendid = "friendid3";
                }
            
           
            updatefriendID(friendid, userid);
            

                try
                {
                    string sql = String.Format(@"  delete gyb_user_friend where sfzh='" + sfzh + "'COMMIT TRAN ");
                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

        }

        /**更新用户表中常用就诊人**/
        public bool updatefriendID(String friendid, String userid)
        {
           

            try
            {
                string sql = String.Format(@"BEGIN TRAN  update  gyb_user set " + friendid + "=null where userid='" + userid + "' ");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /**根据常用就诊人获取住院号**/
        public List<String> getzygbysfzg(String sfzh)
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select  convert(varchar(10),ryrq ,120) ,bagl_basy_jbxx.zyh from bagl_basy_jbxx,bagl_basy_zdxx where sfzh='" + sfzh + "'and  bagl_basy_zdxx.zyh=bagl_basy_jbxx.zyh ");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }
        /**根据常用就诊人获取挂号序号**/
        public List<String> getghxhbysfzg(String sfzh)
        {
            List<string> list = new List<string>();

            try
            {

                string sql = String.Format(@"select ghb_brgh.brid ,ghxh ,convert(varchar(10),ghrq ,120) from ghb_brgh,ghb_zcxx where sfzh='" + sfzh + "' and ghb_brgh.brid=ghb_zcxx.brid");

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                }

                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {

            }

            return list;
        }

        /**根据身份证号获取住院病人信息**/
        public List<String> getfriendinfobysfzh(String sfzh)
        { List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select  convert(varchar(10),ryrq ,120) ,bagl_basy_jbxx.zyh,bagl_basy_jbxx.bah,bagl_basy_jbxx.brxm,bagl_basy_jbxx.brxb,bagl_basy_jbxx.brnl,bagl_basy_jbxx.csrq,bagl_basy_jbxx.gzdwjdz,bagl_basy_jbxx.lxrxm,bagl_basy_jbxx.lxrdh,bagl_basy_jbxx.lxrdz,bagl_basy_zdxx.ryks,bagl_basy_zdxx.cyks,bagl_basy_zdxx.zzys,bagl_basy_zdxx.zrhs from bagl_basy_jbxx,bagl_basy_zdxx where sfzh='"+sfzh+"'and  bagl_basy_zdxx.zyh=bagl_basy_jbxx.zyh");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {   list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[11].ToString());
                    list.Add(reader[12].ToString());
                    list.Add(reader[13].ToString());
                    list.Add(reader[14].ToString());
                }
                reader.Close();
                cmd.Dispose();  }
            catch (Exception)
            { }
            return list; }

        /**根据身份证号获取门诊病人信息**/
        public List<String> getmzfriendinfobysfzh(String sfzh)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select ghb_zcxx.brid,ghb_zcxx.brnl,ghb_zcxx.czybm ,ghb_zcxx.sfzh,ghb_zcxx.brxb,ghb_zcxx.brxm,ghb_zcxx.jtzz,ghb_zcxx.gzdw,ghb_zcxx.sj,ghb_zcxx.brsr,ghb_zcxx.brnldw,ghb_zcxx.lxrdh,ghb_brgh.ghxh,ghb_brgh.ghzlbm,ghb_brgh.yyghid,ghb_brgh.ghrq,ghb_brgh.ghks,ghb_brgh.jzys,ghb_brgh.zyh from ghb_zcxx,ghb_brgh where ghb_zcxx.sfzh='"+sfzh+"' and ghb_zcxx.brid=ghb_brgh.brid");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[11].ToString());
                    list.Add(reader[12].ToString());
                    list.Add(reader[13].ToString());
                    list.Add(reader[14].ToString());
                    list.Add(reader[15].ToString());
                    list.Add(reader[16].ToString());
                    list.Add(reader[17].ToString());
                    list.Add(reader[18].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { }
            return list;
        }

          /**根据身份证号获取门诊病人信息JSON**/
        public String  getmzfriendinfobysfzhtoJson(String sfzh)
        {List<string> list = new List<string>();
            list = getmzfriendinfobysfzh(sfzh);
           MzbrjbxxList mzbrjbxxList = new MzbrjbxxList();
            List<Mzbrjbxx> myddcxms = new List<Mzbrjbxx>();
            for (int i = 0; i < list.Count(); i = i + 19)
            { Mzbrjbxx pro = new Mzbrjbxx() { brid = list[i], brnl = list[i + 1],czybm = list[i + 2],sfzh = list[i + 3],brxb = list[i + 4], brxm = list[i + 5],jtzz = list[i + 6], gzdw = list[i + 7], sj = list[i + 8], brsr = list[i + 9], brnldw = list[i + 10],lxrdh = list[i + 11], ghxh = list[i +12], ghzlbm = list[i + 13], yyghid= list[i + 14], ghrq = list[i +15],ghks = list[i + 16], jzys = list[i + 17], zyh = list[i + 18]
                };
               myddcxms.Add(pro);
            } mzbrjbxxList.GetMzbrjbxx = myddcxms;
            return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(mzbrjbxxList);
        }
        //通过挂号序号或者住院号判断设备名称
        public List<String> checkdevices(String cxh)
        {
            List<string> list = new List<string>();
            try
            {

                string sql = String.Format(@"select ris_patient.PATIENTID, ris_patient.PHOTONO,ris_patient.NAME,ris_patient.SEX,ris_patient.CLINICNO,ris_patient.INPATIENTNO,  ris_studies.AGE,ris_studies.LODGESECTION,ris_studies.LODGEDOCTOR,ris_studies.LODGEDATE,ris_studies.CLASSNAME,ris_studies.STATUS,ris_studies.CHECKDATE,ris_studies.CHECKDOCTOR,ris_studies.PARTOFCHECK,ris_StudyReport.studyReportID,ris_StudyReport.reportDate,ris_StudyReport.reportDoctor from ris_patient,ris_studies,ris_StudyReport where (ris_patient.CLINICNO='"+cxh+"'OR ris_patient.INPATIENTNO='"+cxh+"')AND ris_patient.PATIENTID=ris_studies.PATIENTID and ris_patient.PATIENTID=ris_StudyReport.PATIENTID AND( ris_studies.STATUS <> '已作废' )");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);   
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {   list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[11].ToString());
                    list.Add(reader[12].ToString());
                    list.Add(reader[13].ToString());
                    list.Add(reader[14].ToString());
                    list.Add(reader[15].ToString());
                    list.Add(reader[16].ToString());
                    list.Add(reader[17].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {

            }
         return list;
        }
        //检查报告
        public List<String> jcbg(String sfzh)
        {
            //门诊病人基本信息
            List<string> list = new List<string>();
            //住院病人基本信息
            List<string> list1 = new List<string>();
            //保存住院号和门诊号
            List<string> list2 = new List<string>();
            List<string> list3= new List<string>();
            //检查信息保存
            List<string> list5 = new List<string>();
            //
            list = getmzfriendinfobysfzh(sfzh);
            if (list.Count>2)
            {
                for (int i = 0; i < list.Count; i = i + 19)
                {
                    list2.Add(list[i + 12]);
                }
            }
            else { }
           list1 = getfriendinfobysfzh(sfzh);
           if (list1.Count>2)
           {
               for (int j = 0; j < list1.Count; j = j + 15)
               {
                   list2.Add(list1[j + 1]);
               }
           }
           else { }
           String sk = list2[1] + "  " + list2[2] + "   " + list2[3] + " ";
           if (list2.Count > 0) {
               for (int k = 0; k < list2.Count; k++) {
                   List<string> list4 = new List<string>();
                   list4 = checkdevices(list2[k]);
                   if (list4.Count > 2) {
                       for (int m = 0; m < list4.Count; m++) {
                           list5.Add(list4[m]);
                       }
                   }
               }
           }

             return list5;
        }

        /**根据手机号或者brid获取病人预约信息返回JSON**/
        public String getyyxxbysjtojson(String sj)
        {
            List<string> list = new List<string>();
   try
            {
                string sql = String.Format(@"select yyghid ,brid,yyghrq,brxm,brxb,brnl ,brnldw,jtzz,mzsbdd,zcmc ,ysxm,ksmc from VIEW_YYGH_YHXX   where yyghrq >GETDATE()and (sj='"+sj+"' or brid='"+sj+"')");
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { list.Add(reader[0].ToString()); list.Add(reader[1].ToString()); list.Add(reader[2].ToString()); list.Add(reader[3].ToString()); 
                  list.Add(reader[4].ToString()); list.Add(reader[5].ToString()); list.Add(reader[6].ToString()); list.Add(reader[7].ToString()); 
                 list.Add(reader[8].ToString()); list.Add(reader[9].ToString());  list.Add(reader[10].ToString());  list.Add(reader[11].ToString()); 
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                
            }
                  YYXXList  yyxxList = new YYXXList();
                  List<YYXX> yyxxs = new List<YYXX>();
                  int i = 0;
                  for (i = 0; i < list.Count();i=i+12)
                  {
                      YYXX yyxx = new YYXX()
                      {
                          yyghid = list[i],
                          brid = list[i + 1],
                          yyghrq = list[i + 2],
                          brxm = list[i + 3],
                          brxb = list[i + 4],
                          brnl = list[i + 5],
                          brnldw = list[i + 6],
                          jtzz = list[i + 7],
                          mzsbdd = list[i + 8],
                          zcmc = list[i + 9],
                          ysxm = list[i + 10],
                          ksmc = list[i + 11]
                      }; yyxxs.Add(yyxx); }yyxxList.GetYyxx = yyxxs;
                      return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(yyxxList);
                  }

        /** 获取预约挂号的日期和星期**/
        public List<String> getyyrqandxq()
        { List<string> list = new List<string>();
            try
            {
                string sql = String.Format(@"select (select right(convert(varchar(10),getdate(),120),5)),(SELECT datename(weekday,getdate())),(select right(convert(varchar(10),getdate()+1,120),5)),(SELECT datename(weekday,getdate()+1)),(select right(convert(varchar(10),getdate()+2,120),5)),(SELECT datename(weekday,getdate()+2)),(select right(convert(varchar(10),getdate()+3,120),5)),(SELECT datename(weekday,getdate()+3)),(select right(convert(varchar(10),getdate()+4,120),5)),(SELECT datename(weekday,getdate()+4)),(select right(convert(varchar(10),getdate()+5,120),5)),(SELECT datename(weekday,getdate()+5)),(select right(convert(varchar(10),getdate()+6,120),5)),(SELECT datename(weekday,getdate()+6))");
               SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {   list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[9].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[11].ToString());
                    list.Add(reader[12].ToString());
                    list.Add(reader[13].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
       return list;
        }
          


    }
}