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
                c.CreateMap<ShoppingList, ListDto>();
                c.CreateMap<Item, ItemDto>();
                c.CreateMap<Item, ItemDetailedDto>();
            });
            return services;
        }
    }
}
