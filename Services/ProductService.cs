using System.Data;
using Dapper;

public class ProductService : BaseService, IProductService
{
    public ProductService(IConfiguration config) : base(config)
    {
    }

    public async Task<int> CreateProductAsync(Product entity)
    {
        try
        {
            var query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
    
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Price", entity.Price, DbType.Decimal);
            parameters.Add("Quantity", entity.Quantity, DbType.Int32);
    
            using (var connection = CreateConnection())
            {
                return (await connection.ExecuteAsync(query, parameters));
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<int> DeleteProductAsync(Product entity)
    {
        try
        {
        var query = "DELETE FROM Products WHERE Id = @Id";
 
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id, DbType.Int32);
 
        using (var connection = CreateConnection())
        {
            return (await connection.ExecuteAsync(query, parameters));
        }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        try 
        {
            var query = "SELECT * FROM Products";
            using (var connection  = CreateConnection())
            {
                return (await connection.QueryAsync<Product>(query)).ToList();
            }
        }
        catch (Exception ex)
        {
            throw new Exception (ex.Message, ex);
        }
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        try 
        {
            var query = "SELECT * FROM Products WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using ( var connection = CreateConnection())
            {
                return (await connection.QueryFirstOrDefaultAsync<Product>(query, parameters));
            }
        }
        catch (Exception ex) 
        {
            throw new Exception (ex.Message, ex);
        }
    }

    public async Task<int> UpdateProductAsync(Product entity)
    {
        try
        {
            var query = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id";
    
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Price", entity.Price, DbType.Decimal);
            parameters.Add("Quantity", entity.Quantity, DbType.Int32);
            parameters.Add("Id", entity.Id, DbType.Int32);
    
            using (var connection = CreateConnection())
            {
                return (await connection.ExecuteAsync(query, parameters));
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}