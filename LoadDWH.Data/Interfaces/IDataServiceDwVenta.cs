
using LoadDWH.Data.Entities.DwNorthwind;
using LoadDWH.Data.Result;

namespace LoadDWH.Data.Interfaces
{
    public interface IDataServiceDwVenta
    {
        Task<OperationResult> LoadDimEmployees();
        Task<OperationResult> LoadDimCustomers();
        Task<OperationResult> LoadDimProducts();
        Task<OperationResult> LoadDimShippers();    
            
    }
}
