using AutoMapper;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public class GetAllListsHandler : IRequestHandler<GetAllListsQuery, ICollection<ListDto>>
{
    private readonly IShoppingDbContext _context;
    private readonly IMapper _mapper;

    public GetAllListsHandler(IShoppingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<ListDto>> Handle(GetAllListsQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.ShoppingLists
            .Include(l => l.Items)
            .AsNoTracking()
            .Take(request.Count ?? 100)
            .OrderBy(l => l.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<ICollection<ListDto>>(list);
    }
}

