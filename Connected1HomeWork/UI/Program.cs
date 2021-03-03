using Connected1HomeWork.Data;
using Connected1HomeWork.Service;
using System;

namespace Connected1HomeWork.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfiguration();

            var customerDataAccess = new DataAccessCustomers();
            string needToFindCustomers = "Where IsDebtor = 1";
            var customersWithDebt = customerDataAccess.Select(needToFindCustomers);

            var authorDataAccess = new DataAccessBooksInLibrary();
            string needToFindBookAuthor = "Where Id_book = 3";
            var authorsOfBook3 = customerDataAccess.Select(needToFindBookAuthor);

            var bookDataAccess = new DataAccessBooks();
            var booksInLibrary = customerDataAccess.Select();

            string needToFindBookCustomer = "Where Id_Customer = 2";
            var booksOfCustomer = customerDataAccess.Select(needToFindBookCustomer);

            foreach(var custom in customersWithDebt)
            {
                custom.IsDebtor = false;
                customerDataAccess.Update(custom);
            }
           
        }

        private static void InitConfiguration()
        {
            ConfigService.Init();
        }
    }
}
