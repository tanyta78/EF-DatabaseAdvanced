using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Serializer ser = new Serializer();
            string path = string.Empty;
            string xmlInputData = string.Empty;
            string xmlOutputData = string.Empty;

            //EXAMPLE 1
            path = Directory.GetCurrentDirectory() + @"\Customer.xml";
            xmlInputData = File.ReadAllText(path);

            Customer customer = ser.Deserialize<Customer>(xmlInputData);
            xmlOutputData = ser.Serialize<Customer>(customer);

            // EXAMPLE 2
            path = Directory.GetCurrentDirectory() + @"\CustOrders.xml";
            xmlInputData = File.ReadAllText(path);

            Customer customer2 = ser.Deserialize<Customer>(xmlInputData);
            xmlOutputData = ser.Serialize<Customer>(customer2);

            Console.ReadKey();

        }

    }

}
