using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeciHelpWeb.Models
{
    public class OrganizacionRequest
    {
        public int idOrganizacion { get; set; }
        public int idComuna { get; set; }
        public string nombre { get; set; }
    }
}