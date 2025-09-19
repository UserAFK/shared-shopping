using AutoMapper;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public class GetShoppingListByIdHandler : IRequestHandler<GetShoppingListByIdQuery, ShoppingListDto>
{
    private readonly IShoppingDbContext _context;
    private readonly IMapper _mapper;

    public GetShoppingListByIdHandler(IShoppingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ShoppingListDto> Handle(GetShoppingListByIdQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.ShoppingLists
            .Include(l => l.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);
        return _mapper.Map<ShoppingListDto>(list);
    }
}

