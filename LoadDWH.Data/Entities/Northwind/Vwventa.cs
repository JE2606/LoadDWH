
namespace LoadDWH.Data.Entities.Northwind
{
    public class VwVenta
    {
        public string? CustomerKey { get; set; } 

        public string? CustomerName { get; set; }

        public int EmployeeID { get; set; }
        public string? EmployeeName { get; set; }

        public int ShipperID { get; set; }
        public string? ShipperName { get; set; }

        public string? Country { get; set; }

        public int ProductID { get; set; }
        public string? ProductName { get; set; }  

        public int DateKey { get; set; }  
        public int Year { get; set; }  
        public int Month { get; set; }  

        public decimal TotalVentas { get; set; }  
        public int Cantidad { get; set; }  
    }

}
