namespace Basket.API.Features.Basket.GetBasket;

public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

internal class GetBasketQueryHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
	public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
	{
		var basket = await _repository.GetBasket(request.UserName);
		var response = basket.Adapt<GetBasketResult>();

		return response;
	}
}
