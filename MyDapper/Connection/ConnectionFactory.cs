using MyDapper.Connection.ConnectionModel;
using MyDapper.Connection.MyEnum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyDapper.Connection
{
    public class ConnectionFactory : IConnectionFactory
    {
        ///<summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <returns>DatabaseType</returns>
        public DatabaseType GetDataBaseType(string databaseType)
        {
            DatabaseType returnValue = DatabaseType.MySql;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(databaseType, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }

        ///<summary>
        ///获取数据库连接
        ///</summary>
        ///<return>IDbConnection</return> 
        public IDbConnection CreateConnection()
        {
            IDbConnection connection = null;

            //获取配置进行转换
            var type = ConnectionStrings.ComponentDbType;
            var dbType = GetDataBaseType(type);

            //DefaultDatabase 根据这个配置项获取对应连接字符串
            var database = ConnectionStrings.DefaultDatabase;
            if (string.IsNullOrEmpty(database))
            {
                database = "mysql";//默认配置
            }
            string strConn = ConnectionStrings.ConnectionString;
            if (string.IsNullOrWhiteSpace(strConn))
            {
                throw new Exception("db connection string is null");
            }
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new System.Data.SqlClient.SqlConnection(strConn);
                    break;
                case DatabaseType.MySql:
                    connection = new MySql.Data.MySqlClient.MySqlConnection(strConn);
                    break;
                case DatabaseType.Oracle:
                    connection = new Oracle.ManagedDataAccess.Client.OracleConnection(strConn);
                    //  connection = new System.Data.OracleClient.OracleConnection(strConn);
                    break;
                default:
                    throw new Exception("db type is undefinded");

            }

            return connection;
        }
    }
}
