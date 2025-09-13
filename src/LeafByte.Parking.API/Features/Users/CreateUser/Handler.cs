using MediatR;
using LeafByte.Parking.API.Data;
using LeafByte.Parking.API.Models;
using LeafByte.Parking.CrossCutting.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LeafByte.Parking.API.Features.Users.CreateUser;

public class CreateUserHandler(ApplicationDbContext context) : IRequestHandler<Command, Response>
{
    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        // Check if username already exists
        if (await context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken))
        {
            throw new BadRequestException("Username already exists");
        }

        // Generate salt and hash password
        var salt = GenerateSalt();
        var hashedPassword = HashPassword(request.Password, salt);

        var user = new User
        {
            Username = request.Username,
            Password = hashedPassword,
            Salt = salt,
            Role = request.Role,
            CreatedAt = DateTime.UtcNow,
            Status = Status.Active
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return new Response(user.Id);
    }

    private string GenerateSalt()
    {
        var saltBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }

    private string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var combined = password + salt;
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
        return Convert.ToBase64String(hashBytes);
    }
}
