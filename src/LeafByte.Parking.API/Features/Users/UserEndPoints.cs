using Carter;

namespace LeafByte.Parking.API.Features.Users;

public static class UserEndPoints
{
    public static void AddUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users").WithTags("Users");

        group.MapPost("/", CreateUserEndpoint.AddRoute);
        group.MapGet("/", GetUsersEndpoint.AddRoute);
        group.MapGet("/{id}", GetUserByIdEndpoint.AddRoute);
        group.MapPut("/{id}", UpdateUserEndpoint.AddRoute);
        group.MapDelete("/{id}", DeleteUserEndpoint.AddRoute);
    }
}
