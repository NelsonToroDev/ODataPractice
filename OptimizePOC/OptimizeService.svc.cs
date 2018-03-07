using System.Data.Services;
using System.Data.Services.Common;


namespace OptimizePOC
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OptimizeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OptimizeService.svc or OptimizeService.svc.cs at the Solution Explorer and start debugging.
    public class OptimizeService : DataService<OptimizeDataModel>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All | EntitySetRights.AllRead
                                                                         | EntitySetRights.WriteMerge
                                                                         | EntitySetRights.WriteReplace);
            
            
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
