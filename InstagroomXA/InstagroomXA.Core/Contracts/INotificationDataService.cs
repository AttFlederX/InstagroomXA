using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Model;

namespace InstagroomXA.Core.Contracts
{
    /// <summary>
    /// Interface for user notification data services' classes
    /// </summary>
    public interface INotificationDataService
    {
        Task<Notification> GetNotificationByIDAsync(int notificationId);
        Task<IEnumerable<Notification>> GetUserNotifications(int userId, int n = 0);

        Task<bool> AddNotificationAsync(Notification newNotification);
    }
}
