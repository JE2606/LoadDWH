
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWH.Data.Entities.DwVentas
{
    [Table("FactServedCustomers", Schema ="dbo")]
    public class FactServedCustomer
    {
        [Key]
        public int ServedCustomerId { get; set; }

        public int? EmployeeKey { get; set; }

        public int? TotalCustomersServed { get; set; }

        public string? EmployeeName { get; set; }

        public int? DataKey { get; set; }
    }
}
