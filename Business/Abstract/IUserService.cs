using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        int AddAndGetId(User user);
        IDataResult<IQueryable<User>> GetByEmailQueryable(string email);
        IDataResult<User> GetByEmail(string email);        
    }
}