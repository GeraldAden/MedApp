namespace MedApp.Infrastructure.Security;

using System.Security.Cryptography;

public interface IAuthenticationService
{
    (string passwordHash, string passwordSalt) GenerateHashAndSalt(string password);
    bool IsPasswordValid(string password, string passwordHash, string passwordSalt);
}

public class AuthenticationService : IAuthenticationService
{
    public (string passwordHash, string passwordSalt) GenerateHashAndSalt(string password)
    {
        var salt = GenerateSalt();
        var hashedPassword = GenerateHash(password, salt);

        return (Convert.ToBase64String(hashedPassword), Convert.ToBase64String(salt));
    }
    
    public bool IsPasswordValid(string password, string passwordHash, string passwordSalt)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash) || string.IsNullOrWhiteSpace(passwordSalt))
            return false;
        var salt = Convert.FromBase64String(passwordSalt);
        var hash = GenerateHash(password, salt);

        return passwordHash == Convert.ToBase64String(hash);
    }
    
    private byte[] GenerateSalt()
    {
        var saltBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
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
