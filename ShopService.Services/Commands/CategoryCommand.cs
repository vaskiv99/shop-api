using MediatR;
using ShopService.Services.Responses;

namespace ShopService.Services.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public string Name { get; set; }
    }

    public class UpdateCategoryCommand : CreateCategoryCommand
    {
        public long Id { get; set; }
    }

    public class DeleteCategoryCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
