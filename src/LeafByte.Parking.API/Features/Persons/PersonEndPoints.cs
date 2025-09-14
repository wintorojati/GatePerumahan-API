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

        CreatePersonEndpoint.AddRoute(group);
        GetPersonsEndpoint.AddRoute(group);
        GetPersonEndpoint.AddRoute(group);
        UpdatePersonEndpoint.AddRoute(group);
        DeletePersonEndpoint.AddRoute(group);
    }
}
