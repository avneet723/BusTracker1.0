using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;

namespace BusTracker
{
    class Class1
    {
    }

    public static class SharedState
    {
        public static ITwitterAuthorizer Authorizer { get; set; }
    }
}
