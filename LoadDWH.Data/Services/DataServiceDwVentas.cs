
using LoadDWH.Data.Context;
using LoadDWH.Data.Entities.DwNorthwind;
using LoadDWH.Data.Interfaces;
using LoadDWH.Data.Result;
using Microsoft.EntityFrameworkCore;

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

        private async Task ClearDimTablesAsync()
        {
            // Usamos transacciones para asegurar que todas las eliminaciones sean atómicas
            using (var transaction = await _salesContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _salesContext.DimCustomers.RemoveRange(_salesContext.DimCustomers);
                    _salesContext.DimEmployees.RemoveRange(_salesContext.DimEmployees);
                    _salesContext.DimProducts.RemoveRange(_salesContext.DimProducts);
                    _salesContext.DimShippers.RemoveRange(_salesContext.DimShippers);

                    
                    await _salesContext.SaveChangesAsync();

                    
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Error al limpiar las tablas Dim: {ex.Message}");
                }
            }
        }

        private async Task<OperationResult> LoadDimCustomers()
        {
            OperationResult result = new OperationResult();

            try
            {
                var customer = _norwindContext.Customers.AsNoTracking().Select(cos => new DimCustomers() 
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

        private async Task<OperationResult> LoadDimEmployees()
        {
            OperationResult result = new OperationResult() ;

            try
            {
                var employees = _norwindContext.Employees.AsNoTracking().Select(emp => new DimEmployees()
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

        private async Task<OperationResult> LoadDimProducts()
        {

            OperationResult result = new OperationResult() ;

            try
            {
                var productCategories = (from product in _norwindContext.Products
                                         join category in _norwindContext.Categories on product.CategoryID equals category.CategoryID
                                         select new DimProducts()
                                         {
                                             CategoryID = category.CategoryID,
                                             ProductName = product.ProductName,
                                             CategoryName = category.CategoryName,
                                             ProductID = product.ProductID,
                                         }).AsNoTracking().ToList();
                await _salesContext.DimProducts.AddRangeAsync(productCategories);
                await _salesContext.SaveChangesAsync();
            }
            catch (Exception ex) {
                result.Success = false;
                result.Message = $"Error cargando la dimension de Productos con las Categorias. {ex.Message}";
            }

            return result;
            

        }

        private async Task<OperationResult> LoadDimShippers()
        {
            OperationResult result = new OperationResult();

            try
            {
                var shippers = _norwindContext.Shippers.AsNoTracking().Select(ship => new DimShippers()
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

        public async Task<OperationResult> LoadDWH()
        {
            OperationResult result = new OperationResult();

            try
            {
                await ClearDimTablesAsync();

                await LoadDimEmployees();
                await LoadDimCustomers();
                await LoadDimProducts();
                await LoadDimShippers();

            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando el DWH Sales {ex.Message}";
            }


            return result;
        }
    }
}
