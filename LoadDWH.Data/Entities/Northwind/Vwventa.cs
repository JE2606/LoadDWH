
namespace LoadDWH.Data.Entities.Northwind
{
    public class Vwventa
    {
        public int OrderKey { get; set; }

        public string CustomerKey { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? ShipName { get; set; }

        public decimal? Total { get; set; }

        public int? Cantidad { get; set; }

        public string? CompanyName { get; set; }

        public string? EmployeeName { get; set; }

        public int? DataKey { get; set; }

    }
}
