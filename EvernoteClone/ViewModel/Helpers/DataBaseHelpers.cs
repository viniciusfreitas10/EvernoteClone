using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DataBaseHelpers
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        List<string> list;
        public static bool Insert<T>(T item)
        {
            bool result = false;
            using(SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rows = conn.Insert(item);
                result = (rows > 0);
            }
            return result;
        }

        public static bool Update<T>(T item)
        {
            bool result = false;
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rows = conn.Update(item);
                result = (rows > 0);
            }
            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                int rows = conn.Delete(item);
                result = (rows > 0);
            }
            return result;
        }

        public static List<T> Read<T>() where T: new()
        {
            List<T> items = new List<T>();
            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                items = conn.Table<T>().ToList<T>();
            }
            return items;
        }
    }
}
