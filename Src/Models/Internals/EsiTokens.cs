using System;

namespace Eslib.Models.Internals
{
    public class EsiTokens
    {
        public EsiTokens()
        {
            Access = new Token();
            Refresh = new Token();
        }

        public Token Access { get; set; }
        public Token Refresh { get; set; }
    }

    public class Token
    {
        private DateTime? _expiryDate;
        public Token()
        {
            Value = string.Empty;
            ExpiryDate = null;
        }
        
        public string Value { get; set; }
        public DateTime? ExpiryDate
        {
            get => _expiryDate;
            set => _expiryDate = value?.ToUniversalTime();
        }
        public bool HasExpired => !ExpiryDate.HasValue || DateTime.UtcNow > ExpiryDate;
    }
}