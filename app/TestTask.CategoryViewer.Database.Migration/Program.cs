﻿namespace TestTask.CategoryViewer.Database.Migration
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;

    using DbUp;

    internal static class Program
    {
        private static int Main(string[] args)
        {
            var connectionString = args.FirstOrDefault() ?? ConfigurationManager.ConnectionStrings["Categories"].ConnectionString;

            var upgrader = DeployChanges.To
                            .SqlDatabase(connectionString)
                            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                            .LogToConsole()
                            .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
