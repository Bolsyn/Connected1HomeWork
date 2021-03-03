using Connected1HomeWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Connected1HomeWork.Data
{
    class DataAccessBooksInLibrary : DbDataAccess<BooksInLibrary>
    {
        public override void Delete(BooksInLibrary entity)
        {
            string deleteSqlScript = $"Delete From BooksInLibrary where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(BooksInLibrary entity)
        {
            string insertSqlScript = "Insert into BooksInLibrary values (@Id_book, @Id_author, @Id_customers)";

            using (var transaction = connection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = insertSqlScript;

                try
                {
                    command.Transaction = transaction;

                    var bookNameParameter = factory.CreateParameter();
                    bookNameParameter.DbType = System.Data.DbType.Int32;
                    bookNameParameter.Value = entity.Id_book;
                    bookNameParameter.ParameterName = "Id_book";

                    command.Parameters.Add(bookNameParameter);

                    var authorNameParameter = factory.CreateParameter();
                    authorNameParameter.DbType = System.Data.DbType.Int32;
                    authorNameParameter.Value = entity.Id_author;
                    authorNameParameter.ParameterName = "Id_author";

                    command.Parameters.Add(authorNameParameter);


                    var customerParameter = factory.CreateParameter();
                    customerParameter.DbType = System.Data.DbType.Int32;
                    customerParameter.Value = entity.Id_customer;
                    customerParameter.ParameterName = "Id_customer";

                    command.Parameters.Add(customerParameter);

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

        public override ICollection<BooksInLibrary> Select()
        {
            var selectSqlScript = "select * from BooksInLibrary";
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = selectSqlScript;

            var dataReader = command.ExecuteReader();

            var users = new List<BooksInLibrary>();

            while (dataReader.Read()) // до тех пор пока есть, что читать - читай!
            {
                users.Add(new BooksInLibrary
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    Id_book = int.Parse(dataReader["Id"].ToString()),
                    Id_author = int.Parse(dataReader["Id"].ToString()),
                    Id_customer = int.Parse(dataReader["Id"].ToString())
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }

        public override ICollection<BooksInLibrary> Select(string param)
        {
            var selectSqlScript = $"select * from BooksInLibrary {param}";
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = selectSqlScript;

            var dataReader = command.ExecuteReader();
            var users = new List<BooksInLibrary>();

            while (dataReader.Read()) // до тех пор пока есть, что читать - читай!
            {
                users.Add(new BooksInLibrary
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    Id_book = int.Parse(dataReader["Id"].ToString()),
                    Id_author = int.Parse(dataReader["Id"].ToString()),
                    Id_customer = int.Parse(dataReader["Id"].ToString())
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }

        public override void Update(BooksInLibrary entity)
        {
            string updateSqlScript = $"update Customers Set FirstName = {entity.Id_book}, Set LastName = {entity.Id_author}, Set IsDebtor = {entity.Id_customer} where Id = {entity.Id}";

            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = updateSqlScript;
                command.ExecuteNonQuery();
            }
        }
    }
}
