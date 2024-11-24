 #nullable disable
using System;
using System.Collections.Generic;

namespace LoadDWH.Models.DwVentas.ModelsVentas;

public partial class FactServedCustomer
{
    public int ServedCustomerId { get; set; }

    public int? EmployeeKey { get; set; }

    public int? TotalCustomersServed { get; set; }

    public string EmployeeName { get; set; }

    public int? DataKey { get; set; }
}