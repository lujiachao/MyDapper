using MyDapper;
using MyDapper.Connection;
using MyDapper.SqlPower;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using Xunit;

namespace MyDapperFacts
{
    public class MyDapperFacts
    {
        public class Test
        {

        }

        [Fact]
        public void DbConnectionMysqlFact()
        {
            //mysql 配置测试
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
        }

        [Fact]
        public void DbConnectionOracleFact()
        {
            //oracle 配置测试
            MyDapperPower.DbConnection("Data Source=192.168.1.75/pdb2;User ID=itps_test;PassWord=mysql", "oracle", "oracel");
        }

        [Fact]
        public void CreateConnectionMysqlFact()
        {
            //mysql 创建连接
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            ConnectionFactory connectionFactory = new ConnectionFactory();
            IDbConnection dbConnection = connectionFactory.CreateConnection();
            dbConnection.Open();
        }

        [Fact]
        public void CreateConnectionOracleFact()
        {
            //oracle 创建连接
            MyDapperPower.DbConnection("Data Source=192.168.1.75/pdb2;User ID=itps_test;PassWord=mysql", "oracle", "oracel");
            ConnectionFactory connectionFactory = new ConnectionFactory();
            IDbConnection dbConnection = connectionFactory.CreateConnection();
            dbConnection.Open();
        }

        [Fact]
        public void MysqlGetAll()
        {
            //mysql getall
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var result = userLocalDAL.GetAll();
        }

        [Fact]
        public void MysqlGetAllTra()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = userLocalDAL.Connection)
                {
                    var result = userLocalDAL.GetAll(connection);
                }
            }
        }

        [Fact]
        public void MysqlFindByID()
        {
            //mysql FindByID
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var result = userLocalDAL.FindByID(2);
        }

        [Fact]
        public void MysqlFindByIDTra()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = userLocalDAL.Connection)
                {
                    var result = userLocalDAL.FindByID(connection, 2);
                }
            }
        }

        [Fact]
        public void MysqlInsert()
        {
            //mysql Insert
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var result = userLocalDAL.Insert(new UserLocal { Password = "1", UserName = "1", PickName = "1", MobilePhone = "1", Status = 1 });
        }

        [Fact]
        public void MysqlInsertTra()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = userLocalDAL.Connection)
                {
                    var result = userLocalDAL.Insert(connection, new UserLocal { Password = "1", UserName = "1", PickName = "1", MobilePhone = "1", Status = 1 });
                }
            }
        }

        [Fact]
        public void MysqlInsertList()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            IList<UserLocal> userLocalList = new List<UserLocal>();
            for (int i = 0; i < 5; i++)
            {
                userLocalList.Add(new UserLocal { Password = "2", UserName = "2", PickName = "2", MobilePhone = "2", Status = 2 });
            }
            var result = userLocalDAL.Insert(userLocalList);
        }

        [Fact]
        public void MysqlInsertListTra()
        {
            // 事务不能insert数据，需要修改bug
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            IList<UserLocal> userLocalList = new List<UserLocal>();
            for (int i = 0; i < 5; i++)
            {
                userLocalList.Add(new UserLocal { Password = "2", UserName = "2", PickName = "2", MobilePhone = "2", Status = 2 });
            }
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = userLocalDAL.Connection)
                {
                    var result = userLocalDAL.Insert(connection,userLocalList);
                }
            }
        }

        [Fact]
        public void MysqlUpdate()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            //ID id必须填写，否则修改失败
            var result = userLocalDAL.Update(new UserLocal { ID = 8, Password = "2", UserName = "2", PickName = "2", MobilePhone = "2", Status = 2 });
        }

        [Fact]
        public void MysqlUpdateTra()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = userLocalDAL.Connection)
                {
                    var result = userLocalDAL.Update(connection,new UserLocal { ID = 26, Password = "2", UserName = "2", PickName = "2", MobilePhone = "2", Status = 2 });
                }
            }
        }

        [Fact]
        public void MysqlUpdateList()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            IList<UserLocal> userLocalList = new List<UserLocal>();
            for (int i = 8; i < 14; i++)
            {
                userLocalList.Add(new UserLocal { ID = i,Password = "3", UserName = "3", PickName = "3", MobilePhone = "3", Status = 3 });
            }
            var result = userLocalDAL.Update(userLocalList);
        }

        [Fact]
        public void MysqlDelete()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var result = userLocalDAL.Delete(new UserLocal { ID = 13});
        }

        [Fact]
        public void MysqlDeleteList()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            IList<UserLocal> userLocalList = new List<UserLocal>();
            for (int i = 8; i < 20; i++)
            {
                userLocalList.Add(new UserLocal { ID = i});
            }
            var result = userLocalDAL.Delete(userLocalList);
        }

        [Fact]
        public void MysqlDeleteById()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var result = userLocalDAL.Delete(20);
        }

        [Fact]
        public void MysqlDeleteAll()
        {

        }

        [Fact]
        public void MysqlExecuteScalar()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var commandText = $"select * from {EntityHelper.CallName<UserLocal>()} where id = @id";
            int id = 21;
            var result = userLocalDAL.ExecuteScalar<int>(commandText, new { id });
            Assert.Equal(id,result);
        }

        [Fact]
        public void MysqlExecuteScalarTra()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var commandText = $"select status from {EntityHelper.CallName<UserLocal>()} where id = @id";
            var commandText2 = $"update {EntityHelper.CallName<UserLocal>()} set status = 5 where id = @id";
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = userLocalDAL.Connection)
                {
                    int id = 22;
                    var result1 = userLocalDAL.ExecuteScalar<int>(connection, commandText2, new { id });
                    var result2 = userLocalDAL.ExecuteScalar<int>(connection,commandText, new { id });
                    Assert.Equal(5, result2);
                }
            }
        }

        [Fact]
        public void MysqlExecute()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var commandText = $"update {EntityHelper.CallName<UserLocal>()} set status = 7 where id > 30";
            var result = userLocalDAL.Execute(commandText,null);
        }

        [Fact]
        public void MysqlExecuteTra()
        {
            MyDapperPower.DbConnection("server=localhost;port=3306;user id=root;password=Aa82078542;database=testmysql;SslMode=none", "mysql", "mysql");
            UserLocalDAL userLocalDAL = new UserLocalDAL();
            var commandText = $"update {EntityHelper.CallName<UserLocal>()} set status = 7 where id > 30";
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var connection = userLocalDAL.Connection)
                {
                    var result = userLocalDAL.Execute(connection,commandText, null);
                }
            }
        }

    }
}