using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seltest_1
{
    public class Values
    {
        public string AzureWebJobsStorage { get; set; }
        public string AzureWebJobsDashboard { get; set; }
        public string FUNCTIONS_WORKER_RUNTIME { get; set; }
        public string websiteUrl { get; set; }
        public string siginEmailid { get; set; }
        public string SigInPassword { get; set; }
        public string PortalUrl { get; set; }
        public Int32 threadSleep { get; set; }

    }
    public class ConnectionStorage
    {
        public bool IsEncrypted { get; set; }
        public Values Values { get; set; }

    }
}
