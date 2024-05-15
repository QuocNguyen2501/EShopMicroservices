namespace Catalog.API.Features.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Catagory) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
	: IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
	public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
	{
		logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);
		var products = await session.Query<Product>().Where(p => p.Category.Contains(query.Catagory)).ToListAsync(cancellationToken);
		return new GetProductByCategoryResult(products);
	}
}
