using AutoMapper;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public class GetAllShoppingListsHandler : IRequestHandler<GetAllShoppingListsQuery, ICollection<ShoppingListDto>>
{
    private readonly IShoppingDbContext _context;
    private readonly IMapper _mapper;

    public GetAllShoppingListsHandler(IShoppingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ShoppingListDto>> Handle(GetAllShoppingListsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.ShoppingLists
            .Include(l => l.Items)
            .AsNoTracking()
            .Take(request.Count ?? 100)
            .OrderBy(l => l.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<ICollection<ShoppingListDto>>(list);
    }
}

