using SearchFunctionality.BusinessLogic.AuthModels;
using SearchFunctionality.BusinessLogic.ResponseModels;
using SearchFunctionality.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFunctionality.BusinessLogic.Services
{
    public interface IAuthenticationService
    {
        Task<UserDetails> VerifyUser(Login model);
    }
}
