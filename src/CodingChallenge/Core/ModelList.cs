using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenge.Core
{
    public class ModelList<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IList<T> Results { get; set; }
    }
}
