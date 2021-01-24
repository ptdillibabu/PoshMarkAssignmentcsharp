using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace PoshMarkAssignment
{
    class Program
    {
        static ResponseFormationHelper helper = new ResponseFormationHelper();


        /*Collecting input from user
         * Calling GetCost method from helper class with user input.
        */
        static void Main(string[] args)
        {                       
            Console.WriteLine("Please enter the number of hours wants to use the servers");
            long hours = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Please enter the minimum number of cpu  needs");
            long cpuCount = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Please enter the maximum price able to pay ");
            float price = float.Parse(Console.ReadLine());
            List<ResponseObject> response = helper.Get_cost(hours, cpuCount, price);
            string serilizedObject = JsonConvert.SerializeObject(response);
            Console.WriteLine(serilizedObject.ToString());
        }
        
        

    }
}
