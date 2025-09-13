using Carter;
using LeafByte.Parking.API.Features.Users.CreateUser;
using LeafByte.Parking.API.Features.Users.DeleteUser;
using LeafByte.Parking.API.Features.Users.GetUserById;
using LeafByte.Parking.API.Features.Users.GetUsers;
using LeafByte.Parking.API.Features.Users.UpdateUser;
using Microsoft.AspNetCore.Authorization;

namespace LeafByte.Parking.API.Features.Users;

public static class UserEndPoints
{
    public static void AddUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users").WithTags("Users").RequireAuthorization();

        group.MapPost("/", CreateUserEndpoint.AddRoute);
        group.MapGet("/", GetUsersEndpoint.AddRoute);
        group.MapGet("/{id}", GetUserByIdEndpoint.AddRoute);
        group.MapPut("/{id}", UpdateUserEndpoint.AddRoute);
        group.MapDelete("/{id}", DeleteUserEndpoint.AddRoute);
    }
}
