namespace LabAdo
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class StartUp
    {
        static void Main()
        {
            SqlConnection dbcon = new SqlConnection("Server=.\\SQLEXPRESS;Initial Catalog=SoftUni;Integrated Security = true;");
            dbcon.Open();

            using (dbcon)
            {
                while (true)
                {
                    Console.WriteLine("Enter command: ");
                    string command = Console.ReadLine();

                    switch (command)
                    {
                        case "list": ListAllProject(dbcon); break;
                        case "details": ShowDetails(dbcon); break;
                        case "search": SearchByName(dbcon); break;
                        case "exit": return;
                    }
                }
            }

        }

        public static void Demo(SqlConnection dbCon)
        {
            Console.WriteLine("Enter town name:");
            string townName = Console.ReadLine();

            //ExecuteScalar()
            string query = "SELECT COUNT(*) AS Count FROM Employees";
            SqlCommand cmd = new SqlCommand(query, dbCon);
            Console.WriteLine("Number of employees: {0}", cmd.ExecuteScalar());

            //ExecuteReader()
            string query2 = "SELECT * FROM Employees";
            SqlCommand cmd2 = new SqlCommand(query2, dbCon);
            var reader = cmd.ExecuteReader();

            using (reader)
            {
                //  reader.Read();//чете по редове това е първия ред
                // Console.WriteLine(reader[0]);//а така извикваме първата колонка
                // reader.FieldCount => column count
                // reader.GetName(0);= col name
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {

                        Console.Write(reader[i]);
                    }
                    Console.WriteLine();
                }
            }

            //ExecuteNonQuery();
            string query3 = $"INSERT INTO Towns VALUES('{townName}')";
            var cmd3 = new SqlCommand(query3, dbCon);

            int affected = cmd3.ExecuteNonQuery();//връща броя редове които са засегнати


            //use parameters to protect from sql injection
            string query4 = $"INSERT INTO Towns VALUES(@TownName)";
            var cmd4 = new SqlCommand(query4, dbCon);
            cmd4.Parameters.AddWithValue("@TownName", townName);


        }

        public static void SearchByName(SqlConnection dbcon)
        {
            Console.WriteLine("Enter search criteria: ");
            string pattern = Console.ReadLine();

            string query = @"
SELECT ProjectID
FROM Projects
WHERE Name LIKE @ProjectName";

            SqlCommand cmd = new SqlCommand(query, dbcon);

            cmd.Parameters.AddWithValue("@ProjectName", "%" + pattern + "%");
            //for scalar
            //int? result = (int?)cmd.ExecuteScalar() ?? -1;
            //if (result == -1)
            //{
            //    Console.WriteLine("Project not found");
            //    return;
            //}
            //Console.WriteLine("Project ID:{0}", result);

            //for more than one result
            var reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("Project not found");
                return;
            }

            using (reader)
            {
                List<string> ids = new List<string>();

                while (reader.Read())
                {
                    ids.Add(reader[0].ToString());
                }

                Console.WriteLine($"Found project with ID:{string.Join(", ", ids)}");
            }

        }

        public static void ShowDetails(SqlConnection dbcon)
        {

            Console.WriteLine("Enter project ID: ");

            int projectId = int.Parse(Console.ReadLine());


            string query = @"SELECT * FROM Projects WHERE ProjectID=@projectId";
            SqlCommand cmd = new SqlCommand(query, dbcon);
            //подаваме параметър за да предотвратим injection
            cmd.Parameters.AddWithValue("@projectId", projectId);

            var reader = cmd.ExecuteReader();

            using (reader)
            {
                if (!reader.Read())
                {
                    Console.WriteLine($"Project not found! ");
                    return;
                }

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine(reader.GetName(i));
                    Console.WriteLine("=====================");
                    Console.WriteLine(reader[i]);
                    Console.WriteLine("=====================");
                }
            }

            string query2 = @"
SELECT e.EmployeeId,e.FirstName,e.LastName
FROM Employees as e
JOIN EmployeesProjects as ep
ON ep.ProjectId=@projectId
AND e.EmployeeId=ep.EmployeeId";

            SqlCommand cmd2 = new SqlCommand(query2, dbcon);
            cmd2.Parameters.AddWithValue("@projectId", projectId);

            var reader2 = cmd2.ExecuteReader();

            using (reader)
            {
                Console.WriteLine("Assigned employees: ");
                if (!reader2.HasRows)
                {
                    Console.WriteLine($"No employees! ");
                }
                else
                {
                    using (reader2)
                    {
                        while (reader2.Read())
                        {
                            Console.WriteLine($"{reader2[0],4}| {reader2[1]} {reader2[2]}");

                        }
                    }
                }

            }
        }

        public static void ListAllProject(SqlConnection dbcon)
        {

            string query = @"SELECT ProjectId,Name FROM Projects";
            SqlCommand cmd = new SqlCommand(query, dbcon);

            var reader = cmd.ExecuteReader();

            Console.WriteLine("ID | Project Name");
            Console.WriteLine("---+-------------------------");

            using (reader)
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0],4}| {reader[1]} ");

                }
            }

        }

    }
}
