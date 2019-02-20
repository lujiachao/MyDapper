using Microsoft.Extensions.DependencyInjection;
using MyDapper.Connection;
using MyDapper.Connection.ConnectionModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDapper
{
    public class MyDapperPower
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <param name="connectionStr">数据库连接字符串</param>
        /// <param name="componentDbType"></param>
        /// <param name="defaultDatabase"></param>
        /// <returns></returns>
        public static IServiceProvider DbConnection(string connectionStr, string componentDbType, string defaultDatabase)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IConnectionFactory, ConnectionFactory>();
            MyDapperExtension.serviceProvider = serviceCollection.BuildServiceProvider();
            ConnectionStrings.ConnectionString = connectionStr;
            ConnectionStrings.ComponentDbType = componentDbType;
            ConnectionStrings.DefaultDatabase = defaultDatabase;
            return serviceCollection.BuildServiceProvider();
        }
    }
}
