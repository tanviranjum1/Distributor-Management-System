using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DistributorManagement
{
    public class LoginInfo
    {

        public static int ClientId {
            get{
                return Int32.Parse(WebConfigurationManager.AppSettings["ClientId"]);
            }

        }
        public static bool CodeBasedSetup
        {
            get
            {
                return Boolean.Parse(WebConfigurationManager.AppSettings["CodeBasedSetup"]);
            }

        }

    }
}