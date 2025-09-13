using MediatR;
using LeafByte.Parking.API.Models;
using LeafByte.Parking.API.Services;
using Microsoft.EntityFrameworkCore;

namespace LeafByte.Parking.API.Features.Auth.Login;

public class Handler : IRequestHandler<Command, Response>
{
    private readonly ApplicationDbContext _context;
    private readonly AuthService _authService;

    public Handler(ApplicationDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username && u.Status == Status.Active, cancellationToken);

        if (user is null || !VerifyPassword(request.Password, user.Password, user.Salt))
            throw new UnauthorizedAccessException("Invalid username or password");

        var token = _authService.GenerateJwtToken(user);

        return new Response(
            token,
            user.Id,
            user.Username,
            user.Role);
    }

    private static bool VerifyPassword(string enteredPassword, string storedPassword, string salt)
    {
        // Implement password verification logic (e.g., using HMAC or PBKDF2)
        // For now, we'll do a simple comparison (in production, use proper hashing)
        return enteredPassword == storedPassword;
    }
}
