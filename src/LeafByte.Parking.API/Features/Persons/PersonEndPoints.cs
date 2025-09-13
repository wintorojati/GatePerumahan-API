using Carter;

namespace LeafByte.Parking.API.Features.Persons;

public static class PersonEndPoints
{
    public static void AddPersonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/persons").WithTags("Persons");

        group.MapPost("/", CreatePersonEndpoint.AddRoute);
        group.MapGet("/", GetPersonsEndpoint.AddRoute);
        group.MapGet("/{id}", GetPersonEndpoint.AddRoute);
        group.MapPut("/{id}", UpdatePersonEndpoint.AddRoute);
        group.MapDelete("/{id}", DeletePersonEndpoint.AddRoute);
    }
}
