using TestApp.Repositories;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public string GetProductName(int id)
    {
        var product = _repository.GetProductById(id);

        if (product == null)
            return "Product Not Found";

        return product.Name;
    }
}