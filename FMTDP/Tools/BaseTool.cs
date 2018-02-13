using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FMTDP.Tools
{
    public class BaseTool
    {
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP(HttpContext httpContext)
        {
            var ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = httpContext.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 计算文件MD5
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string GetMD5(string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(file);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public decimal GetSimilarityDegree(string str1, string str2)
        {
            decimal Kq = 2;
            decimal Kr = 1;
            decimal Ks = 1;
            char[] ss = str1.ToCharArray();
            char[] st = str2.ToCharArray();
            //获取交集数量
            int q = ss.Intersect(st).Count();
            int s = ss.Length - q;
            int r = st.Length - q;
            return Math.Round(Kq * q / (Kq * q + Kr * r + Ks * s), 2);
        }


        /// <summary>
        /// entities to Table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static DataTable GetTable<T>(List<T> entities)
        {
            DataTable dt = new DataTable();
            Type t = typeof(T);
            // 获得此模型的公共属性  
            PropertyInfo[] propertys = t.GetProperties();
            foreach (var n in propertys)
            {
                var column = new DataColumn();
                column.AllowDBNull = true;
                column = new DataColumn(n.Name); //由于 可空类型存在，此处未定义类型
                dt.Columns.Add(column);
            }
            foreach (var n in entities)
            {
                object[] entityValues = new object[propertys.Length];
                for (int i = 0; i < propertys.Length; i++)
                {
                    entityValues[i] = propertys[i].GetValue(n, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }
    }
}
