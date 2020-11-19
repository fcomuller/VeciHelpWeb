using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using VeciHelpWeb.Models;

namespace VeciHelpWeb.Interface
{
    public interface IVeciHelp
    {

        [Get("/vecihelp/GetPais")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> GetPais();

        [Get("/vecihelp/GetRegion?id={id}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> GetRegion(int id);

        [Get("/vecihelp/GetProvincia?id={id}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> GetProvincia(int id);

        [Get("/vecihelp/GetCiudad?id={id}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> GetCiudad(int id);

        [Get("/vecihelp/GetComuna?id={id}")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> GetComuna(int id);






        [Post("/vecihelp/CrearOrganizacion")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> CrearOrganizacion(OrganizacionRequest org);


        [Post("/vecihelp/EnrolarAdmin")]
        [Headers("Authorization: Bearer")]
        Task<HttpResponseMessage> EnrolarAdmin(RequestEnrolar enrolar);

    }
}