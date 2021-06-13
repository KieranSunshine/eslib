using System;

namespace Eslib.Models.Internals
{
    public class EsiTokens
    {
        public EsiTokens()
        {
            Access = new AccessToken();
            Refresh = string.Empty;
        }

        public AccessToken Access { get; set; }
        public string Refresh { internal get; set; }
    }

    public class AccessToken
    {

    }
}