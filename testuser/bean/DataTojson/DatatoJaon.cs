using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

namespace testuser.bean.DataTojson
{
    public class DatatoJaon
    {
        public string DataTableToJsonList(DataTable dt, DataTable list)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str_cname = dt.Columns[j].ColumnName;
                    if (str_cname == "rowno")
                    {
                        continue;
                    }
                    //递归调用
                    if (str_cname == "list" && list != null)
                    {
                        jsonBuilder.Append("\"list\":");
                        jsonBuilder.Append(DataTableToJsonList(list, null));
                        jsonBuilder.Append(",");
                        continue;
                    }
                    string str_type = dt.Columns[j].DataType.ToString();
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(str_cname);
                    jsonBuilder.Append("\":\"");
                    //日期类型处理格式
                    if (str_type.Equals("System.DateTime"))
                    {
                        string str_data = dt.Rows[i][j].ToString() == null ? "" : dt.Rows[i][j].ToString().Trim();
                        if (!str_data.Equals(""))
                        {
                            DateTime ld_rq = Convert.ToDateTime(str_data);
                            jsonBuilder.Append(ld_rq.ToString("yyyy-MM-dd"));
                        }
                    }
                    else
                    {
                        jsonBuilder.Append(dt.Rows[i][j].ToString() == null ? "" : dt.Rows[i][j].ToString().Trim());
                    }
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }
    }
}