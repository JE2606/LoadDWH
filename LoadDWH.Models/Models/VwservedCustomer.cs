﻿ #nullable disable
using System;
using System.Collections.Generic;

namespace LoadDWH.Models.Models;

public partial class VwservedCustomer
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; }

    public int? TotalCustomersServed { get; set; }
}