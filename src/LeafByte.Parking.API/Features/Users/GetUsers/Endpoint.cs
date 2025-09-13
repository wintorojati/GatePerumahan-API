using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Users.GetUsers;

public class GetUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (IMediator mediator, CancellationToken ct, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10) =>
        {
            var query = new Query(new PaginationRequest(pageIndex, pageSize));
            var result = await mediator.Send(query, ct);
            return Results.Ok(result.Result);
        })
        .WithName("GetUsers")
        .WithTags("Users");
    }
}
