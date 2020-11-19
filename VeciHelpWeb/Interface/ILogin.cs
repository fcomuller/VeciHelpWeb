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
    public interface ILogin
    {
        [Post("/login/authenticate")]
        Task<HttpResponseMessage> PostLogin(Login log);
    }
}