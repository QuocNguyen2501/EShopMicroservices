namespace Basket.API.Features.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
		RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

internal class DeleteBasketCommandHandler(IBasketRepository _repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
	public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
	{
		//TODO: delete basket from database and cache
		await _repository.DeleteBasket(command.UserName);

		return new DeleteBasketResult(true);
	}
}
