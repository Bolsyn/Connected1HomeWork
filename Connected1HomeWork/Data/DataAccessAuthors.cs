using Connected1HomeWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Connected1HomeWork.Data
{
    class DataAccessAuthors : DbDataAccess<Authors>
    {
        public override void Delete(Authors entity)
        {
            string deleteSqlScript = $"Delete From Authors where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(Authors entity)
        {
            string insertSqlScript = "Insert into Authors values (@FirstName, @LastName)";

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

        public override ICollection<Authors> Select()
        {
            var selectSqlScript = "select * from Authors";
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = selectSqlScript;

            var dataReader = command.ExecuteReader();

            var users = new List<Authors>();

            while (dataReader.Read()) // до тех пор пока есть, что читать - читай!
            {
                users.Add(new Authors
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString()
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }

        public override ICollection<Authors> Select(string param)
        {
            var selectSqlScript = $"select * from Account {param}";
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = selectSqlScript;

            var dataReader = command.ExecuteReader();

            var users = new List<Authors>();

            while (dataReader.Read()) // до тех пор пока есть, что читать - читай!
            {
                users.Add(new Authors
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    FirstName = dataReader["FirstName"].ToString(),
                    LastName = dataReader["LastName"].ToString()
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }

        public override void Update(Authors entity)
        {
            string updateSqlScript = $"update Author Set FirstName = {entity.FirstName}, Set LastName = {entity.LastName} where Id = {entity.Id}";

            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = updateSqlScript;
                command.ExecuteNonQuery();
            }
        }
    }
}
