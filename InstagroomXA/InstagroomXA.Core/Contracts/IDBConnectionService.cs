using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace InstagroomXA.Core.Contracts
{
    /// <summary>
    /// Interface for database connection classes
    /// </summary>
    public interface IDBConnectionService
    {
        SQLiteAsyncConnection GetConnection();
    }
}
