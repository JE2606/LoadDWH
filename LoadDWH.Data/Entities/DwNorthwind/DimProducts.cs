
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWH.Data.Entities.DwNorthwind
{

    [Table("DimProducts")]

    public class DimProducts
    {
        public int ProductKey { get; set; } 
        public int ProductID { get; set; }  
        public string ProductName { get; set; } 
        public int CategoryID { get; set; }      
        public string CategoryName { get; set; }
    }
}
