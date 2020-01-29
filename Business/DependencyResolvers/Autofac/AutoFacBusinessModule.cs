using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutoFacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<TestManager>().As<ITestService>();
            builder.RegisterType<CevapManager>().As<ICevapService>();
            builder.RegisterType<YorumManager>().As<IYorumService>();
            builder.RegisterType<OgretmenManager>().As<IOgretmenService>();
            builder.RegisterType<SoruManager>().As<ISoruService>();

            builder.RegisterType<EfOgrenciOgretmeniDal>().As<IOgrenciOgretmeniDal>();
            builder.RegisterType<EfKonuDal>().As<IKonuDal>();
            builder.RegisterType<EfDersDal>().As<IDersDal>();
            builder.RegisterType<EfRoleDal>().As<IRoleDal>();
            builder.RegisterType<EfOgretmenDal>().As<IOgretmenDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<EfOkulDal>().As<IOkulDal>();
            builder.RegisterType<EfYorumDal>().As<IYorumDal>();
            builder.RegisterType<EfIlceDal>().As<IIlceDal>();
            builder.RegisterType<EfCevapDal>().As<ICevapDal>();
            builder.RegisterType<EfIlDal>().As<IIlDal>();
            builder.RegisterType<EfGenelIstatistikDal>().As<IGenelIstatistikDal>();
            builder.RegisterType<EfCevapDal>().As<ICevapDal>();
            builder.RegisterType<EfOgrenciDal>().As<IOgrenciDal>();
            builder.RegisterType<EfSoruDal>().As<ISoruDal>();
            builder.RegisterType<EfTestSonucDal>().As<ITestSonucDal>();
            builder.RegisterType<EfAltBasliklarDal>().As<IAltBasliklarDal>();
            builder.RegisterType<EfSoruAltBaslikDal>().As<ISoruAltBaslikDal>();

            builder.RegisterType<UserManager>().As<IUserService>();

        }
    }
}
