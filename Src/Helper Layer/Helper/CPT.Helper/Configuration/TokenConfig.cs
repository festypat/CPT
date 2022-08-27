using System;
using System.Collections.Generic;
using System.Text;

namespace CPT.Helper.Configuration
{
    public class TokenConfig
    {
        public string SecretKey { get; set; }
        public string TokenType { get; set; }
        public int TokenExpiration { get; set; }
    }
}
