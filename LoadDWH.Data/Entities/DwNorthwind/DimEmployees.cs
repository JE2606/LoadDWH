﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWH.Data.Entities.DwNorthwind
{

    [Table("DimEmployees")]

    public class DimEmployees
    {
        public int EmployeeKey { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}