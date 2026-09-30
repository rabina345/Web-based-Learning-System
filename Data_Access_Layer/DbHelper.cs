using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SkillCore.Data_Access_Layer
{
    // Central place for all ADO.NET plumbing. No business logic here —
    // this class only knows how to talk to SQL Server. Every other DAL
    // class in this project uses this instead of opening its own connections.
    public static class DbHelper
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["SkillCoreDB"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static SqlCommand CreateCommand(SqlConnection con, string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandTimeout = 30;
            return cmd;
        }

        public static void AddParam(SqlCommand cmd, string name, object value)
        {
            cmd.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }

        // ---- Safe readers: turn database NULL into a usable C# value ----

        public static string GetString(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? null : r.GetString(i);
        }

        public static int GetInt(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? 0 : r.GetInt32(i);
        }

        public static int? GetNullableInt(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? (int?)null : r.GetInt32(i);
        }

        public static bool GetBool(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? false : r.GetBoolean(i);
        }

        public static DateTime GetDate(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? DateTime.MinValue : r.GetDateTime(i);
        }

        public static DateTime? GetNullableDate(IDataRecord r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? (DateTime?)null : r.GetDateTime(i);
        }
    }
}