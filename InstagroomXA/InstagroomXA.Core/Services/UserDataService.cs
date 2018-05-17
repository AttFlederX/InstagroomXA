using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Model;

using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace InstagroomXA.Core.Services
{
    /// <summary>
    /// User data service implementation
    /// </summary>
    public class UserDataService : IUserDataService
    {
        private SQLiteAsyncConnection _masterConnection;

        public User CurrentUser { get; set; }


        public UserDataService(IDBConnectionService connectionService)
        {
            _masterConnection = connectionService.GetConnection();
            _masterConnection.CreateTableAsync<User>();
        }

        /// <summary>
        /// Adds a new user profile to the SQLite database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<bool> AddUserAsync(User newUser)
        {
            try
            {
                await _masterConnection.InsertWithChildrenAsync(newUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the user profile with the specified ID (ID property is a primary key)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIDAsync(int userId)
        {
            return await _masterConnection.FindWithChildrenAsync<User>(userId);
        }

        /// <summary>
        /// Returns the user profile with the specified username (username should be unique)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var query = await _masterConnection.GetAllWithChildrenAsync<User>(up => (up.Username == username));
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Returns the list of users whose name matches the query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUserByQueryAsync(string queryString)
        {
            var query = await _masterConnection.GetAllWithChildrenAsync<User>(u => (u.Username.Contains(queryString) || 
                                                                        u.FirstName.Contains(queryString) ||
                                                                        u.LastName.Contains(queryString)));

            return query.ToList();
        }

        /// <summary>
        /// Updates the user profile in the database
        /// </summary>
        /// <param name="updUser"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserAsync(User updUser)
        {
            try
            {
                await _masterConnection.UpdateWithChildrenAsync(updUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
