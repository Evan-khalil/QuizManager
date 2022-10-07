using System;
using System.Collections.Generic;

namespace DAL
{
    [Serializable]
    public class QuizItem
    {
        public int Id { get; set; }
        public List<string> DescriptionStrings { get; set; }
        public Dictionary<string, List<string>> LinkedTo { get; set; }
    }
}
