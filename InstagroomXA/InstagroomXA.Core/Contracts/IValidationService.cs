using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagroomXA.Core.Contracts
{
    /// <summary>
    /// Interface for syntax validation services' classes
    /// </summary>
    public interface IValidationService
    {
        string PasswordCriteria { get; }

        bool IsEmailValid(string email);
        bool IsPasswordValid(string password);
    }
}
