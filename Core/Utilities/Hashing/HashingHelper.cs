using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Hashing
{
    public static class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using HMACSHA512 hmac = new();


            passwordHash = hmac.Key;
            passwordSalt = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt )
        {
            using HMACSHA512 hmac = new(passwordSalt); //Eğer ctor'ın içine boş bırakırsak random key oluşturur.Biz kayıt esnasında kullanıcının verdiği ve saltladığımız key'i aldık.

            byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
