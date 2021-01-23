using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace PoshMarkAssignment
{
    public class Program
    {
        //Collecting input from user
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the number of hours wants to use the servers");
            long hours = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Please enter the minimum number of cpu  needs");
            long cpuCount = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Please enter the maximum price able to pay ");
            float price = float.Parse(Console.ReadLine());
            List<ResponseObject> response = Get_cost(hours, cpuCount, price);
            string serilizedObject = JsonConvert.SerializeObject(response);
            Console.WriteLine(serilizedObject.ToString());
        }
        //Created public methof for unit testing
        //Calculate the cost
        public static List<ResponseObject> Get_cost(long hours, long cpuCount, float price)
        {
            Dictionary<ServerListEnum, int> serverTypes = new Dictionary<ServerListEnum, int>() { { ServerListEnum.large, 1 }, { ServerListEnum.xlarge, 2 }, { ServerListEnum.x2large, 4 }, { ServerListEnum.x4large, 8 }, { ServerListEnum.x8large, 16 }, { ServerListEnum.x10large, 32 } };
            List<ResponseObject> responseObjects = new List<ResponseObject>();
            DataRepositary repositaryData = new DataRepositary();
            if (hours == 0)
                return ServerWithNoHours(cpuCount, price, responseObjects, serverTypes, repositaryData, 1);
            if (price == 0)
                return ServerWithNoPrice(cpuCount, hours, responseObjects, serverTypes, repositaryData);
            else
                return ServerWithNoHours(cpuCount, price, responseObjects, serverTypes, repositaryData, hours);

        }

        private static List<ResponseObject> ServerWithNoPrice(long cpuCount, long hours, List<ResponseObject> responseObjects, Dictionary<ServerListEnum, int> serverTypes, DataRepositary repositaryData)
        {
            Dictionary<string, List<(string, int)>> locServerList = new Dictionary<string, List<(string, int)>>();
            long tempCount = cpuCount;
            foreach (var loc in repositaryData.ServerDetails.Keys)
            {
                cpuCount = tempCount;
                float price = 0;
                List<(string, int)> serverCountList = new List<(string, int)>();
                while (cpuCount > 0)
                {
                    if (cpuCount >= 32 && repositaryData.ServerDetails[loc].X10Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "x10large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "x10large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "x10large");
                            serverCountList.Add(updateServer);

                        }
                        else
                        {
                            serverCountList.Add(("x10large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.x10large];
                        price += (hours * repositaryData.ServerDetails[loc].X10Large);
                    }
                    else if (cpuCount >= 16 && repositaryData.ServerDetails[loc].X8Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "x8large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "x8large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "x8large");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("x8large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.x8large];
                        price += (hours * repositaryData.ServerDetails[loc].X8Large);
                    }
                    else if (cpuCount >= 8 && repositaryData.ServerDetails[loc].X4Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "x4large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "x4large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "x4large");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("x4large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.x4large];
                        price += (hours * repositaryData.ServerDetails[loc].X4Large);
                    }
                    else if (cpuCount >= 4 && repositaryData.ServerDetails[loc].X2Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "x2large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "x2large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "x2large");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("x2large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.x2large];
                        price += (hours * repositaryData.ServerDetails[loc].X2Large);
                    }
                    else if (cpuCount >= 2 && repositaryData.ServerDetails[loc].XLarge > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "xlarge"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "xlarge");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "xlarge");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("xlarge", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.xlarge];
                        price += (hours * repositaryData.ServerDetails[loc].XLarge);
                    }
                    else if (cpuCount >= 1 && repositaryData.ServerDetails[loc].Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "large");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.large];
                        price += repositaryData.ServerDetails[loc].Large;
                    }
                    else
                    {
                        break;
                    }
                }
                responseObjects.Add(new ResponseObject() { Server = serverCountList, Region = loc, Price = price });
            }
            return responseObjects;
        }

        private static List<ResponseObject> ServerWithNoHours(long cpuCount, float price, List<ResponseObject> responseObjects, Dictionary<ServerListEnum, int> serverTypes, DataRepositary repositaryData, long hours = 1)
        {
            long tempCount = cpuCount;
            float tempPrice = price;            
            float startingPrice = price;
            foreach (var loc in repositaryData.ServerDetails.Keys)
            {
                cpuCount = tempCount;
                price = tempPrice;
                float calculatePrice = 0;
                List<(string, int)> serverCountList = new List<(string, int)>();
                while (cpuCount > 0 && price > 0)
                {
                    if (cpuCount >= 32 && repositaryData.ServerDetails[loc].X10Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "x10large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "x10large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "x10large");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("x10large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.x10large];
                        price -= (hours * repositaryData.ServerDetails[loc].X10Large);
                        calculatePrice += (hours * repositaryData.ServerDetails[loc].X10Large);
                    }
                    else if (cpuCount >= 16 && repositaryData.ServerDetails[loc].X8Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "x8large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "x8large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "x8large");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("x8large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.x8large];
                        price -= (hours * repositaryData.ServerDetails[loc].X8Large);
                        calculatePrice += (hours * repositaryData.ServerDetails[loc].X8Large);
                    }
                    else if (cpuCount >= 8 && repositaryData.ServerDetails[loc].X4Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "x4large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "x4large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "x4large");
                            serverCountList.Add(updateServer);

                        }
                        else
                        {
                            serverCountList.Add(("x4large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.x4large];
                        price -= (hours * repositaryData.ServerDetails[loc].X4Large);
                        calculatePrice += (hours * repositaryData.ServerDetails[loc].X4Large);
                    }
                    else if (cpuCount >= 4 && repositaryData.ServerDetails[loc].X2Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "x2large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "x2large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "x2large");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("x2large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.x2large];
                        price -= (hours * repositaryData.ServerDetails[loc].X2Large);
                        calculatePrice += (hours * repositaryData.ServerDetails[loc].X2Large);
                    }
                    else if (cpuCount >= 2 && repositaryData.ServerDetails[loc].XLarge > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "xlarge"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "xlarge");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "xlarge");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("xlarge", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.xlarge];
                        price -= (hours * repositaryData.ServerDetails[loc].XLarge);
                        calculatePrice += (hours * repositaryData.ServerDetails[loc].XLarge);
                    }
                    else if (cpuCount >= 1 && repositaryData.ServerDetails[loc].Large > 0)
                    {
                        if (serverCountList.Exists((server) => server.Item1 == "large"))
                        {
                            var updateServer = serverCountList.Find((x) => x.Item1 == "large");
                            updateServer.Item2 += 1;
                            serverCountList.RemoveAll((x) => x.Item1 == "large");
                            serverCountList.Add(updateServer);
                        }
                        else
                        {
                            serverCountList.Add(("large", 1));
                        }
                        cpuCount -= serverTypes[ServerListEnum.large];
                        price -= (hours * repositaryData.ServerDetails[loc].Large);
                        calculatePrice += (hours * repositaryData.ServerDetails[loc].Large);
                    }
                    else
                    {
                        break;
                    }
                }
                responseObjects.Add(new ResponseObject() { Server = serverCountList, Region = loc, Price = calculatePrice });
            }
            return responseObjects;
        }        
    }
}
