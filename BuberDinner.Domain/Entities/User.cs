using System.Security.Cryptography.X509Certificates;

namespace BuberDinner.Domain.Entities;

public class User
{
    public User(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public string Password { get; } = null!;
}