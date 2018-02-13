using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FMTDP.Tools;
using FocusFilmDBTool;
using Microsoft.Extensions.Configuration;

namespace FMTDP.DAL
{
    public abstract class BaseDAL
    {
        private static string _ConnectionString;
        private IConfiguration _Configuration;
        public BaseDAL(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        protected virtual T GetSingle<T>(string sql, object param, SqlTransaction tran = null)
        {
            if (tran != null)
            {
                return tran.Connection.QueryFirstOrDefault<T>(sql, param, tran);
            }
            else
            {
                using (var conn = GetDbConnection())
                {
                    return conn.QueryFirstOrDefault<T>(sql, param);
                }
            }
        }
        protected virtual List<T> GetList<T>(string sql, object param, SqlTransaction tran = null)
        {
            if (tran != null)
            {
                return tran.Connection.Query<T>(sql, param, tran).ToList();
            }
            else
            {
                using (var conn = GetDbConnection())
                {
                    return conn.Query<T>(sql, param).ToList();
                }
            }
        }
        protected virtual bool Execute(string sql, object param, SqlTransaction tran = null)
        {
            if (tran != null)
            {
                return tran.Connection.Execute(sql, param, tran) >= 0;
            }
            else
            {
                using (var conn = GetDbConnection())
                {
                    return conn.Execute(sql, param) >= 0;
                }
            }
        }
        protected virtual bool BUlkToDB<T>(List<T> param, string tableName, SqlTransaction tran = null)
        {
            DataTable dt = BaseTool.GetTable(param);
            using (var conn = GetDbConnection())
            {
                SqlBulkCopy bulkCopy;
                if (tran != null)
                {
                    bulkCopy = new SqlBulkCopy(tran.Connection, SqlBulkCopyOptions.Default, tran);
                }
                else
                {
                    bulkCopy = new SqlBulkCopy(conn);
                }
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BatchSize = dt.Rows.Count;
                try
                {
                    if (dt != null && dt.Rows.Count != 0)
                        bulkCopy.WriteToServer(dt);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private SqlConnection GetDbConnection()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString());
            return conn;
        }
        private string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_ConnectionString))
            {
                _ConnectionString = _Configuration.GetSection("FMTDP").Value;
            }
            return _ConnectionString;
        }
    }
}
