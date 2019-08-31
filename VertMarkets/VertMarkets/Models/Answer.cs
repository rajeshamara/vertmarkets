using System;
using System.Collections.Generic;
using System.Text;

namespace VertMarkets.Models
{
    public class Answer 
    {
        public string totalTime { get; set; }
        public bool answerCorrect { get; set; }
        public List<string> shouldBe { get; set; }
    }
}
