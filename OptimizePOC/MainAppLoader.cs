using System;
using System.Reflection;
using Common.Logging;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Spring.Context;
using Spring.Context.Support;
using System.Data.Services;

namespace OptimizePOC
{
    /// <summary>
    /// Application loader. Setups and loads the Spring.NET context that runs the entire application.
    /// </summary>
    public sealed class MainAppLoader : IDisposable
    {
        /// <summary>
        /// Standard logger.
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Spring.NET application context.
        /// </summary>
        private IApplicationContext context;

        /// <summary>
        /// First method to call. Loads and runs
        /// </summary>
        
        public void LoadAndRunApp()
        {
            //ConfigureLogger();

            try
            {
                this.context = ContextRegistry.GetContext();
            }
            catch (Exception e)
            {
                Logger.FatalFormat("Error loading main application context", e);
                Logger.Fatal("Rethrowing to the OS the exception raised");
                throw;
            }

            HostService();
        }

        public void HostService()
        {
            Type serviceType = typeof(OptimizeService);
            Uri baseAddress = new Uri("http://localhost:6000/");
            Uri[] baseAddresses = new Uri[] { baseAddress };

            // Create a new hosting instance for the OptimizeService 
            // data service at the specified address. 
            DataServiceHost host = new DataServiceHost(
               serviceType,
               baseAddresses);
            host.Open();

            // Keep the data service host open while the console is open. 
            Console.WriteLine(
              "Navigate to the following URI to see the service.");
            Console.WriteLine(baseAddress);
            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();

            // Close the host. 
            host.Close();
        }

        /// <summary>
        /// Calls to Dispose().
        /// </summary>
        public void Shutdown()
        {
            this.context.Dispose();
        }

        /// <summary>
        /// Last method to call. Safely shutdowns 
        /// </summary>
        public void Dispose()
        {
            this.Shutdown();
        }

        /// <summary>
        /// Performs Logger configuration that cannot be done by the XML configuration file.
        /// </summary>
        private static void ConfigureLogger()
        {
            Hierarchy hierarchy = (Hierarchy)log4net.LogManager.GetRepository();
            Logger nhibernateLogger = (Logger)hierarchy.GetLogger("NHibernate");
            nhibernateLogger.Level = Level.Off;
        }
    }
}