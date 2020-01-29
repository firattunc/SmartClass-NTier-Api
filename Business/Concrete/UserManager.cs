using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public int AddAndGetId(User user)
        {
            return _userDal.AddAndGetId(user);
        }

        public IDataResult<User> GetByEmail(string email)
        {                
            var user = _userDal.Get(x => x.kullaniciAdi == email);
            if (user==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(user);
        }

        public IDataResult<IQueryable<User>> GetByEmailQueryable(string email)
        {
            var user = _userDal.GetQueryable(x => x.kullaniciAdi == email)
                .Include(x => x.Role).AsQueryable();
            return new SuccessDataResult<IQueryable<User>>(user);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }


    }
}
