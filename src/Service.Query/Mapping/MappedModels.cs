using Core.Domain.Entities;
using Service.Query.DTO;

namespace Service.Query.Mapping
{
    public static class MappedModels
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(c =>
            {
                c.CreateMap<ShoppingList, ShoppingListDto>();
                c.CreateMap<Item, ShoppingItemDto>();
                c.CreateMap<Item, ShoppingItemDetailedDto>();
            });
            return services;
        }
    }
}
