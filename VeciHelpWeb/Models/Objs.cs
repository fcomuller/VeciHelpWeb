using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeciHelpWeb.Models
{
    public class Pais
    {
        public int idPais { get; set; }
        public string nombre { get; set; }
    }

    public class Region
    {
        public int idRegion { get; set; }

        public string nombre { get; set; }
    }

    public class Provincia
    {
        public int idProvincia { get; set; }
        public string nombre { get; set; }

    }

    public class Ciudad
    {
        public int idCiudad { get; set; }
        public string nombre { get; set; }
    }

    public class Comuna
    {
        public int idComuna { get; set; }
        public string nombre { get; set; }
    }
}