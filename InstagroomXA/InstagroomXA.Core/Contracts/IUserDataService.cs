using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Model;

namespace InstagroomXA.Core.Contracts
{
    /// <summary>
    /// Interface for user data services' classes
    /// </summary>
    public interface IUserDataService
    {
        User CurrentUser { get; set; }

        Task<User> GetUserByIDAsync(int userId);
        Task<User> GetUserByUsernameAsync(string username);

        Task<bool> AddUserAsync(User newUser);
        Task<bool> UpdateUserAsync(User updUser);
    }
}
