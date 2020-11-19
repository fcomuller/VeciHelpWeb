using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VeciHelpWeb.Interface;
using VeciHelpWeb.Models;

namespace VeciHelpWeb.Controllers
{
    public class LoginController : Controller
    {
        public string BaseAddress = "http://192.168.1.222/vecihelp/api/v1/";
        public Login log;


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> PostLogin(Login log)
        {
            if (log.Correo!=null && log.Correo.Trim().Length>=5 && log.Clave != null && log.Clave.Trim().Length >= 5)
            {
                Usuario usr = new Usuario();
                log.TokenFireBase = "Web";
                log.Clave = Encriptar(log.Clave);

                //var usr=await validaLogin(log).Result;

                var endPoint = RestService.For<ILogin>(BaseAddress);

                var jsonstri = JsonConvert.SerializeObject(log);


                var response = await endPoint.PostLogin(log);



                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //obtengo el contenido del HttpResponseMessage como string
                    var jsonString = await response.Content.ReadAsStringAsync();

                    //convierto el contenido de json al objeto usuario
                    usr = JsonConvert.DeserializeObject<Usuario>(jsonString);
                }


                if (usr != null)
                {
                    Session.Add("UserLogin", usr);
                    return RedirectToAction("Index", "Principal");
                }
            }
            return View("Login");
        }


        private string Encriptar(string clave)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(clave))).Replace("-", "");
            }
        }
    }
}