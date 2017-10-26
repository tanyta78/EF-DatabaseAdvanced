namespace IntroToDbHomework
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;

    class AddMinion
    {
        public static void ReadInfoInsertMinionVillain(SqlConnection dbcon)
        {
            //read input
            var minionInfo = Console.ReadLine().Split().ToArray();
            var minionName = minionInfo[1];
            var minionAge = minionInfo[2];
            var townName = minionInfo[3];

            var villainInfo = Console.ReadLine().Split().ToArray();
            var villainName = villainInfo[1];


            //check town
            if (!CheckIfTownExist(townName, dbcon))
            {
                AddTown(townName, dbcon);
                Console.WriteLine($"Town {townName} was added to the database.");

            }

            //check villain
            if (!CheckIfVillainExist(villainName, dbcon))
            {
                AddVillain(villainName, dbcon);
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }

            int townId = GetTownIdByName(townName, dbcon);
            AddMinionToDatabase(minionName, minionAge, townId, dbcon);

            int villianId = GetVillianIdByName(villainName, dbcon);
            int minionId = GetMinionIdByName(minionName, dbcon);
            AddMinionToVillian(minionId, villianId, dbcon);

            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
        }

        private static void AddMinionToVillian(int minionId, int villianId, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../InsertMinionToVillian.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@minionId", minionId);
            cmd.Parameters.AddWithValue("@villianId", villianId);
            cmd.ExecuteNonQuery();
        }

        private static int GetMinionIdByName(string minionName, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../MinionIdByName.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@minionName", minionName);
            return (int)cmd.ExecuteScalar();
        }

        private static int GetVillianIdByName(string villainName, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../VillianIdByName.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@villainName", villainName);
            return (int)cmd.ExecuteScalar();
        }



        private static int GetTownIdByName(string townName, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../TownIdByName.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@townName", townName);
            return (int)cmd.ExecuteScalar();
        }

        public static void AddMinionToDatabase(string minionName, string minionAge, int townId, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../InsertMinion.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@minionName", minionName);
            cmd.Parameters.AddWithValue("@minionAge", minionAge);
            cmd.Parameters.AddWithValue("@townId", townId);
            cmd.ExecuteNonQuery();
        }

        private static void AddVillain(string villainName, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../InsertVillain.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@villainName", villainName);
            cmd.ExecuteNonQuery();
        }

        private static bool CheckIfVillainExist(string villainName, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../CheckVillainExist.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@villainName", villainName);
            if ((int)cmd.ExecuteScalar() == 0)
            {
                return false;
            }

            return true;
        }

        private static void AddTown(string townName, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../InsertTown.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@townName", townName);
            cmd.ExecuteNonQuery();
        }

        private static bool CheckIfTownExist(string townName, SqlConnection dbcon)
        {
            string query = File.ReadAllText("../../CheckTownExist.sql");
            SqlCommand cmd = new SqlCommand(query, dbcon);
            cmd.Parameters.AddWithValue("@townName", townName);

            if ((int)cmd.ExecuteScalar() == 0)
            {
                return false;
            }
            return true;
        }
    }
}
