using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Model;

using SQLite;

namespace InstagroomXA.Core.Services
{
    /// <summary>
    /// Notification data service implementation
    /// </summary>
    public class NotificationDataService : INotificationDataService
    {
        private SQLiteAsyncConnection _masterConnection;


        public NotificationDataService(IDBConnectionService connectionService)
        {
            _masterConnection = connectionService.GetConnection();
            _masterConnection.CreateTableAsync<Notification>();
        }

        /// <summary>
        /// Returns the notification with the specified ID (ID property is a primary key)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Notification> GetNotificationByIDAsync(int notificationId)
        {
            // return await _masterConnection.FindAsync<Post>(postId);
            return await _masterConnection.FindAsync<Notification>(notificationId);
        }

        /// <summary>
        /// Return all (or first n) notifications for the specified user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Notification>> GetUserNotifications(int userId, int n = 0)
        {
            var query = _masterConnection.Table<Notification>().Where(nt => (nt.TargetUserID == userId));
            if (n != 0) { query = query.Take(n); }
            return await query.ToListAsync();

            // finds all the user posts, n is irrelevant
            //var query = await _masterConnection.GetAllWithChildrenAsync<Post>(p => (p.UserID == userId));

            //return query.ToList();
        }

        /// <summary>
        /// Adds a new notification profile to the SQLite database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<bool> AddNotificationAsync(Notification newNotification)
        {
            try
            {
                // await _masterConnection.InsertAsync(newPost);
                await _masterConnection.InsertAsync(newNotification);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
