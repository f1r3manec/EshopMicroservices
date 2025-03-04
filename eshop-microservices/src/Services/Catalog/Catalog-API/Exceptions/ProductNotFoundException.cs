using BuildingBlocks.Exceptions;

namespace Catalog_API.Exceptions
{
    public class ProductNotFoundException(Guid id) : NotFoundExceptions("Product", id)
    {
    }
}
