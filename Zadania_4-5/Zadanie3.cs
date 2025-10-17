using System;

namespace Zadania_4_5
{
    using System;
    using System.Collections.Generic;

    public sealed class DatabaseConnectionManager
    {
        private static Dictionary<string, DatabaseConnectionManager> instances = new();
        private static readonly object lockObj = new object();
        private List<string> _data = new();
        public bool IsConnected { get; private set; }
        public string Database { get; private set; }
        

        private DatabaseConnectionManager(string dbName)
        {
            Database = dbName;
        }

        public static DatabaseConnectionManager GetConnection(string database)
        {
            if (!instances.ContainsKey(database))
            {
                lock (lockObj)
                {
                    if (!instances.ContainsKey(database))
                    {
                        instances[database] = new DatabaseConnectionManager(database);
                    }
                }
            }

            Console.WriteLine($"Getting connection with database {database}...\n");
            return instances[database];
        }

        public void OpenConnection()
        {
            Console.WriteLine($"Connecting to database {Database}...\n");
            instances[Database].IsConnected = true;
        }

        public void CloseConnection()
        {
            if (instances[Database].IsConnected)
            {
                Console.WriteLine($"Disconnecting from database {Database}...\n");
                instances[Database].IsConnected = false;
            }
        }

        private bool CheckConnection()
        {
            if (!instances[Database].IsConnected)
            {
                Console.WriteLine("Your connection with database is not opened!\nDo you wish to open it now? (Y/n)");
                string ans = Console.ReadLine();
                if (ans != null && ans.Trim() != "" && Char.ToLower(ans[0]) == 'n')
                {
                    return false;
                }
                else
                {
                    instances[Database].OpenConnection();
                }
            }

            return true;
        }

        public void AddDataToDb(string data)
        {
            if (!CheckConnection())
                return;

            if(data.Trim() == "")
            {
                Console.WriteLine("Data is empty!");
                return;
            }

            _data.Add(data);
            Console.WriteLine($"Successfully added {data} to database!");
        }
        public void AddDataToDb()
        {
            if (!CheckConnection())
                return;

            Console.Write("Enter your data (to end press enter):\n > ");
            string dataToEnter = Console.ReadLine();
            while (dataToEnter.Trim() != "")
            {
                _data.Add(dataToEnter);
                Console.Write(" > ");
                dataToEnter = Console.ReadLine();
            }
        }

        public void ShowDbData()
        {
            if (!CheckConnection())
                return;

            Console.WriteLine($"Database {Database} data:");
            int i = 1;
            foreach(string data in _data)
            {
                Console.WriteLine($"{i}. {data}");
                i += 1;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            DatabaseConnectionManager db1 = DatabaseConnectionManager.GetConnection("library");
            DatabaseConnectionManager db2 = DatabaseConnectionManager.GetConnection("school");
            DatabaseConnectionManager db3 = DatabaseConnectionManager.GetConnection("music");
            DatabaseConnectionManager db4 = DatabaseConnectionManager.GetConnection("library");
            DatabaseConnectionManager db5 = DatabaseConnectionManager.GetConnection("school");


            db1.AddDataToDb("Book 1");
            db1.AddDataToDb("Book 2");
            db1.AddDataToDb();
            db2.AddDataToDb("Teacher 1");
            db2.AddDataToDb("Teacher 2");
            db3.AddDataToDb("Music 1");
            db3.AddDataToDb("Music 2");
            db4.AddDataToDb("Book 3");
            db4.AddDataToDb("Book 4");
            db5.AddDataToDb();
           

            db1.ShowDbData();
            db2.ShowDbData();
            db3.ShowDbData();
            db4.ShowDbData();
            db5.ShowDbData();

            db1.CloseConnection();
            db2.CloseConnection();
            db3.CloseConnection();
            db4.CloseConnection();
            db5.CloseConnection();

            Thread t1 = new Thread(() =>
            {
                DatabaseConnectionManager db = DatabaseConnectionManager.GetConnection("testowa");
                db.OpenConnection();
                db.AddDataToDb("testowa data z db6 nr 1");
                db.AddDataToDb("testowa data z db6 nr 2");
                db.AddDataToDb("testowa data z db6 nr 3");
                db.CloseConnection();
            });

            Thread t2 = new Thread(() =>
            {
                DatabaseConnectionManager db = DatabaseConnectionManager.GetConnection("testowa");
                db.OpenConnection();
                db.AddDataToDb("testowa data z db7 nr 1");
                db.AddDataToDb("testowa data z db7 nr 2");
                db.AddDataToDb("testowa data z db7 nr 3");
                db.CloseConnection();
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            DatabaseConnectionManager db6 = DatabaseConnectionManager.GetConnection("testowa");
            db6.OpenConnection();
            db6.ShowDbData();
            db6.CloseConnection();
        }
    }
}
