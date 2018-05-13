using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_ConsumingData
{
    [Serializable]
    public class Book
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
    }
}
