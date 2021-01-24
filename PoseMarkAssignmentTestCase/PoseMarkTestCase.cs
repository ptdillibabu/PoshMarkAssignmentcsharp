using Newtonsoft.Json;
using PoshMarkAssignment;
using System.Collections.Generic;
using Xunit;

namespace PoseMarkAssignmentTestCase
{
    public class PoseMarkTestCase
    {
        [Fact]
        public void TotalCostTestCase()
        {
            string output = "[{\"Region\":\"us-east\",\"Price\":5.64,\"Server\":[{\"Item1\":\"x10large\",\"Item2\":1}]},{\"Region\":\"us-west\",\"Price\":5.94,\"Server\":[{\"Item1\":\"x10large\",\"Item2\":1}]},{\"Region\":\"asia\",\"Price\":4.72,\"Server\":[{\"Item1\":\"x8large\",\"Item2\":2}]}]";
            List<ResponseObject> actual = JsonConvert.DeserializeObject<List<ResponseObject>>(output);
            ResponseFormationHelper rp = new ResponseFormationHelper();
            List<ResponseObject> expected = rp.Get_cost(2, 32, 8);
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }
    }
}

