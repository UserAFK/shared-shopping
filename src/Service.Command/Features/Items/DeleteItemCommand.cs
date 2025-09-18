using MediatR;

namespace Service.Command.Features.Items;

public record DeleteItemCommand(Guid ItemId) : IRequest<Guid>;
