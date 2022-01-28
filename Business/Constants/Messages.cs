using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string FailedImageLimit => "İstenilen araç maksimum fotoğraf yükleme kapasitesine ulaştı.";

        public static string Success => "İşlem Başarılı.";

        public static string NotFoundRecord => "İlgili Kayıt Bulunamadı.";

        public static string AuthorizationDenied => "Yetkisiz işlem.";

        public static string UserRegistered = "Kullanıcı kayıt edildi.";

        public static string UserNotFound = "Bu kullanıcıya ait bir bilgi bulunamadı.";

        public static string PasswordError = "Şifre veya Email kontrol ediniz.";

        public static string SuccessfulLogin = "Giriş başarılı";

        public static string UserAlreadyExists = "Bu kullanıcı zaten kayıtlı.";

        public static string AccessTokenCreated = "Token oluşturuldu.";
    }
}
