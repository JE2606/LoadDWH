 #nullable disable
using System;
using System.Collections.Generic;

namespace LoadDWH.Models.DwVentas.ModelsVentas;

public partial class FactVenta
{
    public int VentaId { get; set; }

    public int? OrderKey { get; set; }

    public int? CustomerKey { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string ShipName { get; set; }

    public decimal? Total { get; set; }

    public int? Cantidad { get; set; }

    public string CompanyName { get; set; }

    public string EmployeeName { get; set; }

    public int? DataKey { get; set; }
}