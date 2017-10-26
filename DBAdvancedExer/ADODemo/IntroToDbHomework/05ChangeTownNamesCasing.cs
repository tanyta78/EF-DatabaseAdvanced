namespace IntroToDbHomework
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;

    public class _05ChangeTownNamesCasing
    {
        public static void ChangeTownNameCasingFromCountry(SqlConnection dbcon)
        {
            string country = Console.ReadLine();
            List<string> townAffected = new List<string>();

            string townAffectedQuery = @"SELECT TownName FROM Towns
                WHERE Country=@country";
            SqlCommand cmd = new SqlCommand(townAffectedQuery, dbcon);
            cmd.Parameters.AddWithValue("@country", country);

            SqlDataReader reader = cmd.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    var currentTown = reader["TownName"].ToString().ToUpper();
                    townAffected.Add(currentTown);

                }
            }

            foreach (var currentTown in townAffected)
            {
                ChangeTownName(currentTown, dbcon);
            }

            StringBuilder result = new StringBuilder();

            if (townAffected.Count != 0)
            {
                result.AppendLine($"{townAffected.Count} towns were affected.");
                result.AppendLine($"[{String.Join(", ", townAffected)}]");
            }
            else
            {
                result.AppendLine("No town names were affected.");
            }
            Console.Write(result.ToString());
        }

        public static void ChangeTownName(string currentTown, SqlConnection dbcon)
        {
            string UpdateTownNameQuery = @"UPDATE Towns
SET TownName=@currentTown
WHERE Towns.TownName=@currentTown";
            SqlCommand cmd = new SqlCommand(UpdateTownNameQuery, dbcon);
            cmd.Parameters.AddWithValue("@currentTown", currentTown);
            cmd.ExecuteNonQuery();
        }
    }
}
