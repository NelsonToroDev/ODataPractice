using System.Data.Services;
using System.Data.Services.Common;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;


namespace OptimizePOC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OptimizeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OptimizeService.svc or OptimizeService.svc.cs at the Solution Explorer and start debugging.
    
    public class OptimizeService : DataService<OptimizeDataSource> 
    {
        //public OptimizeService()
        //{
        //    //Configuration cfg = new Configuration();
        //    //cfg.Configure();

        //    // Add class mappings attributes to configuration object
        //    //cfg.AddInputStream(HbmSerializer.Default.Serialize(typeof(Models.Order)));
        //    //ISessionFactory sessionFactory = cfg.BuildSessionFactory();
        //}

        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Orders", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
