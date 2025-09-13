﻿//using MediatR;

namespace LeafByte.Parking.CrossCutting.CQRS;
public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}
