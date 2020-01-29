using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {

        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError="Şifre Hatalı.";
        public static string SuccessfulLogin="Giriş Başarılı";

        public static string UserAlreadyExist = "Bu kullanıcı zaten mevcut";

        public static string UserRegistered = "Kullanıcı başarıyla kayıt oldu";

        public static string AccessTokenCreated = "Access Token başarıyla oluşturuldu";

    }
}
