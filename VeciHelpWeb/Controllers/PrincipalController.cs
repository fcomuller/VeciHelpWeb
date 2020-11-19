using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VeciHelpWeb.Interface;
using VeciHelpWeb.Models;
using VeciHelpWeb.Security;

namespace VeciHelpWeb.Controllers
{
    public class PrincipalController : Controller
    {
        // GET: Principal
        public string BaseAddress = "http://192.168.1.222/vecihelp/api/v1/";
        private List<SelectListItem> _regionsItems;




        public async Task<ActionResult> Index()
        {
            var usuariologueado = (Usuario)Session["UserLogin"];

           
            

            if (usuariologueado!=null)
            {
                if (usuariologueado.rolename=="Sistemas")
                {
                    Session.Add("token", usuariologueado.token);

                    TempData["NombreUsuario"] = usuariologueado.nombre + " " + usuariologueado.apellido;
                    List<Region> region = new List<Region>();

                    var endPoint = RestService.For<IVeciHelp>(new HttpClient(new AuthenticatedHttpClientHandler(Session["token"].ToString())) { BaseAddress = new Uri(BaseAddress) });

                    //var jsonstri = JsonConvert.SerializeObject(log);


                    var response = await endPoint.GetRegion(1);


                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //obtengo el contenido del HttpResponseMessage como string
                        var jsonString = await response.Content.ReadAsStringAsync();

                        //convierto el contenido de json al objeto usuario
                        region = JsonConvert.DeserializeObject<List<Region>>(jsonString);

                        _regionsItems = new List<SelectListItem>();

                        foreach (var item in region)
                        {
                            _regionsItems.Add(new SelectListItem
                            {
                                Text = item.nombre,
                                Value = item.idRegion.ToString()
                            });
                        }
                        ViewBag.regionsItems = _regionsItems;
                        return View();

                    }
                    
                }
            }
            return Redirect("/admin/Login/Login");
        }

        public async Task<ActionResult> CrearOrganizacion(Organizacion organiza)
        {
            Organizacion orga = new Organizacion();
            RequestEnrolar enrol = new RequestEnrolar();
            OrganizacionRequest orgReq = new OrganizacionRequest();


            orgReq.nombre = organiza.nombreOrg;
            orgReq.idComuna= int.Parse(Session["id_comuna"].ToString());
            orgReq.idOrganizacion = 0;


            var endPoint = RestService.For<IVeciHelp>(new HttpClient(new AuthenticatedHttpClientHandler(Session["token"].ToString())) { BaseAddress = new Uri(BaseAddress) });
            var response = await endPoint.CrearOrganizacion(orgReq);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                //obtengo el contenido del HttpResponseMessage como string
                var jsonString = await response.Content.ReadAsStringAsync();

                //convierto el contenido de json al objeto usuario
                orga=JsonConvert.DeserializeObject<Organizacion>(jsonString);


                //asigno los valores al objeto requestEnrolar
                enrol.correo = organiza.correoAdmin;
                enrol.idOrganizacion = orga.idOrganizacion;

                var response2 = await endPoint.EnrolarAdmin(enrol);

                if (response2.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString2 = await response2.Content.ReadAsStringAsync();
                    var mensaje = JsonConvert.DeserializeObject<string>(jsonString2);
                    TempData["MensajeEnrolar"] = mensaje;
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> GetProvinciasList(int RegionId)
        {
            List<Provincia> provincia = new List<Provincia>();


            var endPoint = RestService.For<IVeciHelp>(new HttpClient(new AuthenticatedHttpClientHandler(Session["token"].ToString())) { BaseAddress = new Uri(BaseAddress) });

            var response = await endPoint.GetProvincia(RegionId);

            var jsonString = await response.Content.ReadAsStringAsync();

            //convierto el contenido de json al objeto usuario
            provincia = JsonConvert.DeserializeObject<List<Provincia>>(jsonString);


            return Json(provincia, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCiudadesList(int ProvinciaId)
        {
            List<Ciudad> ciudades = new List<Ciudad>();


            var endPoint = RestService.For<IVeciHelp>(new HttpClient(new AuthenticatedHttpClientHandler(Session["token"].ToString())) { BaseAddress = new Uri(BaseAddress) });

            var response = await endPoint.GetCiudad(ProvinciaId);

            var jsonString = await response.Content.ReadAsStringAsync();

            //convierto el contenido de json al objeto usuario
            ciudades = JsonConvert.DeserializeObject<List<Ciudad>>(jsonString);


            return Json(ciudades, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetComunasList(int CiudadId)
        {
            List<Comuna> comunas = new List<Comuna>();


            var endPoint = RestService.For<IVeciHelp>(new HttpClient(new AuthenticatedHttpClientHandler(Session["token"].ToString())) { BaseAddress = new Uri(BaseAddress) });

            var response = await endPoint.GetComuna(CiudadId);

            var jsonString = await response.Content.ReadAsStringAsync();

            //convierto el contenido de json al objeto usuario
            comunas = JsonConvert.DeserializeObject<List<Comuna>>(jsonString);


            return Json(comunas, JsonRequestBehavior.AllowGet);
        }

        public void GuardarComuna(int ComunaId)
        {
            Session.Add("id_comuna", ComunaId);
        }

        public ActionResult CerrarSesion()
        {
            Session.Remove("UserLogin");
            Session.Remove("token");
            return RedirectToAction("Index");
        }
    }
}