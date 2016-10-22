// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="FI MUNI">
//   Tobias Kamenicky, Marek Halas, Robert Havlicek, Miroslav Gasparovic
// </copyright>
// <summary>
//   Launcher class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MyWallet
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main method
        /// </summary>
        /// <param name="args">
        /// Parameters for the program
        /// </param>
        [SuppressMessage("ReSharper", "TooManyChainedReferences", Justification = "This is a convention builder")]
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
