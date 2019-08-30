using System;
using System.Collections.Generic;
using System.Text;

namespace VertMarkets.Models
{
    public class AnswerResponse : Token
    {
        public Answer data { get; set; }
        public string message { get; set; }

        
    }
}
