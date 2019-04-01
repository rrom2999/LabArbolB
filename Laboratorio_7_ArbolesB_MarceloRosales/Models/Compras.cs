using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio_7_ArbolesB_MarceloRosales.Models
{
    public class Compras
    {
        public string Nombre { get; set; }
        public string NIT { get; set; }
        public string Farmaco { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double Total { get; set; }

        public Compras llenar(string nombre, string _NIT, string farmaco, int cantidad, double PrecioUnitario)
        {
            Compras llenado = new Compras();
            llenado.Nombre = nombre;
            llenado.NIT = _NIT;
            llenado.Farmaco = farmaco;
            llenado.Cantidad = cantidad;
            llenado.PrecioUnitario = PrecioUnitario;
            llenado.Total = cantidad * PrecioUnitario;
            return llenado;
        }
    }
}