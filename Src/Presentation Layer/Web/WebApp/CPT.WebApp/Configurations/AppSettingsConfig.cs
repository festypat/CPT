using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.WebApp.Configurations
{
    public class AppSettingsConfig
    {
        public string BaseUrl { get; set; }
        public string LoginUrl { get; set; }
        public string TransactionSummaryUrl { get; set; }
        public string TransactionHistoryUrl { get; set; }
    }
}
