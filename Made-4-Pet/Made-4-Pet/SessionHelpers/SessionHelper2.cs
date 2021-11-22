using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.SessionHelpers
{
    public class SessionHelper2
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionHelper2(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetString(string param, string param2)
        {
            _session.SetString(param, param2);
        }

        public void GetString(string param)
        {
            var message = _session.GetString(param);
        }
    }
}

