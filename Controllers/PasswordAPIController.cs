using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using JTMaher2.Modules.JTM2_PasswordManager.Models;
using Newtonsoft.Json;

namespace JTMaher2.Modules.JTM2_PasswordManager.Controllers
{
    public class PasswordAPIController : DnnApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage GetPasswords(ModuleIdMasterPass moduleIdMasterPass)
        {
            return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(new Components.PasswordManager().GetPasswords(moduleIdMasterPass.ModuleId, moduleIdMasterPass.MasterPassword)));
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage SavePassword(Password pw)
        {
            new Components.PasswordManager().UpdatePassword(pw);
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}
