using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stack.DAL;
using Stack.Entities.Models;
using Stack.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stack.Core.Managers
{
    public class EmployeeRepositry : UserManager<Employee>
    {
        public EmployeeRepositry(IUserStore<Employee> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Employee> passwordHasher, IEnumerable<IUserValidator<Employee>> userValidators, IEnumerable<IPasswordValidator<Employee>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Employee>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,logger)
        {



        }



    }
}
