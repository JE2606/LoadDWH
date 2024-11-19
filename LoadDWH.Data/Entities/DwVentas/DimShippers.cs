
using System.ComponentModel.DataAnnotations;

namespace LoadDWH.Data.Entities.DwNorthwind
{
    public class DimShippers
    {
        [Key]
        public int ShipperKey { get; set; }
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
    }
}
