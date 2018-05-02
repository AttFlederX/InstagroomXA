using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;

namespace InstagroomXA.Core.Services
{
    /// <summary>
    /// Syntax validation service implementation
    /// </summary>
    public class ValidationService : IValidationService
    {
        private Regex emailValidator = new Regex(@"^[0-9a-z\.]+@\w+.\w+$");

        public string PasswordCriteria => throw new NotImplementedException();


        public bool IsEmailValid(string email)
        {
            return (!string.IsNullOrWhiteSpace(email) && emailValidator.IsMatch(email));
        }

        public bool IsPasswordValid(string password)
        {
            return (!string.IsNullOrWhiteSpace(password) && password.Length > 7);
        }
    }
}
