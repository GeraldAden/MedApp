namespace MedApp.Application.Abstractions.Models;
public class User
{
    public User(string firstName, string lastName, string username, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Email = email;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string Username { get; }
    public string Email { get; }
}