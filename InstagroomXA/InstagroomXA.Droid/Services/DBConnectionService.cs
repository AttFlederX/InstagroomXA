using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
// using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using InstagroomXA.Core.Contracts;
using SQLite;

namespace InstagroomXA.Droid.Services
{
    internal class DBConnectionService : IDBConnectionService
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "InstagroomDB.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}