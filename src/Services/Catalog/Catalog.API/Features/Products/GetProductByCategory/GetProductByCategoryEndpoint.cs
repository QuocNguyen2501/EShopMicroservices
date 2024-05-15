namespace Catalog.API.Features.Products.GetProductByCategory;

public record GetProductByCatagoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
		{
			var result = await sender.Send(new GetProductByCategoryQuery(category));
			var response = result.Adapt<GetProductByCatagoryResponse>();
			return Results.Ok(response);
		})
		.WithName("GetProductByCategory")
		.Produces<GetProductByCatagoryResponse>()
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Product By Category")
		.WithDescription("Get Product By Category")
		.WithOpenApi();
	}
}
