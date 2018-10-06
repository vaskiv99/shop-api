using FluentValidation;
using ShopService.Services.Commands;

namespace ShopService.Services.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(255).WithMessage("Length must be less then 255").NotEmpty().NotNull();
            RuleFor(x => x.FirstName).MaximumLength(255).WithMessage("Length must be less then 255").NotEmpty().NotNull();
            RuleFor(x => x.LastName).MaximumLength(255).WithMessage("Length must be less then 255").NotEmpty().NotNull();
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
            RuleFor(x => x.Password).MinimumLength(8).NotEmpty().NotNull();
            RuleFor(x => x.ConfirmPassword)
                .Must((password, confirmPassword) => password.Password.Equals(confirmPassword));
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(255).WithMessage("Length must be less then 255").NotEmpty().NotNull();
            RuleFor(x => x.Password).MinimumLength(8).NotEmpty().NotNull();
        }
    }

    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Length must be less then 255").NotEmpty().NotNull();
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Length must be less then 255").NotEmpty().NotNull();
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0").NotNull();
        }
    }

    public class CreateGoodsValidator : AbstractValidator<CreateGoodsCommand>
    {
        public CreateGoodsValidator()
        {
            RuleFor(x => x.Name).MaximumLength(255).WithMessage("Length must be less then 255").NotEmpty().NotNull();
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.CategoryIds).NotEmpty().NotNull().WithMessage("Goods must has minimum one category");
        }
    }

    public class UpdateGoodsValidator : AbstractValidator<UpdateGoodsCommand>
    {
        public UpdateGoodsValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0").NotNull();
        }
    }

    public class DeleteGoodsValidator : AbstractValidator<DeleteGoodsCommand>
    {
        public DeleteGoodsValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0").NotNull();
        }
    }

    public class AddItemToBasketValidator : AbstractValidator<AddItemToBasket>
    {
        public AddItemToBasketValidator()
        {
            RuleFor(x => x.GoodsId).GreaterThan(0).WithMessage("Goods id must be greater than 0").NotNull();
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("Count must be greater than 0").NotNull();
        }
    }

    public class RemoveItemFromBasketValidator : AbstractValidator<DeleteItemFromBasket>
    {
        public RemoveItemFromBasketValidator()
        {
            RuleFor(x => x.GoodsId).GreaterThan(0).WithMessage("Goods id must be greater than 0").NotNull();
            RuleFor(x => x.Count).GreaterThan(0).WithMessage("Count must be greater than 0").NotNull();
        }
    }
}
