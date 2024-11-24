using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWH.Data.Entities.DwVentas
{
    [Table("FactVentas", Schema = "dbo")]
    public class FactVentas
    {
        [Key]
        public int VentaId { get; set; }

        public int? OrderKey { get; set; }

        public int? CustomerKey { get; set; }

        public DateOnly? OrderDate { get; set; }

        public string? ShipName { get; set; }

        public decimal? Total { get; set; }

        public int? Cantidad { get; set; }

        public string? CompanyName { get; set; }

        public string? EmployeeName { get; set; }

        public int? DataKey { get; set; }

                 public int? EmployeeID { get; set; }                   public int? ShipperID { get; set; }                    public string? ShipperName { get; set; }               public string? Country { get; set; }                   public int? ProductID { get; set; }                    public string? ProductName { get; set; }               public int? Year { get; set; }                         public int? Month { get; set; }                        public decimal? TotalVentas { get; set; }          }
}
