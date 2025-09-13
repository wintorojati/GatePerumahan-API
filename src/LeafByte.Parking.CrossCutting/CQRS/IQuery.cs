//using MediatR;

namespace LeafByte.Parking.CrossCutting.CQRS;

public interface IQuery<out TResponse>;
//public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull;
