﻿using System;
namespace TM.Desktop
{
    /*
    public class OleDBF
    {
        public static string DataSource { get; set; }
        public static string GetConnectionString(string DataSource, string version)
        {
            DataSource = Path.Combine(DataSource);
            if (version == "dBASE" || version == "1")
                return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataSource + ";Extended Properties=dBASE IV";
            else if (version == "MS" || version == "2")
                return @"Driver={Microsoft dBASE Driver (*.dbf)};DriverID=277;Dbq=" + DataSource + ";";
            else
                return "Provider=VFPOLEDB;Data Source=" + DataSource + ";Collating Sequence=machine;";
        }
        public static string GetConnectionString(string DataSource)
        {
            return GetConnectionString(DataSource, "0");
        }
        public static string GetConnectionString()
        {
            return GetConnectionString(DataSource, "0");//"VF"
        }
        public static string GetConnectionStringVersion(string version)
        {
            return GetConnectionString(DataSource, version);
        }
        public static OleDbConnection Connection(string DataSource, string version)
        {
            try
            {
                //using (OleDbConnection con = new OleDbConnection(GetConnectionString(DataSource, version)))
                //{
                OleDbConnection con = new OleDbConnection(GetConnectionString(DataSource, version));
                if (con.State == System.Data.ConnectionState.Open) con.Close();
                con.Open();
                return con;
                //}
            }
            catch (Exception) { throw; }
        }
        public static OleDbConnection Connection(string DataSource)
        {
            return Connection(DataSource, "0");
        }
        public static OleDbConnection Connection()
        {
            return Connection(DataSource, "0");
        }
        public static void ConnectionOpen(string DataSource, string version)
        {
            try
            {
                if (Connection(DataSource, version) != null && Connection(DataSource, version).State == System.Data.ConnectionState.Closed) Connection(DataSource, version).Open();
            }
            catch (Exception) { throw; }
        }
        public static void ConnectionOpen(string DataSource)
        {
            ConnectionOpen(DataSource, "0");
        }
        public static void ConnectionOpen()
        {
            ConnectionOpen(DataSource, "0");
        }
        public static void ConnectionClose(string DataSource, string version)
        {
            try
            {
                if (Connection(DataSource, version) != null && Connection(DataSource, version).State == System.Data.ConnectionState.Open) { Connection(DataSource, version).Close(); }//Connection().Dispose(); } }
            }
            catch (Exception) { throw; }
        }
        public static void ConnectionClose(string DataSource)
        {
            ConnectionClose(DataSource, "0");
        }
        public static void ConnectionClose()
        {
            ConnectionClose(DataSource, "0");
        }
        public OleDbCommand getCommand(string DataSource, string version, string query)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (OleDbConnection con = Connection(DataSource, version))
                {
                    using (OleDbCommand cmd = new OleDbCommand(query))
                    {
                        cmd.Connection = con;
                        return cmd;
                    }
                }
            }
            catch (Exception) { throw; }
            finally { ConnectionClose(); }
        }
        public OleDbCommand getCommand(string DataSource, string query)
        {
            return getCommand(DataSource, "0", query);
        }
        public OleDbCommand getCommand(string query)
        {
            return getCommand(DataSource, "0", query);
        }
        public static bool Execute(string DataSource, string version, OleDbCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (OleDbConnection con = Connection(DataSource, version))
                {
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception) { throw; }
            finally { ConnectionClose(); }
        }
        public static bool Execute(string DataSource, OleDbCommand cmd)
        {
            return Execute(DataSource, "0", cmd);
        }
        public static bool Execute(OleDbCommand cmd)
        {
            return Execute(DataSource, "0", cmd);
        }
        public static bool Execute(string DataSource, string version, string sql, string extraEX)
        {
            try
            {
                extraEX = extraEX != null ? extraEX + ": " : "";
                //ClearCache();
                //Connection().Open();
                using (OleDbConnection con = Connection(DataSource, version))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sql, con))
                    {
                        int i = cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(extraEX + ex.Message); }
            finally { ConnectionClose(); }
        }
        public static bool Execute(string DataSource, string sql, string extraEX)
        {
            return Execute(DataSource, "0", sql, extraEX);
        }
        public static bool Execute(string sql, string extraEX)
        {
            return Execute(DataSource, "0", sql, extraEX);
        }
        public static bool Execute(string sql)
        {
            return Execute(DataSource, "0", sql, null);
        }
        public static OleDbDataReader ToDataReader(string DataSource, string version, OleDbCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (OleDbConnection con = Connection(DataSource, version))
                {
                    cmd.Connection = con;
                    using (OleDbDataReader datareader = cmd.ExecuteReader())
                    {
                        return cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception) { throw; }
            finally { ConnectionClose(); }
        }
        public static OleDbDataReader ToDataReader(string DataSource, OleDbCommand cmd)
        {
            return ToDataReader(DataSource, "0", cmd);
        }
        public static OleDbDataReader ToDataReader(OleDbCommand cmd)
        {
            return ToDataReader(DataSource, "0", cmd);
        }
        public static OleDbDataReader ToDataReader(string DataSource, string version, string sql)
        {
            try
            {
                //ClearCache();
                //using (OleDbConnection con = Connection(DataSource, version))
                //{
                //using (OleDbCommand cmd = new OleDbCommand(sql, con))
                //{
                OleDbConnection con = Connection(DataSource, version);
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //OleDbDataReader reader = cmd.ExecuteReader();
                return cmd.ExecuteReader();
                //using (OleDbDataReader reader = cmd.ExecuteReader())
                //{
                //    return cmd.ExecuteReader();
                //}
                //}
                //}
            }
            catch (Exception) { throw; }
            finally { ConnectionClose(); }
        }
        public static OleDbDataReader ToDataReader(string DataSource, string sql)
        {
            return ToDataReader(DataSource, "0", sql);
        }
        public static OleDbDataReader ToDataReader(string sql)
        {
            return ToDataReader(DataSource, "0", sql);
        }
        public static System.Collections.Generic.List<object> ToList(string DataSource, string version, string sql)
        {
            try
            {
                var rs = new System.Collections.Generic.List<object>();
                //ClearCache();
                //using (OleDbConnection con = Connection(DataSource, version))
                //{
                //using (OleDbCommand cmd = new OleDbCommand(sql, con))
                //{
                OleDbConnection con = Connection(DataSource, version);
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = System.Data.CommandType.Text;
                //OleDbDataReader reader = cmd.ExecuteReader();
                //return cmd.ExecuteReader();
                return rs;
                //using (OleDbDataReader reader = cmd.ExecuteReader())
                //{
                //    return cmd.ExecuteReader();
                //}
                //}
                //}
            }
            catch (Exception) { throw; }
            finally { ConnectionClose(); }
        }
        public static System.Data.DataTable ToDataTable(string DataSource, string version, OleDbCommand cmd)
        {
            try
            {
                //ClearCache();
                //Connection().Open();
                using (OleDbConnection con = Connection(DataSource, version))
                {
                    using (cmd.Connection = con)
                    {
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            using (System.Data.DataTable dt = new System.Data.DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
            finally { ConnectionClose(); }
        }
        public static System.Data.DataTable ToDataTable(string DataSource, OleDbCommand cmd)
        {
            return ToDataTable(DataSource, "0", cmd);
        }
        public static System.Data.DataTable ToDataTable(OleDbCommand cmd)
        {
            return ToDataTable(DataSource, "0", cmd);
        }
        public static System.Data.DataTable ToDataTable(string DataSource, string version, string sql, string extraEX)
        {
            extraEX = extraEX != null ? extraEX + ": " : "";
            try
            {
                using (OleDbConnection con = Connection(DataSource, version))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sql, con))
                    {
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            using (System.Data.DataTable dt = new System.Data.DataTable())
                            {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception(extraEX + ex.Message); }
            finally { ConnectionClose(); }
        }
        public static System.Data.DataTable ToDataTable(string DataSource, string sql, string extraEX)
        {
            return ToDataTable(DataSource, "0", sql, extraEX);
        }
        public static System.Data.DataTable ToDataTable(string sql, string extraEX)
        {
            return ToDataTable(DataSource, "0", sql, extraEX);
        }
        public static System.Data.DataTable ToDataTable(string sql)
        {
            return ToDataTable(DataSource, "0", sql, null);
        }
        public static System.Collections.ArrayList ExportDBF(string DataSource, string filename, System.Data.DataTable dt)
        {
            var list = CreateTable(DataSource, filename, dt);
            var listErr = new System.Collections.ArrayList();
            var Err = "";
            foreach (System.Data.DataRow row in dt.Rows)
            {
                try
                {
                    Err = "";
                    string sql = "insert into " + filename + " values(";
                    foreach (System.Data.DataColumn dc in dt.Columns)
                    {
                        Err += row[dc.ColumnName].ToString();
                        if (dc.DataType.ToString() == "System.Int32" || dc.DataType.ToString() == "System.Double")
                            sql += row[dc.ColumnName].ToString() + ",";
                        else
                            sql += "'" + row[dc.ColumnName].ToString() + "',";
                    }
                    //for (int i = 0; i < list.Count; i++)
                    //    sql += "'" + row[list[i].ToString()].ToString() + "',";
                    sql = sql.Trim(',') + ")";
                    Execute(sql);
                }
                catch (Exception)
                {
                    listErr.Add(Err);
                }
            }
            return listErr;
        }
        public static System.Collections.ArrayList ExportDBF2(string DataSource, string filename, System.Data.DataTable dt)
        {
            var listErr = new System.Collections.ArrayList();
            var Err = "";
            foreach (System.Data.DataRow row in dt.Rows)
            {
                try
                {
                    Err = "";
                    string sql = "insert into " + filename + " values(";
                    foreach (System.Data.DataColumn dc in dt.Columns)
                    {
                        Err += row[dc.ColumnName].ToString() + ",";
                        sql += "'" + row[dc.ColumnName].ToString().Replace("\n", " ") + "',";
                    }
                    sql = sql.Trim(',') + ")";
                    Execute(DataSource, sql, filename);
                }
                catch (Exception)
                {
                    listErr.Add(Err.Trim(','));
                }
            }
            return listErr;
        }
        public static System.Collections.ArrayList ExportDBF2(string filename, System.Data.DataTable dt)
        {
            return ExportDBF2(DataSource, filename, dt);
        }
        public static void ExportDBF(string filename, System.Data.DataTable dt)
        {
            ExportDBF(DataSource, filename, dt);
        }
        public static System.Collections.ArrayList CreateTable(string DataSource, string filename, System.Data.DataTable dt)
        {
            try
            {
                var list = new System.Collections.ArrayList();
                IO.Delete(DataSource + filename);
                string sql = "create table " + filename + " (";
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    string fieldName = dc.ColumnName;
                    string type = dc.DataType.ToString();
                    switch (type)
                    {
                        //case "System.String":
                        //    type = "varchar(255)";
                        //    break;
                        //c(100)NOCPTRANS
                        case "System.Boolean":
                            type = "varchar(50)";
                            break;
                        case "System.Int32":
                            type = "int";
                            break;
                        case "System.Double":
                            type = "f(11,2)";
                            break;
                        case "System.DateTime":
                            type = "TimeStamp";
                            break;
                        default:
                            type = "c(100)";
                            break;
                    }
                    sql += "[" + fieldName + "]" + " " + type + ",";
                    list.Add(fieldName);
                }
                sql = sql.Substring(0, sql.Length - 1) + ")";
                TM.Desktop.OleDBF.DataSource = DataSource;
                Execute(sql);
                return list;
            }
            catch (Exception) { throw; }
        }
        public static System.Collections.ArrayList CreateTable(string filename, System.Data.DataTable dt)
        {
            return CreateTable(DataSource, filename, dt);
        }
        public static void CreateTable(string DataSource, string filename, System.Collections.Generic.Dictionary<string, string> cols)
        {
            try
            {
                IO.Delete(DataSource + filename);
                string sql = "create table " + filename + " (";
                foreach (var col in cols)
                    sql += "[" + col.Key + "]" + " " + col.Value + ",";
                sql = sql.Trim(',') + ")";
                Execute(DataSource, sql, filename);
            }
            catch (Exception) { throw; }
        }
        public static void CreateTable(string filename, System.Collections.Generic.Dictionary<string, string> cols)
        {
            try
            {
                IO.Delete(DataSource + filename);
                string sql = "create table " + filename + " (";
                foreach (var col in cols)
                    sql += "[" + col.Key + "]" + " " + col.Value + ",";
                sql = sql.Substring(0, sql.Length - 1) + ")";
                Execute(DataSource, sql, filename);
            }
            catch (Exception) { throw; }
        }
        public static System.Data.DataTable GetSchemaTable(string DataSource, string version, string table)
        {
            try
            {
                System.Data.DataTable schemaTable = Connection(DataSource, version).GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, table, null });
                return schemaTable;
            }
            catch (Exception) { throw; }
        }
        public static System.Data.DataTable GetSchemaTable(string DataSource, string table)
        {
            return GetSchemaTable(DataSource, "0", table);
        }
        public static System.Data.DataTable GetSchemaTable(string table)
        {
            return GetSchemaTable(DataSource, "0", table);
        }
    //}
     */
}