using Carter;
using LeafByte.Parking.API.Features.Persons.CreatePerson;
using LeafByte.Parking.API.Features.Persons.DeletePerson;
using LeafByte.Parking.API.Features.Persons.GetPerson;
using LeafByte.Parking.API.Features.Persons.GetPersons;
using LeafByte.Parking.API.Features.Persons.UpdatePerson;
using Microsoft.AspNetCore.Authorization;

namespace LeafByte.Parking.API.Features.Persons;

public static class PersonEndPoints
{
    public static void AddPersonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/persons").WithTags("Persons").RequireAuthorization();

        group.MapPost("/", CreatePersonEndpoint.AddRoute);
        group.MapGet("/", GetPersonsEndpoint.AddRoute);
        group.MapGet("/{id}", GetPersonEndpoint.AddRoute);
        group.MapPut("/{id}", UpdatePersonEndpoint.AddRoute);
        group.MapDelete("/{id}", DeletePersonEndpoint.AddRoute);
    }
}
