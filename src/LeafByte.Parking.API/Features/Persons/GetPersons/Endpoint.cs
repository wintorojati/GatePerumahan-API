using Carter;
using MediatR;
using LeafByte.Parking.CrossCutting.Pagination;

namespace LeafByte.Parking.API.Features.Persons.GetPersons;

public static class GetPersonsEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (int? pageNumber, int? pageSize, IMediator mediator, CancellationToken ct) =>
        {
            var pagination = new PaginationRequest(
                pageNumber.HasValue ? pageNumber.Value - 1 : 0,
                pageSize ?? 10);
            var query = new Query(pagination);
            var result = await mediator.Send(query, ct);
            return Results.Ok(result);
        })
        .WithName("GetPersons");
    }
}
