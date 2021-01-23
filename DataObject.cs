using System.Collections.Generic;

namespace PoshMarkAssignment
{
    public class DataObject
    {
        public float Large { get; set; }
        public float XLarge { get; set; }

        public float X2Large { get; set; }
        public float X4Large { get; set; }
        public float X8Large { get; set; }
        public float X10Large { get; set; }
    }

    public class ResponseObject
    {
        public string Region { get; set; }
        public float Price { get; set; }

        public List<(string, int)> Server { get; set; }
    }   
}
