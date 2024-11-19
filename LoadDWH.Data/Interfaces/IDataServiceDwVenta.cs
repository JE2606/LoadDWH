
using LoadDWH.Data.Entities.DwNorthwind;
using LoadDWH.Data.Result;

namespace LoadDWH.Data.Interfaces
{
    public interface IDataServiceDwVenta
    {

        Task<OperationResult> LoadDWH();

    }
}
