using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeciHelpWeb.Models
{
    public class Organizacion
    {
        public int idOrganizacion { get; set; }
        public string nombreOrg { get; set; }
        public int idComuna { get; set; }
        public string correoAdmin { get; set; }
        public string mensaje { get; set; }
    }
}