using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Hashing
{
    public class HashingHelper
    {
        /*password sifreleme algoritmamiz
         * gonderecegimiz sifreyi PasswordSalt ile birlestirip bir sifre olusturucaz. sonra login isleminde ise sifremizi tekrardan hashleme ile passwordSalt ile tekrardan bir kriptolu sifre haline getiricez. bundan sonra ise harf harf kontrol ettiricez islem dogru mu diye.
         */
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //bu noktada artik gonderdigimiz sifreyi tekrardan kriptoladik elimizde de kendi sifremiz vardi. for dongusu ile beraber bu iki sifreyi kontrol etmemiz lazim.
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
