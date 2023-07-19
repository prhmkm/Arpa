using System;
using System.Collections.Generic;
using System.Text;

namespace ArpaBackend.Core.Model.Base
{
    public class AppSettings
    {
        public string TokenSecret { get; set; }
        public int TokenValidateInMinutes { get; set; }
        public string MagfaSmsServiceUrl { get; set; }
        public string MagfaSmsUsername { get; set; }
        public string MagfaSmsPassword { get; set; }
        public string PublishImagePath { get; set; }
        public string SaveImagePath { get; set; }
        public string DefaultLanguage { get; set; }
        public int EventGB { get; set; }
        public int EventIR { get; set; }
        public int EventDE { get; set; }
        public int EventSA { get; set; }
        public int EventFR { get; set; }
        public string ApiKey { get; set; }
        public string Url { get; set; }

    }
}
