namespace RavenShop.Application.Services.v1;

public class ProductService
{
    private readonly IRavenDbRepository<Product> _repository;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;

    public ProductService(IRavenDbRepository<Product> repository, ILogger<ProductService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public ProductModel? FindOne(SelectProduct selectProduct)
    {
        try
        {
            var product = _repository.Select(selectProduct.id);

            _logger.LogInformation("Product obtained successfully");

            var productModel = _mapper.Map<ProductModel>(product);
            return productModel;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error getting product - Exception message: {ex.Message}");
            return null;
        }
    }

    public List<ProductModel> FindAll(int pageSize, int pageNumber)
    {
        try
        {
            var products = _repository.SelectAll(pageSize, pageNumber);

            var productModels = products
                .ToList()
                .Select(p => _mapper.Map<ProductModel>(p))
                .ToList();

            _logger.LogInformation("Products obtained successfully");
            return productModels;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error getting products - Exception message: {ex.Message}");
            return new List<ProductModel>();
        }
    }

    public string Create(ProductModel productModel)
    {
        var product = _mapper.Map<Product>(productModel);

        try
        {
            _repository.Create(product);
            _logger.LogInformation("Product created successfully");
            return string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating product - Exception message: {ex.Message}");
            return ex.Message;
        }
    }

    public string Update(ProductModel productModel)
    {
        var product = _mapper.Map<Product>(productModel);

        try
        {
            _repository.Update(product);
            _logger.LogInformation("Product updated successfully");
            return string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating product - Exception message: {ex.Message}");
            return ex.Message;
        }
    }

    public string Delete(string id)
    {
        try
        {
            _repository.Delete(id);
            _logger.LogInformation("Product deleted successfully");
            return string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting the product - Exception message: {ex.Message}");
            return ex.Message;
        }
    }
}
