using Connected1HomeWork.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connected1HomeWork.Data
{
    class DataAccessBooks : DbDataAccess<Books>
    {
        public override void Delete(Books entity)
        {
            string deleteSqlScript = $"Delete From Books where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(Books entity)
        {
            string insertSqlScript = "Insert into Books values (@Title)";


            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = insertSqlScript;

                command.ExecuteNonQuery();
            }
        }

        public override ICollection<Books> Select()
        {
            var selectSqlScript = "select * from Books";
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = selectSqlScript;

            var dataReader = command.ExecuteReader();

            var users = new List<Books>();

            while (dataReader.Read()) // до тех пор пока есть, что читать - читай!
            {
                users.Add(new Books
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    Title = dataReader["Title"].ToString()                    
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }

        public override ICollection<Books> Select(string param)
        {
            var selectSqlScript = $"select * from Account where TelephoneNumber = {entity}";
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = selectSqlScript;

            var dataReader = command.ExecuteReader();

            var users = new List<Books>();

            while (dataReader.Read()) // до тех пор пока есть, что читать - читай!
            {
                users.Add(new Books
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    Title = dataReader["Title"].ToString()
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }

        public override void Update(Books entity)
        {
            string updateSqlScript = $"update Books Set Title = {entity.Title} where Id = {entity.Id}";

            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = updateSqlScript;
                command.ExecuteNonQuery();
            }
        }
    }
}
