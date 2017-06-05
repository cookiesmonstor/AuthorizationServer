using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants {
    public static class Paths {
        public const string AuthorizationServerBaseAddress = "";
               
        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";

        public const string AuthorizePath = "/OAuth/Authorize";
        public const string TokenPath = "/OAuth/Token";
    }
}