namespace Catalog.API.Features.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id): ICommand<DeleteProductCommandResult>;
public record DeleteProductCommandResult(bool isSuccess);

public class DeleteProductCommandValidator: AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
		RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
	}
}

internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
{
	public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
	{
		logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", command);
		var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
		if(product is null)
		{
			throw new ProductNotFoundException(command.Id);
		}
		session.Delete(product);
		await session.SaveChangesAsync();

		return new DeleteProductCommandResult(true);
	}
}
