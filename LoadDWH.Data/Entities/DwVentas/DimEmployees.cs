
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWH.Data.Entities.DwNorthwind
{

    [Table("DimEmployees")]

    public class DimEmployees
    {
        [Key]
        public int EmployeeKey { get; set; }
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
    }
}
