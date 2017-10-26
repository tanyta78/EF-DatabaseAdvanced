namespace IntroToDbHomework
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class _02GetVillainsName
    {
        public static void GetVillainsNames(SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../FindAllVillainsNames.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var villainName = reader["Name"];
                var minionsCount = reader["MinionsCount"];
                Console.WriteLine($"{villainName} {minionsCount}");
            }
        }
    }
}
