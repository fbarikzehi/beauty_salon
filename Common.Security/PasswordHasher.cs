namespace Common.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public virtual string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public virtual PasswordVerificationResultEnum VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (Crypto.VerifyHashedPassword(hashedPassword, providedPassword))
            {
                return PasswordVerificationResultEnum.Success;
            }
            return PasswordVerificationResultEnum.Failed;
        }
    }
}
