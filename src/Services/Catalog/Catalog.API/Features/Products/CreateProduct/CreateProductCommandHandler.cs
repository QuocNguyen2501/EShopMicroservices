namespace Catalog.API.Features.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
	: ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandVaidator : AbstractValidator<CreateProductCommand>
{
	public CreateProductCommandVaidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is not empty");
		RuleFor(x => x.Category).NotEmpty().WithMessage("Category is not empty");
		RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Imagefile is required");
		RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
	}
}

internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{

	private IDocumentSession _session;
	private ILogger<CreateProductCommandHandler> _logger;

	public CreateProductCommandHandler(
		IDocumentSession session,
		ILogger<CreateProductCommandHandler> logger)
	{
		_session = session;
		_logger = logger;
	}

	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		_logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

		var product = new Product
		{
			Name = command.Name,
			Category = command.Category,
			Description = command.Description,
			ImageFile = command.ImageFile,
			Price = command.Price
		};

		_session.Store(product);
		await _session.SaveChangesAsync(cancellationToken);

		return new CreateProductResult(product.Id);
	}
}
