using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JupiterEcoTech.Models
{
    public class GeneralModels
    {
        public string ip { get; set; }

        public string country_code { get; set; }

        public string ex { get; set; }

        public string servername { get; set; }

        public string connection_string { get; set; }

        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success
            {
                get;
                set;
            }

            [JsonProperty("error-codes")]
            public List<string> ErrorMessage
            {
                get;
                set;
            }
        }
    }
}