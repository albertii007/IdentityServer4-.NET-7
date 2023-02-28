using System;

namespace ASP.IdentityServer.Core.Common.Exceptions
{
    public class OnLoginFailureException : Exception
    {
        public OnLoginFailureException() : base("Incorrect password!")
        {

        }
    }
}
