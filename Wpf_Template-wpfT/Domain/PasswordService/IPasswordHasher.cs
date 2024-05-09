namespace Domain.PasswordService;

public interface IPasswordHasher
{
    string? GetHashPassword(string password);
}