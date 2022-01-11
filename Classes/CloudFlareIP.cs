using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JupiterEcoTech.Classes
{
    public static class CloudFlareIP
    {
        public static string GetIPAddress(this HttpRequestBase request)
        {
            if (request.Headers["CF-CONNECTING-IP"] != null)
                return request.Headers["CF-CONNECTING-IP"];
            var ipaddr = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipaddr))
            {
                var addr = ipaddr.Split('.');
                if (addr.Length != 0)
                    return addr[0];
            }
            return request.UserHostAddress;
        }

        public static string GetCountryName(this HttpRequestBase request)
        {
            return request.Headers["CF-IPCountry"];
        }
    }
}