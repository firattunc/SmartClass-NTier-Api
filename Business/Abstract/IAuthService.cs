using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<List<Il>> GetIller();
        IDataResult<List<Ilce>> GetIlcelerByIl(int id);
        IDataResult<List<Okul>> GetOkullarByIlceId(int id);
        IDataResult<List<OkulDto>> GetOkullar();
        IResult AddKullanici(UserForRegisterDto userForRegisterDto);
        IDataResult<List<Role>> GetRoller();
    }
}