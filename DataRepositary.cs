using System.Collections.Generic;

namespace PoshMarkAssignment
{
    public class DataRepositary
    {
        public readonly Dictionary<string, DataObject> ServerDetails;

        public DataRepositary()
        {
            ServerDetails = new Dictionary<string, DataObject>() { { "us-east", new DataObject() {Large=0.12F,XLarge=0.23F,X2Large=0.45F,X4Large=0.774F,X8Large=1.4F,X10Large=2.82F } },
                                 { "us-west", new DataObject() {Large=0.14F,X2Large=0.143F,X4Large=0.89F,X8Large=1.3F,X10Large=2.97F } },
                                 { "asia", new DataObject() {Large=0.11F,XLarge=0.20F,X4Large=0.67F,X8Large=1.18F } },
            };
        }        
    }
}
