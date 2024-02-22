using Microsoft.EntityFrameworkCore;
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
    public class AuthenticationService : IAuthenticationService
    {
        private readonly airline_dbContext _dbContext;
        public AuthenticationService(airline_dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDetails> VerifyUser(Login login)
        {
            var user = await (from ruser in _dbContext.RegisteredUsers
                              where ruser.UserName == login.Username                              
                              select new UserDetails
                              {
                                  FirstName = ruser.FirstName,
                                  LastName = ruser.LastName,
                                  UserName = ruser.UserName,
                                  Password = ruser.LoginPassword
                              }).SingleOrDefaultAsync();
            if (user == null)
            {
                throw new KeyNotFoundException("The username dosen't exists");
            }

            if (string.Compare(user.Password, login.Password, false) != 0)
            {
                throw new UnauthorizedAccessException("Username and password don't match");
            }

            return user;
        }
    }
}
