﻿namespace P03_FootballBetting
{
    using Data;

    public class StartUp
    {
       public static void Main()
       {
           var context = new FootballBettingContext();

           context.Database.EnsureCreated();
       }
    }
}
