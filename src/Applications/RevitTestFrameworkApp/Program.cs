using System;
using System.Linq;
using RTF.Framework;

namespace RTF.Applications
{
    class Program
    {
        private static Runner runner;
        
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var setupData = Runner.ParseCommandLineArguments(args);

                runner = new Runner(setupData);

                Run();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private static void Run()
        {
            var assData = runner.Assemblies.FirstOrDefault();
            if (assData == null) return;

            runner.StartServer();
            if (runner.SetupTests())
            {
                runner.RunAllTests();
            }
            else
            {
                Console.Error.WriteLine("ERROR: No tests were run due to configuration problems");
            }
            runner.EndServer();
        }
    }
}
