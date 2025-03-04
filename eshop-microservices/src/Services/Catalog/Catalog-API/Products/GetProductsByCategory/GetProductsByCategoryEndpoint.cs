
using Catalog_API.Products.GetProductById;

namespace Catalog_API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryResponse(IEnumerable<Product> Products) ;
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products/category/{category}", async (string category, CancellationToken cancellationToken, ISender sender) =>
           {
               var result = await sender.Send(new GetProductsByCategoryQuery(category));
               var response = result.Adapt<GetProductsByCategoryResponse>();
               return Results.Ok(response);
           })
               .WithName("GetProduct Category")
              .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Get Product By Category")
              .WithDescription("Get Product by Category"); ;
        }
    }
}
