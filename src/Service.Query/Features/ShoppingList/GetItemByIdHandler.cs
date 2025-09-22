using AutoMapper;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, ItemDetailedDto>
{
    private readonly IShoppingDbContext _context;
    private readonly IMapper _mapper;

    public GetItemByIdHandler(IShoppingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ItemDetailedDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);
        return _mapper.Map<ItemDetailedDto>(list);
    }
}

