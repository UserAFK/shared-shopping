using Core.Domain.Entities;
using MediatR;

namespace Service.Command.Features.Items;

public record UpdateItemCommand(Item Item) : IRequest<Guid>;
