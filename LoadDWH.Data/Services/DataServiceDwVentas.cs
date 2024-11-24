
using LoadDWH.Data.Context;
using LoadDWH.Data.Entities.DwNorthwind;
using LoadDWH.Data.Entities.DwVentas;
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

            using (var transaction = await _salesContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _salesContext.DimCustomers.RemoveRange(_salesContext.DimCustomers);
                    _salesContext.DimEmployees.RemoveRange(_salesContext.DimEmployees);
                    _salesContext.DimProducts.RemoveRange(_salesContext.DimProducts);
                    _salesContext.DimShippers.RemoveRange(_salesContext.DimShippers);
                    _salesContext.FactServedCustomer.RemoveRange(_salesContext.FactServedCustomer);
                    _salesContext.FactVentas.RemoveRange(_salesContext.FactVentas);


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
            OperationResult result = new OperationResult();

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

            OperationResult result = new OperationResult();

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
            catch (Exception ex)
            {
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

                await LoadFactVentas();
                await LoadFactServedCustomers();

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando el DWH Sales {ex.Message}";
            }


            return result;
        }

        private async Task<OperationResult> LoadFactVentas()
        {
            OperationResult result = new OperationResult();
            try
            {
                                 var ventas = await _norwindContext.Vwventa.AsNoTracking().ToListAsync();

                                 var ventasId = await _salesContext.FactVentas.Select(cd => cd.VentaId).ToArrayAsync();
                if (ventasId.Any())
                {
                    await _salesContext.FactVentas
                        .Where(cd => ventasId.Contains(cd.VentaId))
                        .AsNoTracking()
                        .ExecuteDeleteAsync();
                }

                                 var customers = await _salesContext.DimCustomers.ToListAsync();
                var employees = await _salesContext.DimEmployees.ToListAsync();
                var shippers = await _salesContext.DimShippers.ToListAsync();
                var products = await _salesContext.DimProducts.ToListAsync();

                                 List<FactVentas> factVentasList = new List<FactVentas>();

                                 foreach (var venta in ventas)
                {
                    var customer = customers.SingleOrDefault(cust => cust.CustomerId == venta.CustomerKey);
                    var employee = employees.SingleOrDefault(emp => emp.EmployeeId == venta.EmployeeID);
                    var shipper = shippers.SingleOrDefault(ship => ship.ShipperID == venta.ShipperID);
                    var product = products.SingleOrDefault(pro => pro.ProductID == venta.ProductID);

                                         if (product == null)
                    {
                        Console.WriteLine($"Producto no encontrado para ProductID: {venta.ProductID}. Venta omitida.");
                        continue;
                    }
                    if (customer == null)
                    {
                        Console.WriteLine($"Cliente no encontrado para CustomerKey: {venta.CustomerKey}. Venta omitida.");
                        continue;
                    }
                    if (employee == null)
                    {
                        Console.WriteLine($"Empleado no encontrado para EmployeeID: {venta.EmployeeID}. Venta omitida.");
                        continue;
                    }
                    if (shipper == null)
                    {
                        Console.WriteLine($"Transportista no encontrado para ShipperID: {venta.ShipperID}. Venta omitida.");
                        continue;
                    }

                   
                    FactVentas factVentas = new FactVentas()
                    {
                        TotalVentas = venta.TotalVentas,
                        Country = venta.Country,
                        CustomerKey = customer.CustomerKey,
                        EmployeeName = employee.EmployeeName,
                        DataKey = venta.DateKey,
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        ShipName = shipper.CompanyName,
                        Month = venta.Month
                    };

                    factVentasList.Add(factVentas);
                }

                
                if (factVentasList.Any())
                {
                    await _salesContext.FactVentas.AddRangeAsync(factVentasList);
                    await _salesContext.SaveChangesAsync();
                }

                result.Success = true;
                result.Message = "Datos cargados correctamente en FactVentas.";
            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = $"Error al actualizar la base de datos: {dbEx.Message}";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando el DWH Ventas: {ex.Message}";
            }
            return result;
        }

        private async Task<OperationResult> LoadFactServedCustomers()
        {
            OperationResult result = new OperationResult();
            try
            {
               
                var servedCustomers = await _norwindContext.VwservedCustomer.AsNoTracking().ToListAsync();

                
                var employees = await _salesContext.DimEmployees.ToListAsync();

                
                List<FactServedCustomer> factServedCustomers = new List<FactServedCustomer>();

                foreach (var served in servedCustomers)
                {
                   
                    var employee = employees.SingleOrDefault(emp => emp.EmployeeKey == served.EmployeeKey);

                    if (employee != null)
                    {
                       
                        FactServedCustomer factServed = new FactServedCustomer()
                        {
                            
                            EmployeeName = served.EmployeeName, 
                            TotalCustomersServed = served.TotalCustomersServed,
                            DataKey = served.DataKey
                        };

                        
                        factServedCustomers.Add(factServed);
                    }
                }

                
                if (factServedCustomers.Any())
                {
                    await _salesContext.FactServedCustomer.AddRangeAsync(factServedCustomers);
                    await _salesContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando el DWH Clientes Atendidos: {ex.Message}";
            }
            return result;
        }

    }
}
