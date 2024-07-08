using System;
using System.Collections.Generic;

namespace OrderWebApi.Models;

public partial class Orden
{
    public int IdOrden { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public bool Estado { get; set; }
}
