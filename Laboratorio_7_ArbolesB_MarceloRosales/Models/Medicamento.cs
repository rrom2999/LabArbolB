using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio_7_ArbolesB_MarceloRosales.Models
{
    public class Medicamento
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CasaFarmaceutia { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }


        public Medicamento crear(int _codigo, string _Nombre, string _descripcion, string _CasaFarmaceutica, double _Precio, int _Existencia)
        {
            Medicamento temp = new Medicamento();
            temp.Codigo = _codigo;
            temp.Nombre = _Nombre;
            temp.Descripcion = _descripcion;
            temp.CasaFarmaceutia = _CasaFarmaceutica;
            temp.Precio = _Precio;
            temp.Existencia = _Existencia;

            return temp;
        }
    }
}