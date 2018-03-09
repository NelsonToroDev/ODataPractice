using System;
using log4net;
using OptimizePOC;

namespace CommandLineRunner
{
    public class OptimizeConsoleStartUp
    {
        /// <summary>
        /// Main method which is the responsible to load the application. 
        /// </summary>
        /// <param name="args">Some needed parameters.</param>
        public static void Main(string[] args)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info("Loading Optimize from the console line");
            MainAppLoader mainAppLoader = new MainAppLoader();
            mainAppLoader.LoadAndRunApp();
            logger.Info("Optimize is up and running. Press key to halt");
            Console.ReadKey();
            mainAppLoader.Shutdown();
            logger.Info("Shutting down Optimize");
        }
    }
}
