using AutoMapper;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Query.DTO;

namespace Service.Query.Features.ShoppingList;

public class GetListByIdHandler : IRequestHandler<GetListByIdQuery, ListDto>
{
    private readonly IShoppingDbContext _context;
    private readonly IMapper _mapper;

    public GetListByIdHandler(IShoppingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListDto> Handle(GetListByIdQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.ShoppingLists
            .Include(l => l.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == request.ListId, cancellationToken);
        return _mapper.Map<ListDto>(list);
    }
}

