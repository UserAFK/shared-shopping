using AutoMapper;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery, ICollection<ItemDto>>
{
    private readonly IShoppingDbContext _context;
    private readonly IMapper _mapper;

    public GetAllItemsHandler(IShoppingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Items
            .AsNoTracking()
            .Take(request.Count??100)
            .OrderBy(i=>i.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<ICollection<ItemDto>>(list);
    }
}

