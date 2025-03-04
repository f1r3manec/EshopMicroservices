

namespace Catalog_API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image File is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class CreateProducCommandtHandler (IDocumentSession sesion) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        // private readonly IDocumentSession _session = sesion;
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
           
            //Bussines logic to create a product
            // Create product entity from command object
            var product = new Product() 
            {
                Name=command.Name,
                Category=command.Category,
                Description=command.Description,
                ImageFile=command.ImageFile,
                Price=command.Price
            };
            // save database 

            sesion.Store(product);
            await sesion.SaveChangesAsync(cancellationToken);
            // return result object

            return new CreateProductResult(product.Id);
        }
    }

}
