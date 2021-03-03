using Connected1HomeWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Connected1HomeWork.Data
{
    class DataAccessCustomers : DbDataAccess<Customers>
    {
        public override void Delete(Customers entity)
        {
            string deleteSqlScript = $"Delete From Customers where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(Customers entity)
        {
            string insertSqlScript = "Insert into Customers values (@FirstName, @LastName, @IsDebtor)";

            using (var transaction = connection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = insertSqlScript;

                try
                {
                    command.Transaction = transaction;

                    var firstNameParameter = factory.CreateParameter();
                    firstNameParameter.DbType = System.Data.DbType.String;
                    firstNameParameter.Value = entity.FirstName;
                    firstNameParameter.ParameterName = "FirstName";

                    command.Parameters.Add(firstNameParameter);

                    var lastNameParameter = factory.CreateParameter();
                    lastNameParameter.DbType = System.Data.DbType.String;
                    lastNameParameter.Value = entity.LastName;
                    lastNameParameter.ParameterName = "LastName";

                    command.Parameters.Add(lastNameParameter);


                    var isDebtorParameter = factory.CreateParameter();
                    isDebtorParameter.DbType = System.Data.DbType.Boolean;
                    isDebtorParameter.Value = entity.IsDebtor;
                    isDebtorParameter.ParameterName = "IsDebtor";

                    command.Parameters.Add(isDebtorParameter);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
                ExecuteTranaction(command);
            }
        }
        public override ICollection<Customers> Select()
        {
            var selectSqlScript = "select * from Customers";
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = selectSqlScript;

            var dataReader = command.ExecuteReader();

            var users = new List<Customers>();

            while (dataReader.Read()) // до тех пор пока есть, что читать - читай!
            {
                users.Add(new Customers
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    IsDebtor = bool.Parse(dataReader["IsDebtor"].ToString())
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }

        public override ICollection<Customers> Select(string param)
        {
            var selectSqlScript = $"select * from Account {param}";
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = selectSqlScript;
            
            var dataReader = command.ExecuteReader();
            var users = new List<Customers>();

            while (dataReader.Read()) // до тех пор пока есть, что читать - читай!
            {
                users.Add(new Customers
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    IsDebtor = bool.Parse(dataReader["IsDebtor"].ToString())
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }

        public override void Update(Customers entity)
        {
            string updateSqlScript = $"update Customers Set FirstName = {entity.FirstName}, Set LastName = {entity.LastName}, Set IsDebtor = {entity.IsDebtor} where Id = {entity.Id}";

            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = updateSqlScript;
                command.ExecuteNonQuery();
            }
        }
    }
}
