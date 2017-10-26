using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToDbHomework
{
    using System.Data.SqlClient;
    using System.IO;

    public class _03GetMinionNames
    {
        public static void FindMinionNames(SqlConnection dbcon)
        {
            Console.WriteLine("Please insert villain Id");
            int villainId = int.Parse(Console.ReadLine());

            string findVillainNameQuery = File.ReadAllText("../../FindVillainName.sql");
            SqlCommand cmdVillainName = new SqlCommand(findVillainNameQuery, dbcon);
            var villainParam = new SqlParameter("@villainId", villainId);

            cmdVillainName.Parameters.Add(villainParam);
            var reader = cmdVillainName.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"Villain: {reader["name"]}");

                reader.Close();
                string findMinionNamesQuery = File.ReadAllText("../../FindMinionNamesForVillainId.sql");
                var minionParam = new SqlParameter("@villainId", villainId);
                SqlCommand cmdMinionsNames = new SqlCommand(findMinionNamesQuery, dbcon);
                cmdMinionsNames.Parameters.Add(minionParam);
                var reader2 = cmdMinionsNames.ExecuteReader();
                if (!reader2.Read())
                {
                    Console.WriteLine("(no minions)");
                }
                else
                {
                    var index = 1;
                    while (reader2.Read())
                    {
                        Console.WriteLine($"{index}. {reader2["Name"]} {reader2["Age"]}");
                        index++;
                    }
                }
            }
            else
            {
                Console.WriteLine($"No villain with ID {villainId} exists in the database.");
            }
        }
    }
}
