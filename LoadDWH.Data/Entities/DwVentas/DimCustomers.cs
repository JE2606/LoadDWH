
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWH.Data.Entities.DwNorthwind
{

    [Table("DimCustomers")]

    public class DimCustomers
    {
        [Key]
        public int CustomerKey { get; set; } 
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
    }
}
