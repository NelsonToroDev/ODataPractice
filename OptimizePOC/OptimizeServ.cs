using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services;
using System.Data.Services.Common;

namespace OptimizePOC
{
    public class OptimizeServ : DataService<OptimizeDataSource>
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
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}