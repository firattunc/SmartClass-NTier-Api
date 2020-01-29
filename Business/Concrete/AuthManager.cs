using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {

        private IUserService _userService;
        private IUserDal _userDal;
        private ITokenHelper _tokenHelper;
        private IIlDal _ılDal;
        private IIlceDal _ılceDal;
        private IOkulDal _okulDal;
        private IGenelIstatistikDal _genelIstatistikDal;
        private IOgrenciDal _ogrenciDal;
        private IAltBasliklarDal _altBasliklarDal;
        private IRoleDal _roleDal;


        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IIlDal ılDal, IIlceDal ılceDal,IOkulDal okulDal, IGenelIstatistikDal genelIstatistikDal,
            IOgrenciDal ogrenciDal,IAltBasliklarDal altBasliklarDal,IUserDal userDal,IRoleDal roleDal)
        {
            _roleDal = roleDal;
            _userDal = userDal;
            _userService = userService;
            _tokenHelper = tokenHelper;
            _ılDal = ılDal;
            _ılceDal = ılceDal;
            _okulDal = okulDal;
            _genelIstatistikDal = genelIstatistikDal;
            _ogrenciDal = ogrenciDal;
            _altBasliklarDal = altBasliklarDal;
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken=_tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var checkUser = _userService.GetByEmail(userForLoginDto.kullaniciAdi);
            if (checkUser.Success!=true)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);    
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.sifre,checkUser.Data.PasswordHash,checkUser.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(checkUser.Data, Messages.SuccessfulLogin);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var checkUser = UserExists(userForRegisterDto.kullaniciAdi);
            if (!checkUser.Success)
            {
                return new ErrorDataResult<User>(checkUser.Message);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.sifre, out passwordHash, out passwordSalt);
            User user = new User
            {
                kullaniciAdi=userForRegisterDto.kullaniciAdi,
                ad= userForRegisterDto.ad,
                soyad= userForRegisterDto.soyad,
                Status=true,
                PasswordHash=passwordHash,
                PasswordSalt=passwordSalt,
                CurrentRoleId=1,  
                Tel="05322222222"
            };
            try
            {
                var kullaniciId=_userService.AddAndGetId(user);
                var ogrenciId = _ogrenciDal.AddAndGetId(new Ogrenci { 
                    OkulId=userForRegisterDto.selectedOkulId,
                    UserId=kullaniciId,
                    No=userForRegisterDto.no
                });                
                var altbasliklar = _altBasliklarDal.GetList().ToList();
                for (int i = 0; i < altbasliklar.Count-1; i++)
                {
                    _genelIstatistikDal.Add(new GenelIstatistik {
                        OgrenciId=ogrenciId,
                        AltBaslikId=altbasliklar[i].Id,
                        DogruSayisi=0                                                
                    });
                }
                
                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }
            catch (Exception)
            {

                return new ErrorDataResult<User>("Kullanıcı eklenemedi.");
            }                      
        }

        public IResult UserExists(string kullaniciAdi)
        {
            if (_userService.GetByEmail(kullaniciAdi).Success!=true)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.UserAlreadyExist);
           
        }
        public IDataResult<List<Il>> GetIller()
        {
            var result = _ılDal.GetList().ToList();
            if (result!=null)
            {
                return new SuccessDataResult<List<Il>>(result);
            }
            return new ErrorDataResult<List<Il>>("Iller bulunamadı.");
        }

        public IDataResult<List<Ilce>> GetIlcelerByIl(int id)
        {
            var result = _ılceDal.GetList(x=>x.IlId==id).ToList();
            if (result != null)
            {
                return new SuccessDataResult<List<Ilce>>(result);
            }
            return new ErrorDataResult<List<Ilce>>("Ilceler bulunamadı.");
        }

        public IDataResult<List<Okul>> GetOkullarByIlceId(int id)
        {
            var result = _okulDal.GetList(x=>x.IlceId==id).ToList();
            if (result != null)
            {
                return new SuccessDataResult<List<Okul>>(result);
            }
            return new ErrorDataResult<List<Okul>>("Okullar bulunamadı.");
        }

        public IDataResult<List<OkulDto>> GetOkullar()
        {
            List<OkulDto> okulDtos = new List<OkulDto>();
            var result = _okulDal.GetQueryable().Include(x=>x.Ilce.Il).Take(10).ToList();
            foreach (var okul in result)
            {
                okulDtos.Add(new OkulDto { ImgUrl = okul.ImgUrl, OkulAdi = okul.OkulAdi, IlAdi = okul.Ilce.Il.IlAd });
            }
            return new SuccessDataResult<List<OkulDto>>(okulDtos);

        }

        public IResult AddKullanici(UserForRegisterDto userForRegisterDto)
        {
            var checkUser = UserExists(userForRegisterDto.kullaniciAdi);
            if (!checkUser.Success)
            {
                return new ErrorDataResult<User>(checkUser.Message);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.sifre, out passwordHash, out passwordSalt);
            User user = new User
            {
                kullaniciAdi = userForRegisterDto.kullaniciAdi,
                ad = userForRegisterDto.ad,
                soyad = userForRegisterDto.soyad,
                Status = true,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CurrentRoleId = userForRegisterDto.CurrentRoleId,
                Tel = "05322222222"
            };
            try
            {
                _userDal.Add(user);
                return new SuccessResult(Messages.UserRegistered);
            }
            catch (Exception)
            {
                return new ErrorResult("Kullanıcı eklenemedi.");
            }
        }

        public IDataResult<List<Role>> GetRoller()
        {
            var result = _roleDal.GetList().Skip(2).ToList();
            return new SuccessDataResult<List<Role>>(result);
        }
    }
}
