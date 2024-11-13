using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //CreatepaswordHash ona verdiğimiz bir passwordu hashini oluşturacak ve salt'ını da oluşturacak.
        //out keywordu ile oraya gönderilen iki tane değeri boş bile olsa doldurup geri döndürmeye yarar.
        //burdaki out'u dışarıya verilecek değer gibi düşünebiliriz.
        //Yani burda biz bir tane string  tipinde bir password vericez
        //ve dışarıya diğer 2 değeri birden çıkaracak bir yapı tasarladık.
        public static void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt) 
        {
            //hmac bizim kriptokrafi sınıfında kullandığımz classa karşılık gelicek
            //Bu(HMACSHA512) algoirtmayı kullanarak password, hash ve salt oluşturucaz.
            using (var hmac=new System.Security.Cryptography.HMACSHA512()) 
            {
                //Kısacası bu yazdığığımız kodlar verdiğimiz bir password değerinin salt ve hash değerini oluşturmaya yarıyor.
                passwordSalt = hmac.Key; //Her kullanıcı için bir key değeri oluşturur. Biz oyüzden veri tabanında salt değerini de tutacağımızı söyledik.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //encoding .... ile bana paswordun byte karşılığını ver diyoruz.

                //bizim göndereceğimiz değerlere hmac içindeki karşılıklarını tanımlıyoruz.
            }
        }
        
        //Password hashini doğrula. Sonradan girilen passwordun yukarıda oluşturulan passwordun hashiyle eşleşip eşleşmediğini kontrol ettiğimiz yer
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) //Hesaplanan hash'in bütün değerlerini tek tek gez 
                {
                    //eğer benim computedHash'in i. değeri veritabanından gönderilen passwordhashin i. değerine eşit değilse false dön kısacası karşılaştırıyoz.
                    if (computedHash[i] != passwordHash[i])  
                        return false;
                }
                return true;
            }   
        }
    }
}
