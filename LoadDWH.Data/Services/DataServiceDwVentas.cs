
using LoadDWH.Data.Context;
using LoadDWH.Data.Entities.DwNorthwind;
using LoadDWH.Data.Interfaces;
using LoadDWH.Data.Result;

namespace LoadDWH.Data.Services
{
    public class DataServiceDwVentas : IDataServiceDwVenta
    {
        private readonly NorwindContext _norwindContext;
        private readonly SalesContext _salesContext;

        public DataServiceDwVentas(NorwindContext norwindContext, SalesContext salesContext) 
        {
            _norwindContext = norwindContext;
            _salesContext = salesContext;
        }

        public async Task<OperationResult> LoadDimCustomers()
        {
            OperationResult result = new OperationResult();

            try
            {
                var customer = _norwindContext.Customers.Select(cos => new DimCustomers() 
                { 
                    CustomerId = cos.CustomerID,
                    CustomerName = cos.ContactName,

                }).ToList();

               await _salesContext.DimCustomers.AddRangeAsync(customer);
               await _salesContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la dimension de Customers. {ex.Message}";
            }

            return result;
        }

        public async Task<OperationResult> LoadDimEmployees()
        {
            OperationResult result = new OperationResult() ;

            try
            {
                var employees = _norwindContext.Employees.Select(emp => new DimEmployees()
                {
                    EmployeeId = emp.EmployeeID,
                    EmployeeName = string.Concat(emp.FirstName, " ", emp.LastName),
                }).ToList();

                await _salesContext.DimEmployees.AddRangeAsync(employees);
                await _salesContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                result.Success = false;
                result.Message = $"Error cargando la dimension de Employees. {ex.Message}";
            }
            return result;
            
        }

        public async Task<OperationResult> LoadDimProducts()
        {

            OperationResult result = new OperationResult() ;

            try
            {
                var productCategories = (from product in _norwindContext.Products
                                         join category in _norwindContext.Categories on product.CategoryID equals category.CategoryID
                                         where product.CategoryID < 0
                                         select new DimProducts()
                                         {
                                             CategoryID = category.CategoryID,
                                             ProductName = product.ProductName,
                                             CategoryName = category.CategoryName,
                                             ProductID = product.ProductID,
                                         }).ToList();
                await _salesContext.DimProducts.AddRangeAsync(productCategories);
                await _salesContext.SaveChangesAsync();
            }
            catch (Exception ex) {
                result.Success = false;
                result.Message = $"Error cargando la dimension de Productos con las Categorias. {ex.Message}";
            }

            return result;
            

        }

        public async Task<OperationResult> LoadDimShippers()
        {
            OperationResult result = new OperationResult();

            try
            {
                var shippers = _norwindContext.Shippers.Select(ship => new DimShippers()
                {
                    ShipperID = ship.ShipperID,
                    CompanyName = ship.CompanyName
                }).ToList();

                await _salesContext.DimShippers.AddRangeAsync(shippers);
                await _salesContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la dimension de Shippers. {ex.Message}";
            }

            return result;
        }
    }
}
