namespace IntroToDbHomework
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            SqlConnection dbcon = new SqlConnection("Server =.\\SQLEXPRESS; Initial Catalog = MinionsDB; Integrated Security = true; ");
            dbcon.Open();

            using (dbcon)
            {
                //to do *Bonus task: Make sure all operations are executed successfully. In case of an error do not change the database.
                

            }
        }

       

       
       
    }
}
