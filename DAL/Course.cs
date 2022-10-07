using System;
using System.Collections.Generic;

namespace DAL
{
    [Serializable]
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Modules { get; set; }
    }
}
