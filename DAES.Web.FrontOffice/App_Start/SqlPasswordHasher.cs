using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DAES.Web.FrontOffice
{
    public class SqlPasswordHasher : PasswordHasher
    {
        public NetFourMembershipProvider netFourMembershipProvider = new NetFourMembershipProvider();

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            string[] passwordProperties = hashedPassword.Split('|');
            if (passwordProperties.Length != 3)
            {
                return base.VerifyHashedPassword(hashedPassword, providedPassword);
            }
            else
            {
                string passwordHash = passwordProperties[0];
                int passwordformat = 2;
                string salt = passwordProperties[2];
                if (String.Equals(netFourMembershipProvider.GetEncodePassword(providedPassword, passwordformat, salt), passwordHash, StringComparison.CurrentCultureIgnoreCase))
                {
                    return PasswordVerificationResult.SuccessRehashNeeded;
                }
                else
                {
                    return PasswordVerificationResult.Failed;
                }
            }
        }
    }

    public class NetFourMembershipProvider : SqlMembershipProvider
    {
        public byte[] GetSaltedPassword(string password, string salt)
        {
            byte[] passwordbuff = Encoding.Unicode.GetBytes(password);
            byte[] saltbuff = Convert.FromBase64String(salt);
            byte[] saltedpassword = new byte[saltbuff.Length + passwordbuff.Length];
            Buffer.BlockCopy(saltbuff, 0, saltedpassword, 0, saltbuff.Length);
            Buffer.BlockCopy(passwordbuff, 0, saltedpassword, saltbuff.Length, passwordbuff.Length);

            return saltedpassword;
        }

        public string GetEncodePassword(string password, int passwordFormat, string passwordSalt)
        {
            string encodedPassword;
            byte[] buff;
            byte[] saltedPassword;

            switch (passwordFormat)
            {
                case 0:
                    encodedPassword = password;
                    break;
                case 1:
                    saltedPassword = GetSaltedPassword(password, passwordSalt); ;
                    HashAlgorithm hashAlgorithm = HashAlgorithm.Create(Membership.HashAlgorithmType);
                    buff = hashAlgorithm.ComputeHash(saltedPassword);
                    encodedPassword = Convert.ToBase64String(buff);
                    break;

                default:
                    saltedPassword = GetSaltedPassword(password, passwordSalt);
                    buff = EncryptPassword(saltedPassword);
                    encodedPassword = Convert.ToBase64String(buff);
                    break;
            }

            return encodedPassword;
        }
    }
}