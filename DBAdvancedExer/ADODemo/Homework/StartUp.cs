namespace Homework
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;

    public class StartUp
    {
        static void Main()
        {
            // in this query we can input db creation sql query
            // string query = File.ReadAllText("../../ItitialSetup.sql");

            SqlConnection dbcon =
                new SqlConnection("Server=.\\SQLEXPRESS;Initial Catalog=MinionsDB;Integrated Security = true;");
            dbcon.Open();

            using (dbcon)
            {
                var minionInput = Console.ReadLine().Split(' ').ToArray();
                string minionName = minionInput[1];
                int minionAge = int.Parse(minionInput[2]);
                string minionTown = minionInput[3];
                var villainName = Console.ReadLine().Split().ToArray()[1];

                //check exist minion town
                string townQuery = File.ReadAllText("../../CheckTownExist.sql");
                SqlCommand townCheckCommand= new SqlCommand(townQuery,dbcon);

                if (townCheckCommand.ExecuteScalar()==null)
                {
                    SqlCommand townInsert = new SqlCommand("INSERT INTO Town VALUES " +
                                                                       "(@townName, 1)", dbcon);

                }

            }

        }

        private static void GetMinionName2(SqlConnection dbcon)
        {
            string findNamequery = File.ReadAllText("../../VilianNameById.sql");
            SqlCommand findVilianNameCommand = new SqlCommand(findNamequery, dbcon);

            int vilianId = int.Parse(Console.ReadLine());

            SqlParameter vilianIdParameter = new SqlParameter("@vilianId", vilianId);
            findVilianNameCommand.Parameters.Add(vilianIdParameter);

            SqlDataReader reader = findVilianNameCommand.ExecuteReader();

            if (reader.Read())
            {
                string villName = (string)reader["name"];
                Console.WriteLine($"Villain: {villName}");

                string findMinionsQuery = File.ReadAllText("../../FindMinions.sql");
                SqlCommand findMinionsCommand = new SqlCommand(findMinionsQuery, dbcon);
                SqlParameter param = new SqlParameter("@vilianId", vilianId);
                findMinionsCommand.Parameters.Add(param);
                reader.Close();

                SqlDataReader minionsReader = findMinionsCommand.ExecuteReader();
                int index = 1;
                if (!minionsReader.Read())
                {
                    Console.WriteLine("<no minions>");
                }
                else
                {
                    while (minionsReader.Read())
                    {
                        string minionsNAme = (string)minionsReader["name"];
                        int minionAge = (int)minionsReader["age"];
                        Console.WriteLine($"{index}. {minionsNAme} {minionAge}");
                        index++;
                    }
                }
                
            }
            else
            {
                Console.WriteLine($"No villain with ID {vilianId} exists in the database.");
            }
        }

        public static void GetMinionsName1(SqlConnection dbcon)
        {
            Console.WriteLine("Please insert vilian id");
            int vilianId = int.Parse(Console.ReadLine());

            string query = @"
SELECT  v.Name,m.Name,m.Age
FROM Vilians AS v
JOIN ViliansMinions AS vm ON v.Id=vm.VilianId
JOIN Minions AS m ON m.Id=vm.MinionId
where v.id=@VilianId";

            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@VilianId", vilianId);

            var reader = cmd.ExecuteReader();
            using (reader)
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine($"No villain with ID {vilianId} exists in the database.");

                    //    Console.WriteLine("(no minions)");

                    return;
                }
                reader.Read();
                Console.WriteLine($"Viliant: {reader[0]}");
                Console.WriteLine("=====================");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    reader.Read();

                    Console.WriteLine($"{i + 1}. {reader[1],-12}| {reader[2],-12} ");
                    Console.WriteLine("=====================");

                }
            }
        }

        public static void GetAllVilainsNames(SqlConnection dbcon)
        {
            string query = @"
SELECT Name,count(*)as [Number of minions]
FROM Vilians
JOIN ViliansMinions on ViliansMinions.VilianId=Vilians.Id
GROUP BY Vilians.Name
HAVING count(*)>3
ORDER BY count(*) desc";

            SqlCommand cmd = new SqlCommand(query, dbcon);

            SqlDataReader reader = cmd.ExecuteReader();
            using (reader)
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("There are no vilians with more than 3 minions!");
                    return;
                }
                Console.WriteLine("Vilian name | Number of minions");
                Console.WriteLine("============+==================");
                while (reader.Read())
                {

                    //Console.WriteLine($"{reader[0],-12}| {reader[1]} ");
                    //Console.WriteLine("------------+-----------------");
                    string villainName = (string) reader["Name"];
                    int countMinions = (int) reader["Number of minions"];
                    Console.WriteLine($"{villainName}={countMinions}" );
                }
            }
        }
    }
}
