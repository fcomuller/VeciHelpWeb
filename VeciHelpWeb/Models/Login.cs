using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VeciHelpWeb.Models
{
    public class Login
    {

        [Required(ErrorMessage = "Debe escribir un correo")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe escribir una contraseña")]
        public string Clave { get; set; }

        [Required]
        [JsonProperty("tokenFireBase")]
        public string TokenFireBase { get; set; }
    }
}