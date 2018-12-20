using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JassPro.DAL
{
    /// <summary>
    /// 原生数据库操作
    /// </summary>
    public class DbHelper
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Jewelry_Sql"].ConnectionString;
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static IList<T> RunProcedure<T>(string storedProcName, IDataParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    IList<T> entityList = new List<T>();
                    while (dataReader.Read())
                    {
                        entityList.Add(FillRecord<T>(dataReader));
                    }
                    connection.Close();
                    return entityList;
                }
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static MySqlCommand BuildQueryCommand(MySqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (MySqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        /// 用DataReader填充实体模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static T FillRecord<T>(IDataReader reader)
        {
            T entity = Activator.CreateInstance<T>();
            Type type = entity.GetType();
            int fieldCount = reader.FieldCount;
            PropertyInfo[] infos = type.GetProperties();
            foreach (PropertyInfo info in infos)
            {
                try
                {
                    object[] cAttributes = info.GetCustomAttributes(typeof(ColumnAttribute), true);
                    string field = "";
                    if (cAttributes.Length > 0)
                    {
                        field = ((ColumnAttribute)cAttributes[0]).Name;
                    }
                    else
                    {
                        field = info.Name;
                    }
                    object o = reader.GetValue(reader.GetOrdinal(field));
                    if (o != DBNull.Value)
                    {
                        if (info.PropertyType == typeof(System.Int32))
                        {
                            int v = 0;
                            int.TryParse(o.ToString(), out v);
                            info.SetValue(entity, v, null);
                        }
                        else if (info.PropertyType == typeof(System.Int64))
                        {
                            long v = 0;
                            long.TryParse(o.ToString(), out v);
                            info.SetValue(entity, v, null);
                        }
                        else
                            info.SetValue(entity, o, null);
                    }
                }
                catch (Exception ex)
                {
                    string errMessage =  ex.Message;
                }
            }
            /*for (int i = 0; i < fieldCount; i++)
            {
                string filedName = reader.GetName(i);
                PropertyInfo info = type.GetProperty(filedName);
                if (info != null)
                {
                    try
                    {
                        object o = reader.GetValue(i);
                        if (o != DBNull.Value)
                        {
                            if (info.PropertyType == typeof(System.Int32))
                            {
                                int v = 0;
                                int.TryParse(o.ToString(), out v);
                                info.SetValue(entity, v, null);
                            }
                            else if (info.PropertyType == typeof(System.Int64))
                            {
                                long v = 0;
                                long.TryParse(o.ToString(), out v);
                                info.SetValue(entity, v, null);
                            }
                            else
                                info.SetValue(entity, o, null);
                        }
                    }
                    catch { }
                }
            }*/
            return entity;
        }

        public static DataTable JKAdapter(string cmdText, MySqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmdText, connectionString))
            {
                if (paras != null)
                {
                    sda.SelectCommand.Parameters.AddRange(paras);
                }
                sda.Fill(dt);
            }
            return dt;
        } 

    }
}
