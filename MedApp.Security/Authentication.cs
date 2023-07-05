namespace MedApp.Security;

public static class Authentication
{
    public static (string hashedPassword, string salt) GenerateHashAndSalt(string password)
    {
        var salt = GenerateSalt();
        var hashedPassword = GenerateHashedPassword(password, salt);

        return (Convert.ToBase64String(hashedPassword), Convert.ToBase64String(salt));
    }
    
    public static bool IsPasswordValid(string password, string passwordHash, string passwordSalt)
    {
        var salt = Convert.FromBase64String(passwordSalt);
        var hash = GenerateHash(password, salt);

        return passwordHash == Convert.ToBase64String(hash);
    }
    
    private static byte[] GenerateSalt()
    {
        var saltBytes = new byte[32];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }

        return saltBytes;
    }

    private static byte[] GenerateHash(string password, byte[] salt)
    {
        using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA512);
        var hashBytes = rfc2898DeriveBytes.GetBytes(64);

        return hashBytes;
    }
}
