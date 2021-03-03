using Connected1HomeWork.Service;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Connected1HomeWork.Data
{
    public abstract class DbDataAccess<T> : IDisposable
    {
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;


        public DbDataAccess()
        {
            factory = DbProviderFactories.GetFactory("ConnectedLessonProvider");

            connection = factory.CreateConnection();

            connection.ConnectionString = ConfigService.Configuration["DataAccessConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public abstract void Insert(T entity);

        public abstract void Update(T entity);

        public abstract void Delete(T entity);

        public abstract ICollection<T> Select();

        public abstract ICollection<T> Select(string param);

        public void ExecuteTranaction(params DbCommand[] commands)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var command in commands)
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
